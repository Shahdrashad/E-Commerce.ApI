 using E_Commerce.ApI.Helper;
using E_commerce.Core.Repository;
using E_commerce.Repository;
using StackExchange.Redis;
using E_commerce.Core.Services;
using E_commerce.Services;
using Stripe;

namespace E_Commerce.ApI.Extension
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
           // Services.AddScoped<IPaymentService, IPaymentService>();
            Services.AddScoped(typeof(IOrderService), typeof(OrderService));
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //  builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //  builder.Services.AddAutoMapper(m=>m.AddProfile(new MappingProfiles()));
            Services.AddAutoMapper(typeof(MappingProfiles));
            
            return Services;
        }
    }
}
