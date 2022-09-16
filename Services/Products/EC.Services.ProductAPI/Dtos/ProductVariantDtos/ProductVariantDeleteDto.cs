using Core.Entities;

namespace EC.Services.ProductAPI.Dtos.ProductVariantDtos
{
    public class ProductVariantDeleteDto:IDto
    {
        public string ProductId { get; set; }
        public string VariantId { get; set; }
    }
}
