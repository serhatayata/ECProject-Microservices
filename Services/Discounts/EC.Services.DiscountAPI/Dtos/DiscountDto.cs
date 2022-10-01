using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos
{
    public class DiscountDto:IDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public DateTime CDate { get; set; }
    }
}
