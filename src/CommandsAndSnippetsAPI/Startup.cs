using System;
using CommandsAndSnippetsAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using UsersServer;

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
                       
            // horrible approach TODO use autofac and eventually with presenters 
            services.AddAuthServerServices(builder, AllowSpecificOrigins, _configuration);

            // Policies
            services
                .AddCors(options =>
                {
                    options.AddPolicy(AllowSpecificOrigins,
                        b => { b.WithOrigins("http://localhost:8080", "http://127.0.0.1:8080").AllowAnyHeader()
                            .AllowAnyMethod(); });
                })
                .AddControllers();


            // Database Context and Swagger
            services
                .AddDbContext<ApiDataContext>(options =>
                {
                    options.UseSqlServer(builder.ConnectionString);
                        

                })
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddScoped<ICommandsApiRepo, ApiRepo>()
                .AddScoped<ISnippetsAPIRepo, ApiRepo>();
 
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // horrible approach TODO use autofac and eventually with presenters 
            app.ConfigureApiWithUsers(env, AllowSpecificOrigins);
        }
    }
}