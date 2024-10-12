using Store.NourhanRageb.APIs.Middlewares;
using Store.NourhanRageb.Repository.Data.Contexts;
using Store.NourhanRageb.Repository;
using Microsoft.EntityFrameworkCore;

namespace Store.NourhanRageb.APIs.Helper
{
    public static class ConfigurateMiddleware
    {
        public static async Task<WebApplication> ConfigurateMiddlewaresAsync(this WebApplication app)
        {

            using var scope = app.Services.CreateScope();

            var Service = scope.ServiceProvider;

            var context = Service.GetRequiredService<StoreDbContext>();

            var LoggerFactory = Service.GetRequiredService<ILoggerFactory>();
            try
            {
                await context.Database.MigrateAsync(); // Update Database
                await StoreDbContextSeed.SeedAsync(context); //Data Seeding دي تخص ال 
            }
            catch (Exception ex)
            {

                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "There are Probems during apply Migrations !!");
            }

            app.UseMiddleware<ExceptionMiddleware>(); // Configure UserDefined Middleware


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
