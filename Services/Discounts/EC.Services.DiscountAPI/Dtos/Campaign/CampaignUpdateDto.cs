using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.Campaign
{
    public class CampaignUpdateDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CampaignTypes CampaignType { get; set; }
        public int Rate { get; set; }
        public int Amount { get; set; }
        public string Sponsor { get; set; }
        public bool Status { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}