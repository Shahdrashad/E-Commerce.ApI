using AutoMapper;
using E_commerce.Core.Entities;
using E_commerce.Core.OrderAggregate;
using E_Commerce.ApI.DTOS;
using AdressOrder = E_commerce.Core.OrderAggregate.Address;

namespace E_Commerce.ApI.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureURLResolver>());
            CreateMap<AdressOrder, AddressDto>().ReverseMap();
            CreateMap<AddressDto, AdressOrder>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCOST, o => o.MapFrom(s => s.DeliveryMethod.Cost));
           // CreateMap<OrderItem, OrderItemDto>()
                //.ForMember(d => d.ProductId, o => o.MapFrom(s => s.product.Productid))
                //.ForMember(d => d.ProductName, o => o.MapFrom(s => s.product.ProductName))
                //.ForMember(d => d.picturl, o => o.MapFrom(s => s.product.PictureUrl))
                //.ForMember(d=>d.purl,o=>o.MapFrom<OrderItemPictureURLResolver>());
           CreateMap<CustomerBasketDto,CustomerBasket>().ReverseMap();
                

        
        
        }
       
    }
}
