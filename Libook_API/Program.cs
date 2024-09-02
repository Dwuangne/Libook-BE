
using Libook_API.Data;
using Libook_API.Repositories.AuthorRepo;
using Libook_API.Repositories.BookImageRepo;
using Libook_API.Repositories.BookRepo;
using Libook_API.Repositories.CategoryRepo;
using Libook_API.Repositories.CommentImageRepo;
using Libook_API.Repositories.CommentRepo;
using Libook_API.Repositories.OrderStatusRepo;
using Libook_API.Repositories.ConversationRepo;
using Libook_API.Service.AuthorService;
using Libook_API.Service.AuthService;
using Libook_API.Service.BookImageService;
using Libook_API.Service.BookService;
using Libook_API.Service.CategoryService;
using Libook_API.Service.CommentImageService;
using Libook_API.Service.CommentService;
using Libook_API.Service.ConversationService;
using Libook_API.Service.DistrictService;
using Libook_API.Service.EmailService;
using Libook_API.Service.MessageService;
using Libook_API.Service.OrderDetailService;
using Libook_API.Service.OrderInfoService;
using Libook_API.Service.OrderService;
using Libook_API.Service.OrderStatusService;
using Libook_API.Service.ParticipantService;
using Libook_API.Service.ProvinceService;
using Libook_API.Service.SupplierService;
using Libook_API.Service.VoucherActivedService;
using Libook_API.Service.VoucherService;
using Libook_API.Service.WardService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Libook_API.Repositories.DistrictRepo;
using Libook_API.Repositories.MessageRepo;
using Libook_API.Repositories.OrderRepo;
using Libook_API.Repositories.OrderDetailRepo;
using Libook_API.Repositories.OrderInfoRepo;
using Libook_API.Repositories.ParticipantRepo;
using Libook_API.Repositories.ProvinceRepo;
using Libook_API.Repositories.SupplierRepo;
using Libook_API.Repositories.VoucherRepo;
using Libook_API.Repositories.VoucherActivedRepo;
using Libook_API.Repositories.WardRepo;
using Libook_API.Mapping;

namespace Libook_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Libook API", Version = "v1" });
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                options.AddSecurityRequirement
                (new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = JwtBearerDefaults.AuthenticationScheme
                                },
                                Scheme = "Oauth2",
                                Name = JwtBearerDefaults.AuthenticationScheme,
                                In = ParameterLocation.Header
                            },
                            new List<string>()
                        }
                    }
                );
            });

            builder.Services.AddDbContext<LibookDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LibookConnectionString")));

            builder.Services.AddDbContext<LibookAuthDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("LibookAuthConnectionString")));

            //Add Services
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IEmailService, SendGridEmailService>();

            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IBookImageService, BookImageService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICommentService, CommentService>();

            builder.Services.AddScoped<ICommentImageService, CommentImageService>();
            builder.Services.AddScoped<IConversationService, ConversationService>();
            builder.Services.AddScoped<IDistrictService, DistrictService>();
            builder.Services.AddScoped<IMessageService, MessageService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
            builder.Services.AddScoped<IOrderInfoService, OrderInfoService>();
            builder.Services.AddScoped<IOrderStatusService, OrderStatusService>();
            builder.Services.AddScoped<IParticipantService, ParticipantService>();
            builder.Services.AddScoped<IProvinceService, ProvinceService>();

            builder.Services.AddScoped<ISupplierService, SupplierService>();
            builder.Services.AddScoped<IVoucherService, VoucherService>();
            builder.Services.AddScoped<IVoucherActivedService, VoucherActivedService>();
            builder.Services.AddScoped<IWardService, WardService>();

            //Add Repositories
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IBookImageRepository, BookImageRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();

            builder.Services.AddScoped<ICommentImageRepository, CommentImageRepository>();
            builder.Services.AddScoped<IConverstationRepository, ConverstationRepository>();
            builder.Services.AddScoped<IDistrictRepository, DistricRepository>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            builder.Services.AddScoped<IOrderInfoRepository, OrderInfoRepository>();
            builder.Services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
            builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
            builder.Services.AddScoped<IProvinceRepository, ProvinceRepository>();

            builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
            builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
            builder.Services.AddScoped<IVoucherActivedRepository, VoucherActivedRepository>();
            builder.Services.AddScoped<IWardRepository, WardRepository>();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

            builder.Services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Libook")
                .AddEntityFrameworkStores<LibookAuthDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
                options.SignIn.RequireConfirmedEmail = true;
            });

            //JWT validation for ASP.NET Core web application
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            })
            .AddGoogle(googleOptions =>
            {
                // Đọc thông tin Authentication:Google từ appsettings.json
                IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");

                // Thiết lập ClientID và ClientSecret để truy cập API google
                googleOptions.ClientId = googleAuthNSection["ClientId"];
                googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];
                // Cấu hình Url callback lại từ Google (không thiết lập thì mặc định là /signin-google)
                googleOptions.CallbackPath = "/api/Auth/LoginbyGoogle";

            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
                RequestPath = "/Images"
            });
            app.MapControllers();

            app.Run();
        }
    }
}
