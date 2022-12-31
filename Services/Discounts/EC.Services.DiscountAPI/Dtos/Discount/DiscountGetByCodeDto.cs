using Core.Entities;
using EC.Services.DiscountAPI.Entities;

namespace EC.Services.DiscountAPI.Dtos.Discount
{
    public class DiscountGetByCodeDto:IDto
    {
        public string Code { get; set; }
        public DiscountStatus Status { get; set; }
    }
}
