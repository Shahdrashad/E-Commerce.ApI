using E_commerce.Core.OrderAggregate;

namespace E_Commerce.ApI.DTOS
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int DeliverMethodId { get; set; }
        public AddressDto ShipingAdress { get; set; }
    }

}
