
using AncientAura.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AncientAura.APIs
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AncientAuraDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            //return group of isercieseScope work with liftime scope 
            // Ê»„« «‰ «· stroe dbcontext ‘€«·Â scope › Â —Ã⁄Â«  
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            // return object from storeDbContext .
            var context = services.GetRequiredService<AncientAuraDbContext>();
            try
            {
                // Update DataBase && Apply Migrations
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var loggerfactory = services.GetRequiredService<ILoggerFactory>();
                var logger = loggerfactory.CreateLogger<Program>();
                logger.LogError(ex, "there are problems during apply migrations");
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
