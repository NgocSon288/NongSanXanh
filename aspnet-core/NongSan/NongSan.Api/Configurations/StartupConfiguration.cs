using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NongSan.Application.AppRoles;
using NongSan.Application.AppUserRoles;
using NongSan.Application.AppUsers;
using NongSan.Application.Common;
using NongSan.Data.EF;
using NongSan.Data.Entities;
using NongSan.Utilities.Constants;
using NongSan.Utilities.Helpers;
using NongSan.Utilities.Options;
using NongSan.ViewModels.AppUsers.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NongSan.Api.Configurations
{
    public static class StartupConfiguration
    {
        public static void ServicesConfig(this IServiceCollection services)
        {
            services.AddTransient<IAppUserService, AppUserService>();
            services.AddTransient<IAppRoleService, AppRoleService>();
            services.AddTransient<IAppUserRoleService, AppUserRoleService>();

            services.AddTransient<IFileStorageService, FileStorageService>();
            services.AddTransient<IMailHelperService, MailHelperService>();
        } 

        public static void OptionServicesConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection(SystemConstants.AppSettings.MailSettings));
        }

        public static void ControllerConfig(this IServiceCollection services)
        {
            services.AddControllers()
                // Fluent Validation
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AppUserLoginRequestValidator>());
        }

        public static void IdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(SystemConstants.NongSanConnectionString));
            });

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false; // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true; // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false; // Xác thực số điện thoại

            });
        }

        public static void SwaggerConfig(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger Nông Sản Xanh Solution", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                    Enter 'Bearer' [space] and then your token in the text input below.
                    \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                                {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            string issuer = Configuration.GetValue<string>(SystemConstants.AppSettings.TokensIssuer);
            string signingKey = Configuration.GetValue<string>(SystemConstants.AppSettings.TokensKey);
            byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = System.TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                };
            });
        }

        public static void CorsConfig(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                    });
            });
        }
    }
}
