using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Interface;
using ProductService.Infrastructure.Repository;
using ProductService.Infrastructure.ServiceApp;
using System;

namespace ProductService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ProductDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("ProductDatabase")));

            Console.WriteLine(builder.Configuration.GetConnectionString("ProductDatabase"));


            //builder.Services.AddDbContext<ProductDbContext>(options =>
            //    options.UseNpgsql(connectionString));

            var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER");

            Console.WriteLine($"Running in Docker: {isDocker}");

            Console.WriteLine($"Environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ProductServiceApp>();

            // Register MVC controllers and authorization services required by UseAuthorization().
            builder.Services.AddControllers();
            builder.Services.AddAuthorization();

            // Optional: API documentation in development
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ProductService API",
                    Version = "v1"
                });
            });

            var app = builder.Build();

            //using (var scope = app.Services.CreateScope())
            //{
            //    var db = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
            //    db.Database.Migrate();
            //}

            //app.UseHttpsRedirection();
            if (!app.Environment.IsProduction())
            {
                app.UseHttpsRedirection();
            }

                // Enable Swagger in both Development and Production
                if (app.Environment.IsDevelopment() || app.Environment.IsProduction() || app.Environment.IsStaging())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductService API v1");
                    });
                }

            // If you later configure authentication, call app.UseAuthentication() before UseAuthorization().
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}