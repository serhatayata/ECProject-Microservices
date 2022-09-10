using Core.Entities;

namespace EC.Services.ProductAPI.Dtos.ProductDtos
{
    public class ProductUpdateDto:IDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Line { get; set; }
        public int CategoryId { get; set; }
    }
}
