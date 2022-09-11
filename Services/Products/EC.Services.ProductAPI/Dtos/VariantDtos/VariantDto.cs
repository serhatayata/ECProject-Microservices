using Core.Entities;

namespace EC.Services.ProductAPI.Dtos.VariantDtos
{
    public class VariantDto:IDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }
}
