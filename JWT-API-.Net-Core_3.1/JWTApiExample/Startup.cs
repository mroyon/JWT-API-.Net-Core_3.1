using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BDO.DataAccessObjects.SecurityModule;
using JWTApiExample.CustomIdentityManagers;
using JWTApiExample.CustomStores;
using JWTApiExample.Descripter;
using JWTApiExample.Filters;
using JWTApiExample.InAppResources;
using JWTApiExample.Providers;
using JWTApiExample.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace JWTApiExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallServicesInAssembly(Configuration);

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
           

            services.AddAuthentication(options =>
            {
                //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                 .AddJwtBearer(options =>
                 {
                     options.SaveToken = true;
                     options.RequireHttpsMetadata = false;
                     options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidAudience = Configuration["Jwt:Issuer"],
                         ValidIssuer = Configuration["Jwt:Audience"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]))
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
                options.AddPolicy("apipolicy", b =>
                {
                    b.RequireAuthenticatedUser();
                    b.AuthenticationSchemes = new List<string> { JwtBearerDefaults.AuthenticationScheme };
                });
            });

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


            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper mapper)
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseRouting();
            app.UseResponseCaching();
            app.UseCors("AllowAllOrigins");

            app.UseHsts(hsts => hsts.MaxAge(365).IncludeSubdomains());

            app.UseCsp(opts => opts
                .BlockAllMixedContent()
                .StyleSources(s => s.Self())
                .StyleSources(s => s.UnsafeInline())
                .FontSources(s => s.Self())
                .FrameAncestors(s => s.Self())
                .FrameAncestors(s => s.CustomSources(
                   "https://localhost:44384", "https://localhost", "http://localhost", "http://localhost:44384", "https://localhost:5000", "http://localhost:5000")
                 )
                .ImageSources(imageSrc => imageSrc.Self())
                .ImageSources(imageSrc => imageSrc.CustomSources("http://www.gravatar.com"))
                .ImageSources(imageSrc => imageSrc.CustomSources("data:"))
                .ScriptSources(s => s.Self())
                .ScriptSources(s => s.UnsafeInline())
            );



            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseSession();
            //Linux hosting as service


            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
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
