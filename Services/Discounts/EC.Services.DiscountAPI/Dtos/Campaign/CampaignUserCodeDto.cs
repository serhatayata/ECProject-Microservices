using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.Campaign
{
    public class CampaignUserCodeDto:IDto
    {
        public string Code { get; set; }
        public bool IsUsed { get; set; }
    }
}
