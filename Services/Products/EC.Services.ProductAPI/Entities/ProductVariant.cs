using Core.Entities;

namespace EC.Services.ProductAPI.Entities
{
    public class ProductVariant:IEntity
    {
        public int ProductId { get; set; }
        public int VariantId { get; set; }
    }
}
