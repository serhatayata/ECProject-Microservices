using Core.Entities;
using EC.Services.DiscountAPI.Entities;

namespace EC.Services.DiscountAPI.Dtos.Campaign
{
    public class CampaignCodeDto:IDto
    {
        public string CampaignCode { get; set; }
        public CampaignStatus Status { get; set; }
    }
}
