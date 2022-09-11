using Core.Entities;

namespace EC.Services.ProductAPI.Dtos.ProductVariantDtos
{
    public class ProductVariantDto:IDto
    {
        public string ProductId { get; set; }
        public string VariantId { get; set; }
    }
}
