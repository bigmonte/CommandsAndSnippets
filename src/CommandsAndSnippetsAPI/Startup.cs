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
using CommandsAndSnippetsAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;


namespace CommandsAndSnippetsAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration config) => _configuration = config;
        private readonly string _allowSpecificOrigins = "_allowSpecificOrigins";
 
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options => { options.AddPolicy(name: _allowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:8080", "http://127.0.0.1:8080");
                }); });

   

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
    
                2. We "MapControllers" to our endpoints. This means we make use of the 
                Controller services, registered in the ConfigureServices method, as endpoints 
                in the Request Pipeline. 
           
            */

            services
                .AddControllers()
                .AddNewtonsoftJson(s =>
                {
                    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

                services
                    .AddIdentity<User, IdentityRole>(options =>
                    {
                        options.User.RequireUniqueEmail = false;
                    })
                    .AddEntityFrameworkStores<IdentitiesContext>()
                    .AddDefaultTokenProviders();
                services
                .AddIdentityCore<User>(opt =>
                {
                    opt.User.RequireUniqueEmail = true;
                })
                .AddSignInManager<SignInManager>();
                services
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddScoped<ICommandsAndSnippetsAPIRepo, ApiRepo>()
                .AddScoped<ISnippetsAPIRepo, ApiRepo>()
                .AddScoped<IUserRepo, UsersRepo>()
                .AddScoped<ILoginManager, LoginManager>()
                .Replace(new ServiceDescriptor(
                    serviceType: typeof(IPasswordHasher<User>),
                    implementationType: typeof(Hasher),
                    ServiceLifetime.Scoped))
                .AddScoped<IHasher, Hasher>()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie()
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateActor = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "Test Issuer",
                        ValidAudience = "Test Sign Key",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TestSingKey"))
                    };
                });
                services.Replace(new ServiceDescriptor(
                    serviceType: typeof(IPasswordHasher<IdentityUser>),
                    implementationType: typeof(Hasher),
                    ServiceLifetime.Scoped));

                services.Replace(new ServiceDescriptor(
                    serviceType: typeof(IUserStore<IdentityUser>),
                    implementationType: typeof(IUserRepo),
                    ServiceLifetime.Scoped));

                services.Replace(new ServiceDescriptor(
                    serviceType: typeof(SignInManager<IdentityUser>),
                    implementationType: typeof(SignInManager),
                    ServiceLifetime.Scoped));

                services.Replace(new ServiceDescriptor(
                    serviceType: typeof(UserManager<IdentityUser>),
                    implementationType: typeof(UserManager),
                    ServiceLifetime.Scoped));



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
