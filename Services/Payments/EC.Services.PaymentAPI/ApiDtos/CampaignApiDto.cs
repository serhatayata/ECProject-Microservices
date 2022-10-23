namespace EC.Services.PaymentAPI.ApiDtos
{
    public class CampaignApiDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public int CampaignType { get; set; }
        public List<string> Products { get; set; }
    }
}
