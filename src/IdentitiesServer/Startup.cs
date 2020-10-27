using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentitiesServer.Data.Identities;
using IdentitiesServer.Identities.Contracts;
using IdentitiesServer.Identities.Cryptography;
using IdentitiesServer.Identities.Managers;
using IdentitiesServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace IdentitiesServer
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration config) => _configuration = config;
        private const string AllowSpecificOrigins = "_allowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
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

            
            services.AddDbContext<IdentitiesContext>(options => { options.UseSqlServer(builder.ConnectionString); });

            services
                .AddIdentityCore<User>(opt => { opt.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<IdentitiesContext>()
                .AddUserManager<UserManager>()
                .AddUserStore<UsersRepo>()
                .AddSignInManager<SignInManager>()
                .AddDefaultTokenProviders();
            
            services.AddScoped<UsersRepo>() // So it gets successfully registered in UserManager
                .AddScoped<IUserRepo, UsersRepo>()
                .AddScoped<IAuthManager, AuthManager>()
                .AddScoped<IHasher, Hasher>();
            services
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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