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

        public AuthController(UserManager<IdentityUser> userManager, ITokenService tokenRepository, IEmailService emailService, IWebHostEnvironment env)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.emailService = emailService;
            this.env = env;
        }

        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] CustomerRegisterDTO customerRegisterDTO)
        {
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
        [HttpGet("LoginbyGoogle")]
        public async Task<IActionResult> LoginbyGoogle()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                return BadRequest("Google login failed.");
            }

            // Extract user information from the claims
            var claims = result.Principal?.Identities.FirstOrDefault()?.Claims;
            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Google login failed. Email not found.");
            }

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // Create a new user if it doesn't exist
                var newUser = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true  // Consider setting this to true since it's coming from Google
                };

                var createResult = await userManager.CreateAsync(newUser);
                if (!createResult.Succeeded)
                {
                    return BadRequest("Failed to create a new user from Google account.");
                }

                // Assign default role(s) if needed
                await userManager.AddToRoleAsync(newUser, "Customer"); // Example role
                user = newUser;
            }

            // Generate JWT token for the user
            var roles = await userManager.GetRolesAsync(user);
            var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Google login successful!",
                data = new CustomerResponseDTO
                {
                    JwtToken = jwtToken
                }
            };

            return Ok(response);
        }

    }
}
