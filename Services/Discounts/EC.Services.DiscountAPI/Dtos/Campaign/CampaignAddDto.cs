using Core.Entities;
using EC.Services.DiscountAPI.Entities;

namespace EC.Services.DiscountAPI.Dtos.Campaign
{
    public class CampaignAddDto:IDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CampaignCode { get; set; }
        public CampaignTypes CampaignType { get; set; }
        public int Rate { get; set; }
        public int Amount { get; set; }
        public string Sponsor { get; set; }
        public CampaignStatus Status { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
