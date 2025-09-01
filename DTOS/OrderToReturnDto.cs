using E_commerce.Core.OrderAggregate;

namespace E_Commerce.ApI.DTOS
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; }
        public Address ShippingAddress { get; set; }
        // public int DeliveryMethodId { get; set; }//fk
        public string DeliveryMethod { get; set; }//Name
        public decimal DeliveryMethodCOST { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
        public decimal SubTotal { get; set; }//price of pro*quantity
        public decimal Total {  get; set; }
        public string paymentIntendId { get; set; }
    }
}
