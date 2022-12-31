using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.Campaign
{
    public class CampaignIdDto:IDto
    {
        public int CampaignId { get; set; }
        public bool IsUsed { get; set; }
    }
}
