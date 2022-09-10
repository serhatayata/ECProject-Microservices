using Core.Entities;

namespace EC.Services.ProductAPI.Entities
{
    public class Variant:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
