using Core.Entities;

namespace EC.Services.PaymentAPI.Dtos.OrderDtos
{
    public class AddressDto:IDto
    {
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string CountyName { get; set; }
        public string AddressDetail { get; set; }
        public string ZipCode { get; set; }
    }
}
