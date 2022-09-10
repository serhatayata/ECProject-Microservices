using Core.Entities;

namespace EC.Services.ProductAPI.Entities
{
    public class Stock:IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
