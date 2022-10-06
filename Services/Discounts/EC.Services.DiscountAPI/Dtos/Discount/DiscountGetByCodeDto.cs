using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.Discount
{
    public class DiscountGetByCodeDto:IDto
    {
        public string Code { get; set; }
    }
}
