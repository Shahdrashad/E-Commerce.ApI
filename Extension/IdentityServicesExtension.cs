using E_commerce.Core.Entities;
using E_commerce.Repository.Identity;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.ApI.Extension
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServicesExtension(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
               .AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddAuthentication();//UserManger - SigninManger-RoleManger
            return services;

        }

    }
}
