using System;
using BlueWest.Cryptography;
using CodeLiturgy.Startup.Application;
using CommandsAndSnippets2.Application;
using CommandsAndSnippets2.Application.Users;
using CommandsAndSnippets2.Auth;
using CommandsAndSnippets2.Domain;
using CommandsAndSnippets2.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using CommandsAndSnippets2.Application.Session;
using CommandsAndSnippets2.Data;

namespace CommandsAndSnippets2
{
	public static class StartupExtensions
	{
        public static IServiceCollection ConfigureSwagger(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSwaggerGen(options =>
                {
                    options.SchemaFilter<SwaggerEnumSchemaFilter>();
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "CodeRecipesApp",
                        Version = "v1"
                    });

                    // Set the comments path for the Swagger JSON and UI.
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                    options.IncludeXmlComments(xmlPath);

                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description =
                            "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                    });

                });
        }

        private static string GetDbConnectionString(this IConfiguration configurationRoot, string key)
        {
            var startupMode = configurationRoot["mode"];

            if (!string.IsNullOrEmpty(startupMode))
            {
                var config = configurationRoot.GetSection($"ConnectionString:{startupMode}")[key];
                return config;
            }
            return String.Empty;

        }


        internal static IServiceCollection AddAuthServerServices(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddSession(options =>
            {
                options.Cookie.Domain = SessionConstants.CookieDomain;
                options.Cookie.HttpOnly = true;
                options.IdleTimeout = TimeSpan.FromHours(8);
            });

            services
                .AddScoped<UserRepository>()
                .AddScoped<IUserManager, ApplicationUserManager>()
                .AddScoped<IAuthManager, AuthManager>()
                .AddScoped<IHasher, Hasher>();

            // Database Context and Swagger


            // Register the ConfigurationBuilder instance of AuthSettings
            var authSettings = configuration.GetSection(nameof(AuthSettings));
            services.Configure<AuthSettings>(authSettings);


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                });



            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy(SessionConstants.ApiNamePolicy,
                    policy => policy.RequireClaim(Constants.JwtClaimIdentifiers.Rol,
                        Constants.JwtClaims.ApiAccess));
                options.AddPolicy(SessionConstants.CookieNamePolicy, policy =>
                {
                    policy.RequireClaim(Constants.CookieClaims.CookieAccess);
                });
            });

            // add identity
            var identityBuilder = services.AddIdentityCore<ApplicationUser>(o =>
            {
                o.User.RequireUniqueEmail = true;

                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            })
                .AddUserManager<ApplicationUserManager>()
                .AddUserStore<UserRepository>();

            identityBuilder =
                new IdentityBuilder(identityBuilder.UserType, typeof(ApplicationRole), identityBuilder.Services);
            identityBuilder
                .AddEntityFrameworkStores<ApplicationUserDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }


        private static MySqlServerVersion GetMySqlServerVersion(int major, int minor, int build) => new (new Version(major, minor, build));

        private static DbContextOptionsBuilder UseSqlConfiguration(this DbContextOptionsBuilder builder, IConfiguration configuration)
        {
            var connString = configuration.GetDbConnectionString("db");
            var sqlVersion = GetMySqlServerVersion(8, 0, 11);
            builder
                .UseMySql(
                    connString, 
                    sqlVersion)
                .UseMySql(sqlVersion,
                    builder =>
                    {
                        builder.EnableRetryOnFailure(6, TimeSpan.FromSeconds(3), null);
                    });
            return builder;
        }


        /// <summary>
        /// Setup database Contexts
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static IServiceCollection PreparePostgresqlDatabasePool(this IServiceCollection serviceCollection,
            IConfiguration configuration, IWebHostEnvironment environment)
        {
            return serviceCollection
            .AddDbContextPool<ApplicationUserDbContext>(options =>
                options.UseSqlConfiguration(configuration))
            .AddDbContextPool<SnippetsDbContext>(options =>
                options.UseSqlConfiguration(configuration));
            
        }

    }
}

