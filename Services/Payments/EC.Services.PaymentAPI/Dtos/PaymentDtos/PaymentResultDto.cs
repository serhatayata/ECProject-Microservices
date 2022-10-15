using Core.Entities;

namespace EC.Services.PaymentAPI.Dtos.PaymentDtos
{
    public class PaymentResultDto:IDto
    {
        public string PaymentNo { get; set; }
    }
}
