using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using UsersServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UsersServer.Contracts;
using UsersServer.Cryptography;
using UsersServer.Cryptography.Services;
using UsersServer.Data;
using UsersServer.Managers;

namespace UsersServer
{
    public class Startup
    {
        /// <summary>
        /// This allows us to access our secrets hosted in the System.
        /// </summary>
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration config) => _configuration = config;

        private const string AllowSpecificOrigins = "_allowSpecificOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new SqlConnectionStringBuilder
            {
                ConnectionString = "Server=localhost,1433\\Catalog=sql1;Database=sql1;",
                UserID = _configuration["UserID"],
                Password = _configuration["Password"]
            };

            // Policies
            services
                .AddCors(options =>
                {
                    options.AddPolicy(AllowSpecificOrigins,
                        b => { b.WithOrigins("http://localhost:8080", "http://127.0.0.1:8080"); });
                })
                .AddControllers();
            services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();
            services.AddScoped<IJwtFactory, JwtFactory>();

            // User management 
            services
                .AddIdentityCore<User>(opt => { opt.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<UserDbContext>()
                
                .AddUserManager<UserManager>()
                .AddUserStore<UsersRepo>()
                .AddSignInManager<SignInManager>();
            // Database Context and Swagger
            services
                .AddDbContext<UserDbContext>(options => { options.UseSqlServer(builder.ConnectionString); })
                // Inject an implementation of ISwaggerProvider with defaulted settings applied
                .AddSwaggerGen()
                .ConfigureSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Commands And Snippets API",
                        Version = "v1",
                        Contact = new OpenApiContact {Email = "geral@bigmonte.com"},
                        Description = "Useful commands and Snippets API"
                    });

                    /*var secScheme = new OpenApiSecurityScheme();
                    secScheme.Description = "JWT Authorization header";
                    options.AddSecurityDefinition("Bearer", secScheme);

                    var secRequirement = new OpenApiSecurityRequirement();
                    secRequirement.Add(secScheme, new[] {"Bearer"});
                    options.AddSecurityRequirement(secRequirement);*/
                });
            services.TryAddSingleton<ISystemClock, SystemClock>();
            // Registering 'services' and Authentication, Cookies, JWT
            services
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddScoped<UsersRepo>() // So it gets successfully registered in UserManager
                .AddScoped<IUserRepo, UsersRepo>()
                .AddScoped<IAuthManager, AuthManager>()
                .AddScoped<IHasher, Hasher>();
            
            
            // Register the ConfigurationBuilder instance of AuthSettings
            var authSettings = _configuration.GetSection(nameof(AuthSettings));
            services.Configure<AuthSettings>(authSettings);

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings[nameof(AuthSettings.SecretKey)]));

            // jwt wire up
            // Get options from app settings
            var jwtAppSettingOptions = _configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;

                configureOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });

            // add identity
            var identityBuilder = services.AddIdentityCore<User>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });

            identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(IdentityRole), identityBuilder.Services);
            identityBuilder.AddEntityFrameworkStores<UserDbContext>().AddDefaultTokenProviders();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }         

            // Cors documentation
            // https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-3.1

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger()
                // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
                .UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint("/swagger/v1/swagger.json", "Commands And Snippets API");
                })
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseCors(AllowSpecificOrigins)
                .UseEndpoints(endpoints =>
                {
                    // Controller services, registered in the ConfigureServices method, as endpoints in the Request Pipeline. 
                    endpoints.MapControllers();
                });

        }
    }
}