namespace EC.Services.PaymentAPI.ApiDtos
{
    public class ProductApiDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
