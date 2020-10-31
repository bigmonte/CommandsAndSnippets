using System;
using CommandsAndSnippetsAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace CommandsAndSnippetsAPI
{
    public class Startup
    {
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
                        b =>
                        {
                            b.WithOrigins("http://localhost:8080", "http://127.0.0.1:8080", "http://localhost:4200",
                                "http://127.0.0.1:4200");
                        });
                })
                .AddControllers()
                .AddNewtonsoftJson(s =>
                {
                    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
            

            // Database Context and Swagger
            services
                .AddDbContext<ApiDataContext>(options =>
                {
                    options.UseSqlServer(builder.ConnectionString)
                        .LogTo(Console.WriteLine, LogLevel.Information);
                })
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

            // Registering 'services' and Authentication, Cookies, JWT
            services
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddScoped<ICommandsApiRepo, ApiRepo>()
                .AddScoped<ISnippetsAPIRepo, ApiRepo>();

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
                .UseCors(AllowSpecificOrigins)
                .UseEndpoints(endpoints =>
                {
                    // Controller services, registered in the ConfigureServices method, as endpoints in the Request Pipeline. 
                    endpoints.MapControllers();
                });
        }
    }
}