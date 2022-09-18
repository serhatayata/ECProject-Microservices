using Core.Entities;

namespace EC.Services.PhotoStockAPI.Entities
{
    public class Photo:IEntity
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int EntityId { get; set; }
        public string Url { get; set; }
    }

    public enum EnumPhotoTypes
    {
        Category=1,
        Product=2
    }
}
