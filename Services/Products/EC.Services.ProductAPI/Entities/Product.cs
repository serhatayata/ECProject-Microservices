using Core.Entities;

namespace EC.Services.ProductAPI.Entities
{
    public class Product:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public int Line { get; set; }
        public int CategoryId { get; set; }
    }
}
