namespace E_Commerce.ApI.DTOS
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public List<BasketItemDto> items {  get; set; }
        public string PaymentIntendId { get; set; }
        public string ClientsSecret { get; set; }

        
    }
}
