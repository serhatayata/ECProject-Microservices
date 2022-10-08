namespace EC.Services.DiscountAPI.Settings
{
    public class DiscountDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string DiscountsCollection { get; set; }
        public string CampaignsCollection { get; set; }

    }
}
