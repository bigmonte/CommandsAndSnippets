
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace  CommandsAndSnippets2;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.


        builder.Services.Configure<CookiePolicyOptions>(options =>
        {
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });


        builder.Services.AddLogging(builder =>
        {
            builder.AddSimpleConsole();
        });

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.ConfigureSwagger();
        }



        builder.Services.AddControllersWithViews()
            .AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });


        builder.Services.AddAuthServerServices(builder.Configuration, builder.Environment);
        builder.Services.PreparePostgresqlDatabasePool(builder.Configuration, builder.Environment);


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
            
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeLiturgy.Views v1");
            });

        }
        
        

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();


        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");

        app.MapFallbackToFile("index.html");

        app.Run();
    }
}

