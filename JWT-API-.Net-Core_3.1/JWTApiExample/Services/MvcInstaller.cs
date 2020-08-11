using BDO.DataAccessObjects.SecurityModule;
using FluentValidation.AspNetCore;
using JWTApiExample.CustomIdentityManagers;
using JWTApiExample.CustomStores;
using JWTApiExample.Descripter;
using JWTApiExample.Filters;
using JWTApiExample.InAppResources;
using JWTApiExample.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebApiOutCache.Models;

namespace JWTApiExample.Services
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            AddLocalizationConfigurations(services);
            services.AddResponseCaching();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowCredentials()
                            .WithOrigins("https://localhost:5000", "https://localhost", "https://localhost:44384", "http://localhost:44384", "http://localhost:5000", "http://localhost")
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });


            services.AddMvc(options =>
            {
                options.Conventions.Add(new AddAuthorizeFiltersControllerConvention());
            });

            services.AddIdentity<owin_userEntity, IdentityRole>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
               .AddErrorDescriber<CusIdentityErrorDescriber>()
               .AddDefaultTokenProviders()
               .AddUserManager<ApplicationUserManager<owin_userEntity>>()
               .AddSignInManager<ApplicationSignInManager<owin_userEntity>>();

            services.AddTransient<SecurityFillerAttribute>();
            services.AddSingleton<IUserStore<owin_userEntity>, CustomUserStore>();



            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);


            services
                .AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
                    //options.Filters.Add<ValidationFilter>();
                })
                .AddFluentValidation(mvcConfiguration => mvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                             .AddJwtBearer(options =>
                             {
                                 options.SaveToken = true;
                                 options.RequireHttpsMetadata = false;
                                 options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                                 {
                                     ValidateIssuer = true,
                                     ValidateAudience = true,
                                     ValidAudience = configuration["Jwt:Issuer"],
                                     ValidIssuer = configuration["Jwt:Audience"],
                                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
                                 };
                                 options.Events = new JwtBearerEvents
                                 {
                                     OnMessageReceived = context =>
                                     {
                                         var accessToken = context.Request.Query["access_token"];

                             // If the request is for our hub...
                             var path = context.HttpContext.Request.Path;
                                         if (!string.IsNullOrEmpty(accessToken) &&
                                             (path.StartsWithSegments("/hubs/chat")))
                                         {
                                 // Read the token out of the query string
                                 context.Token = accessToken;
                                         }
                                         return Task.CompletedTask;
                                     }
                                 };
                             });



            services.AddAuthorization(options =>
            {
                options.AddPolicy("apipolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AuthenticationSchemes = new List<string> { JwtBearerDefaults.AuthenticationScheme };
                });
            });

            //services.AddSingleton<IAuthorizationHandler, Myhandler>();

            services.AddControllersWithViews(options =>
            {

            })
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                        return factory.Create("SharedResource", assemblyName.Name);
                    };
                })
                .AddNewtonsoftJson();


            //for session enable
            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(2);
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.SameSite = SameSiteMode.None;
            //    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            //});
        }

        private static void AddLocalizationConfigurations(IServiceCollection services)
        {
            services.AddSingleton<LocalizeService>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                        {
                            new CultureInfo("en-US"),
                            new CultureInfo("ar-KW")
                        };

                    options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "ar-KW");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;

                    var providerQuery = new LocalizationQueryProvider
                    {
                        QueryParameterName = "ui_locales"
                    };

                    options.RequestCultureProviders.Insert(0, providerQuery);
                });
        }
    }
}
