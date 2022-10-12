namespace EC.Services.PaymentAPI.Settings
{
    public class ClientSettings
    {
        public Client IdentityClient { get; set; }
        public Client DiscountClient { get; set; }
        public Client OrderClient { get; set; }

    }

    public class Client
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
