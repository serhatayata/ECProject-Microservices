using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos
{
    public class DiscountAddDto:IDto
    {
        public string UserId { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
    }
}
