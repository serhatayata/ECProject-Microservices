using Core.Entities;

namespace EC.Services.PaymentAPI.Dtos.ProductDtos
{
    public class ProductGetByProductIdsDto:IDto
    {
        public string[] Ids { get; set; }

    }
}
