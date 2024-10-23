using Libook_API.Data;
using Libook_API.Messages;
using Libook_API.Repositories.AuthorRepo;
using Libook_API.Repositories.BookImageRepo;
using Libook_API.Repositories.BookRepo;
using Libook_API.Repositories.CategoryRepo;
using Libook_API.Repositories.CommentImageRepo;
using Libook_API.Repositories.CommentRepo;
using Libook_API.Repositories.OrderStatusRepo;
using Libook_API.Repositories.ConversationRepo;
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
using Libook_API.Repositories.PaymentOrderRepo;
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
using Libook_API.Service.PaymentOrderService;
using Libook_API.Service.ProvinceService;
using Libook_API.Service.SupplierService;
using Libook_API.Service.VoucherActivedService;
using Libook_API.Service.VoucherService;
using Libook_API.Service.WardService;
using Libook_API.Service.CheckOutService;
using Libook_API.Mapping;
using Libook_API.Middlewares;
using Net.payOS;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using Scrutor;
using Microsoft.AspNetCore.Antiforgery;
using Libook_API.Service.CrawlDataService;

namespace Libook_API
{
    public class Program
{
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Setup Serilog logging
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .MinimumLevel.Warning()
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            //builder.Logging.AddConsole(); // Ghi log ra console
            //builder.Logging.SetMinimumLevel(LogLevel.Debug); // Ghi log ở mức debug

            // Add services to the container
            builder.Services.AddSignalR();
            builder.Services.AddControllers();
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
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
        });
            });

            // Configure DBContexts
            builder.Services.AddDbContext<LibookDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LibookConnectionString")));
            builder.Services.AddDbContext<LibookAuthDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LibookAuthConnectionString")));

            // Register PayOS service
            builder.Services.AddScoped<PayOS>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                return new PayOS(
                    configuration["CheckOutPayOs:Environment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find environment"),
                    configuration["CheckOutPayOs:Environment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find environment"),
                    configuration["CheckOutPayOs:Environment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find environment")
                );
            });

            builder.Services.AddHttpClient<ICrawlDataService, CrawlDataService>();

            // Register Services and Repositories
            builder.Services.Scan(scan => scan
                .FromAssemblyOf<ITokenService>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            builder.Services.Scan(scan => scan
                .FromAssemblyOf<IAuthorRepository>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            // AutoMapper configuration
            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

            // Identity configuration
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

            // JWT Authentication setup
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            // CORS configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();

                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseHttpsRedirection();

            // Static files setup
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
                RequestPath = "/Images"
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Template")),
                RequestPath = "/Template"
            });

            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapHub<ChatHub>("/chathub");

            app.MapGet("/", () => "Welcome to Libook API!");

            app.Run();
        }
    }
}
