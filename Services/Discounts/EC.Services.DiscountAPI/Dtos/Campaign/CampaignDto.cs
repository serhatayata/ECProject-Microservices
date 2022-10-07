using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.Campaign
{
    public class CampaignDto:IDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public int CampaignType { get; set; }
        public List<string> Products { get; set; }
    }
}
