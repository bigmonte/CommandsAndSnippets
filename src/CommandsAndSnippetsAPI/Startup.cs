using System;
using System.Text;
using CommandsAndSnippetsAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using CommandsAndSnippetsAPI.Data.Identities;
using CommandsAndSnippetsAPI.Identities.Contracts;
using CommandsAndSnippetsAPI.Identities.Cryptography;
using CommandsAndSnippetsAPI.Identities.Managers;
using CommandsAndSnippetsAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CommandsAndSnippetsAPI
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

            services
                .AddCors(options =>
                {
                    options.AddPolicy(AllowSpecificOrigins,
                        b => { b.WithOrigins("http://localhost:8080", "http://127.0.0.1:8080"); });
                })
                .AddControllers();

            services
                .AddIdentityCore<User>(opt => { opt.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<IdentitiesContext>()
                .AddUserManager<UserManager>()
                .AddUserStore<UsersRepo>()
                .AddSignInManager<SignInManager>()
                .AddDefaultTokenProviders();

            services
                .AddDbContext<ApiDataContext>(options => { options.UseSqlServer(builder.ConnectionString); })
                .AddDbContext<IdentitiesContext>(options => { options.UseSqlServer(builder.ConnectionString); })
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

                    var secScheme = new OpenApiSecurityScheme();
                    secScheme.Description = "JWT Authorization header";
                    options.AddSecurityDefinition("Bearer", secScheme);

                    var secRequirement = new OpenApiSecurityRequirement();
                    secRequirement.Add(secScheme, new[] {"Bearer"});
                    options.AddSecurityRequirement(secRequirement);
                });


            services
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddScoped<UsersRepo>() // So it gets successfully registered in UserManager
                .AddScoped<IUserRepo, UsersRepo>()
                .AddScoped<ICommandsApiRepo, ApiRepo>()
                .AddScoped<ISnippetsAPIRepo, ApiRepo>()
                .AddScoped<IAuthManager, AuthManager>()
                .AddScoped<IHasher, Hasher>()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie()
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateActor = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "Test Issuer",
                        ValidAudience = "Test Audience",
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("32CHARSECRETKEYTODOMOVETOSECRETS"))
                    };
                });
        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Our implementation of this method does the following -
        /// Does use Developer Exception page if needed or uses HSTS header in production;
        /// Setting our app to use Swagger (documentation)
        /// Use Routing and Endpoints in which our controllers will get automatically configured;
        /// Configure our app to use allow CORS from the AllowSpecificOrigins;
        /// </summary>
        /// <param name="app">Mechanism to configure application request pipeline</param>
        /// <param name="env">Information about web hosting environment</param>
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
                .UseCors(AllowSpecificOrigins)
                .UseEndpoints(endpoints =>
                {
                    // Controller services, registered in the ConfigureServices method, as endpoints in the Request Pipeline. 
                    endpoints.MapControllers();
                });
        }
    }
}