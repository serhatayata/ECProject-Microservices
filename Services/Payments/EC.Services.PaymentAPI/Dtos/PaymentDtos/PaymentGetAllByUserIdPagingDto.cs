namespace EC.Services.PaymentAPI.Dtos.PaymentDtos
{
    public class PaymentGetAllByUserIdPagingDto
    {
        public string UserId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 8;
    }
}
