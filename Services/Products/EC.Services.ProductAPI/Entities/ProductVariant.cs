using Core.Entities;

namespace EC.Services.ProductAPI.Entities
{
    public class ProductVariant:IEntity
    {
        public string ProductId { get; set; }
        public string VariantId { get; set; }
    }
}
