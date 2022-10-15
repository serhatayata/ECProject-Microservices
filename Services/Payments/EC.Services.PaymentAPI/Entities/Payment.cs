using Core.Entities;

namespace EC.Services.PaymentAPI.Entities
{
    public class Payment:IEntity
    {
        public int Id { get; set; }
        public DateTime CDate { get; set; }
        public short Status { get; set; }
        //User
        public string PhoneCountry { get; set; }
        public string PhoneNumber { get; set; }
        public string? UserId { get; set; }
        //Card Payment
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        //public string ExpirationMonth { get; set; }
        //public string ExpirationYear { get; set; }
        //public string CVV { get; set; }
        public decimal TotalPrice { get; set; }
        //Address
        public string CountryName { get; set; }
        public string CountyName { get; set; }
        public string CityName { get; set; }
        public string AddressDetail { get; set; }
        public string ZipCode { get; set; }

    }

    public enum PaymentStatus
    {
        Completed=1,
        Waiting=2,
        Canceled=3
    }
}
