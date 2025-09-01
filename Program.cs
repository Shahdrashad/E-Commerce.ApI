using E_commerce.Core.Contexts;
using E_commerce.Core.Entities;
using E_commerce.Core.Repository;
using E_commerce.Repository;
using E_commerce.Repository.Data;
using E_Commerce.ApI.Helper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using E_Commerce.ApI.Extension;
using E_commerce.Services;
using E_commerce.Repository.Identity;
using E_commerce.Core.Services;
using Stripe;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.ApI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region ConfigureServices
            // Add services to the container.
            
            builder.Services.AddControllers();
            builder.Services.AddScoped<IDbInitializer, DbContextSeed>();
            //builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            //builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
             });
           builder.Services.AddSingleton<IConnectionMultiplexer>(
                _ => ConnectionMultiplexer.Connect( builder.Configuration.GetConnectionString(
                "Redis")!));
            builder.Services.AddApplicationServices();
           builder.Services.AddIdentityServicesExtension();

            #endregion


            var app = builder.Build();
            #region Updata database

            // DbContext _dbContext = new dbContext();
            //await _dbContext.Database.MigrateAsync();

            using var Scope = app.Services.CreateScope();
            var Services = Scope.ServiceProvider;
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {

                var DbContext = Services.GetRequiredService<AppDbContext>();
                await DbContext.Database.MigrateAsync();
                // await DbContextSeed.SeedAsync(DbContext);
                var IdentityDbContext =Services.GetRequiredService<AppIdentityDbContext>();
                await IdentityDbContext.Database.MigrateAsync();
                var UserManger = Services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(UserManger);

            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An Error During Appling The Migration");

            }


            #endregion

            #region Build project
            
           await SeedDataAsync(app);

            #endregion
            #region Kestral Pipliens

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            #endregion




            app.Run();
            async Task SeedDataAsync(WebApplication app)
            {
                using var Scope = app.Services.CreateScope();
                var dbInitializer = Scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                await dbInitializer.InitializeAsync();
            }
        }
      
    }
}