using Core.Entities;

namespace EC.Services.ProductAPI.Dtos.ProductDtos
{
    public class ProductDto:IDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public int Line { get; set; }
        public int CategoryId { get; set; }

    }
}
