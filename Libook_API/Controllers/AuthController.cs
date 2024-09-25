using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Service.EmailService;
using Libook_API.Service.AuthService;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore;
using NSwag.Annotations;
using Google.Apis.Auth;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenService tokenRepository;
        private readonly IEmailService emailService;
        private readonly IWebHostEnvironment env;
        private readonly ILogger<AuthController> logger;

        public AuthController(UserManager<IdentityUser> userManager, ITokenService tokenRepository, IEmailService emailService, IWebHostEnvironment env, ILogger<AuthController> logger)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.emailService = emailService;
            this.env = env;
            this.logger = logger;
        }

        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] CustomerRegisterDTO customerRegisterDTO)
        {
            var existingUser = await userManager.FindByNameAsync(customerRegisterDTO.Username);
            if (existingUser != null)
            {
                return BadRequest("Username already exists.");
            }

            var identityUser = new IdentityUser
            {
                UserName = customerRegisterDTO.Username,
                Email = customerRegisterDTO.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, customerRegisterDTO.Password);

            if (identityResult.Succeeded)
            {
                // Add roles to this User
                if (customerRegisterDTO.Roles != null && customerRegisterDTO.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, customerRegisterDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        // Tạo token xác nhận email
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(identityUser);

                        // Tạo URL xác nhận email
                        var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { userId = identityUser.Id, token = token }, Request.Scheme);

                        // Gửi email chứa link xác nhận
                        //await emailService.SendEmailAsync(identityUser.Email, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{confirmationLink}'>link</a>");

                        var confirmEmailDTO = new ConfirmEmailDTO
                        {
                            Username = identityUser.UserName,
                            ConfirmationLink = confirmationLink
                        };

                        await emailService.SendConfirmEmailAsync(identityUser.Email, "Confirm your email", "Template/EmailTemplate/ConfirmEmailTemplate.html", confirmEmailDTO);

                        return Ok("User was registered! Please login.");
                    }
                }
            }

            return BadRequest("Something went wrong");
        }

        // POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] CustomerLoginDTO customerLoginDTO)
        {
            var user = await userManager.FindByEmailAsync(customerLoginDTO.Username);

            if (user != null)
            {
                if (!await userManager.IsEmailConfirmedAsync(user))
                {
                    return BadRequest("Email is not confirmed. Please confirm your email first.");
                }

                var checkPasswordResult = await userManager.CheckPasswordAsync(user, customerLoginDTO.Password);

                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new ResponseObject
                        {
                            status = System.Net.HttpStatusCode.OK,
                            message = "Login successfully!",
                            data = new CustomerResponseDTO
                            {
                                JwtToken = jwtToken
                            }
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest("Username or password incorrect");
        }

        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            //// Get the base directory
            //string basePath = Directory.GetCurrentDirectory();
            // Combine the base directory with your relative paths
            string failedTemplatePath = Path.Combine(env.ContentRootPath, "Template", "EmailTemplate", "ConfirmEmailFailureTemplate.html");
            string successTemplatePath = Path.Combine(env.ContentRootPath, "Template", "EmailTemplate", "ConfirmEmailSuccessTemplate.html");

            // Read the correct template based on the outcome
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                return Content(await System.IO.File.ReadAllTextAsync(failedTemplatePath), "text/html");
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Content(await System.IO.File.ReadAllTextAsync(failedTemplatePath), "text/html");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);

            string templatePath = result.Succeeded ? successTemplatePath : failedTemplatePath;
            return Content(await System.IO.File.ReadAllTextAsync(templatePath), "text/html");
        }


        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] CustomerLoginByGoogleDTO customerLoginByGoogleDTO)
        {
            // Xác minh token JWT nhận được từ phía client
            var payload = await GoogleJsonWebSignature.ValidateAsync(customerLoginByGoogleDTO.Token);

            // Nếu token hợp lệ, bạn có thể truy cập các thông tin từ payload
            // Ví dụ: email, name, và các claims khác
            var email = payload.Email;

            // Xử lý đăng nhập tại đây, ví dụ: kiểm tra người dùng trong cơ sở dữ liệu, tạo session, v.v.

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // Nếu người dùng chưa tồn tại, tạo mới
                var newUser = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };
                var createResult = await userManager.CreateAsync(newUser);
                if (!createResult.Succeeded)
                {
                    return BadRequest("Failed to create a new user from Google account.");
                }
                await userManager.AddToRoleAsync(newUser, "Customer");
                user = newUser;
            }
            else
            {
                if (user.PasswordHash != null)
                {
                    return BadRequest("Invalid token");
                }
            }

            // Lấy các role của người dùng
            var roles = await userManager.GetRolesAsync(user);

            // Tạo JWT token cho người dùng
            var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

            return Ok(new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Google login successful!",
                data = new CustomerResponseDTO
                {
                    JwtToken = jwtToken // Trả về JWT token để client sử dụng cho các request sau
                }
            });
        }
    }
    //Nào đủ năng lực quay lại làm cách này nhé Dwuangoiwwwww
    //// Initiates Google login challenge
    //[IgnoreAntiforgeryToken]
    //[HttpGet("Login-by-Google")]
    //[AllowAnonymous]
    //public IActionResult LoginByGoogle()
    //{
    //    var state = Guid.NewGuid().ToString(); // Tạo giá trị state
    //    HttpContext.Session.SetString("OAuthState", state); // Lưu giá trị vào session

    //    var properties = new AuthenticationProperties
    //    {
    //        RedirectUri = Url.Action("GoogleResponse", "Auth"),
    //        Items =
    //        {
    //            { "scheme", GoogleDefaults.AuthenticationScheme },
    //            { "state", state } // Gắn state vào request
    //        }
    //    };

    //    return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    //}

    //[IgnoreAntiforgeryToken]
    //[HttpGet("signin-google")]
    //[AllowAnonymous]
    //public async Task<IActionResult> GoogleResponse()
    //{

    //    var state = HttpContext.Session.GetString("OAuthState");
    //    if (state == null || state != Request.Query["state"])
    //    {
    //        return BadRequest("Invalid OAuth state.");
    //    }

    //    var result = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
    //    if (!result.Succeeded)
    //    {
    //        return BadRequest("Google login failed.");
    //    }

    //    var claims = result.Principal.Identities.FirstOrDefault()?.Claims;
    //    var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

    //    // Kiểm tra xem người dùng đã tồn tại chưa
    //    var user = await userManager.FindByEmailAsync(email);
    //    if (user == null)
    //    {
    //        // Nếu người dùng chưa tồn tại, tạo mới
    //        var newUser = new IdentityUser
    //        {
    //            UserName = email,
    //            Email = email,
    //            EmailConfirmed = true
    //        };
    //        var createResult = await userManager.CreateAsync(newUser);
    //        if (!createResult.Succeeded)
    //        {
    //            return BadRequest("Failed to create a new user from Google account.");
    //        }
    //        await userManager.AddToRoleAsync(newUser, "Customer");
    //        user = newUser;
    //    }

    //    // Lấy các role của người dùng
    //    var roles = await userManager.GetRolesAsync(user);

    //    // Tạo JWT token cho người dùng
    //    var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

    //    // Đăng nhập user bằng cookie và gắn JWT token vào cookie
    //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result.Principal);

    //    // Trả về token cho client
    //    return Ok(new ResponseObject
    //    {
    //        status = System.Net.HttpStatusCode.OK,
    //        message = "Google login successful!",
    //        data = new CustomerResponseDTO
    //        {
    //            JwtToken = jwtToken // Trả về JWT token để client sử dụng cho các request sau
    //        }
    //    });
    //}
}

