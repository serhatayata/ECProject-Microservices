using Core.Entities;

namespace EC.Services.ProductAPI.Dtos.ProductVariantDtos
{
    public class ProductVariantGetDto:IDto
    {
        public string ProductId { get; set; }
        public string VariantId { get; set; }
    }
}
