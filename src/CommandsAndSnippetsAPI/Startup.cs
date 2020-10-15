
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
using CommandsAndSnippetsAPI.Data.Identities;
using Newtonsoft.Json.Serialization;

/*

                                    Models, DTOs, Repository and Data Access

    Model               Represents the internal domain data of our application (M in MVC)
    
    DTOs                Are the representations of our Domain Models to our external consumers,
                        meaning that we don't expose internal implementation detail (Models) to
                        external concerns. This has multiple benefits.
    
    Data Access         Takes our Models and represents (or mediates) them to a specific persistent
    (aka DB Context)    layer (e.g SQL Server, etc). Technology-specific term taken from Entity
                        Framework Core (EFC).
    
    Repository          Provides a technology agnostic (or persistent ignorant) view of our permanent
                        view of our permanently stored data to our application. 

    Conclusion          Decoupling implementational detail from the interface or contract we want to
                        provide to consumers.
    

                                                Why Decoupling?
    
    Security            We may not want to expose potential sensitive data contained in our implementation
                        (ie. Model) to our external consumers. Providing an external representation 
                        (e.g., a DTO) with sensitive information removed addresses this
    
    Change Agility      Separating our interface - which should remain consistent so as not to break our
                        "contract" with our consumers, means we can then change our implementation detail
                        without impacting that interface. We then have the confidence to react quickly
                        to market demans without fear of breaking existing agreements. This is shown
                        in our use of dependency injection and our repository

*/


namespace CommandsAndSnippetsAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration config) => _configuration = config;
        private readonly string _allowSpecificOrigins = "_allowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application,
        // visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
        {
            options.AddPolicy(name: _allowSpecificOrigins,
                              builder =>
                              {
                                  builder.WithOrigins("http://localhost:8080",
                                                      "http://127.0.0.1:8080");
                              });
        });

            /*                    Service Lifetimes
             
             AddTransient        A service is created each time it is requested from the Service container
             
             AddScoped           A service is created once per client request (connection)
             
             Add Singleton       A service is created once and reused
             
             More resources:
             https://devblogs.microsoft.com/cesardelatorre/comparing-asp-net-core-ioc-service-life-times-and-autofac-ioc-instance-scopes/
             */

            var builder = new SqlConnectionStringBuilder
            {
                ConnectionString = "Server=localhost,1433\\Catalog=sql1;Database=sql1;",
                UserID = _configuration["UserID"],
                Password = _configuration["Password"]
            };
            
            // UserID = _configuration["User"] retrieves the username on mac 
            
            services.AddDbContext<ApiDataContext>(options =>
            {
                options.UseSqlServer(builder.ConnectionString);
              
            }).AddDbContext<IdentitiesContext>(options =>
                {
                    options.UseSqlServer(builder.ConnectionString);
                });
            
            /*
             
    1. Register services to enable the use of "Controllers" throughout our application.
    
    2. We "MapControllers" to our endpoints. This means we make use of the Controller services,
    registered in the ConfigureServices method, as endpoints in the Request Pipeline. 
           
            */

            services
                .AddControllers()
                .AddNewtonsoftJson(s =>
                {
                    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddScoped<ICommandsAndSnippetsAPIRepo, ApiRepo>()
                .AddScoped<ISnippetsAPIRepo, ApiRepo>()
                .AddScoped<IUserRepo, UsersRepo>();



        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Cors documentation
            // https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-3.1

            app
                .UseRouting()
                .UseCors(_allowSpecificOrigins)
                .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
