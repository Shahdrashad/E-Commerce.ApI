using E_commerce.Core.OrderAggregate;

namespace E_Commerce.ApI.DTOS
{
    public class OrderItemDto
    {
        public ProductItemOrder Product {  get; set; }
        public int Quantity {  get; set; }
        public decimal Price { get; set; }

    }
}
