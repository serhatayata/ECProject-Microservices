using Core.Entities;

namespace EC.Services.PhotoStockAPI.Dtos
{
    public class PhotoAddDto:IDto
    {
        public IFormFile Photo { get; set; }
        public int PhotoType { get; set; }
        public int EntityId { get; set; }
        public bool WithWatermark { get; set; }
    }

    public enum PhotoTypes
    {
        Product=1,
        Category=2
    }

}
