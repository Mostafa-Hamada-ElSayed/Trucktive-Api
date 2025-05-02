
using Microsoft.AspNetCore.Identity;
using Trucktive.APIs.Extensions;
using Trucktive.Core.Entities;
using Trucktive.Repositories._Identity;

namespace Trucktive.APIs
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            
            builder.Services.AddOpenApi();

            builder.Services.AddApplicationServices(builder.Configuration);

            var app = builder.Build();

            #region Apply All Pending Migrations [Update Database] and Data Seeding

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            var logger = loggerFactory.CreateLogger<Program>();

            try
            {
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await ApplicationIdentityDbContextSeed.SeedUserAsync(userManager, roleManager);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Error Has Been occurred during Seeding Data.");
            }

            #endregion

            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
            //}

            app.UseHttpsRedirection();

            app.UseCors(); // Use default CORS policy

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
