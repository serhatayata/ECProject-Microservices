using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.Campaign
{
    public class CampaignDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public int CampaignType { get; set; }
        public List<int> Products { get; set; }
    }
}
