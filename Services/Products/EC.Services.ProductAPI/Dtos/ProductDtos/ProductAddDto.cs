using Core.Entities;

namespace EC.Services.ProductAPI.Dtos.ProductDtos
{
    public class ProductAddDto:IDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
