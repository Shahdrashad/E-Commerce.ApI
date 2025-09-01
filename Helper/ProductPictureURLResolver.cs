using AutoMapper;
using AutoMapper.Execution;
using E_commerce.Core.Entities;
using E_Commerce.ApI.DTOS;

namespace E_Commerce.ApI.Helper
{
    public class ProductPictureURLResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureURLResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            
                return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";
            return string.Empty ;

        }
    }
}
