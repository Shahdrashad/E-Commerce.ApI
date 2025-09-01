using AutoMapper;
using AutoMapper.Execution;
using E_commerce.Core.OrderAggregate;
using E_Commerce.ApI.DTOS;

namespace E_Commerce.ApI.Helper
{
    public class OrderItemPictureURLResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureURLResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.product.PictureUrl))

                return $"{_configuration["ApiBaseUrl"]}{source.product.PictureUrl}";
            return string.Empty;
        }
    }
}
