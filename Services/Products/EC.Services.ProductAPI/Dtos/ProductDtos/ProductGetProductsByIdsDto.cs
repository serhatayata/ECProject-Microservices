using Core.Entities;

namespace EC.Services.ProductAPI.Dtos.ProductDtos
{
    public class ProductGetProductsByIdsDto:IDto
    {
        public string[] Ids { get; set; }
    }
}
