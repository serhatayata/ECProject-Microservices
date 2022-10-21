using Core.Entities;
using EC.Services.PaymentAPI.Dtos.BasketDtos;

namespace EC.Services.PaymentAPI.Dtos.PaymentDtos
{
    public class PaymentWithoutUserAddDto:IDto
    {
        //User
        public string PhoneCountry { get; set; }
        public string PhoneNumber { get; set; }
        public string? UserId { get; set; }
        //public string DiscountCode { get; set; }

        //Card Payment
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string CVV { get; set; }
        public decimal TotalPrice { get; set; }
        //Address
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string CountyName { get; set; }
        public string AddressDetail { get; set; }
        public string ZipCode { get; set; }
        //If user is not authenticated, 
        public BasketDto Basket { get; set; }

    }
}
