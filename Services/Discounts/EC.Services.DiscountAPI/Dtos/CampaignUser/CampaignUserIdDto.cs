using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.CampaignUser
{
    public class CampaignUserIdDto:IDto
    {
        public string UserId { get; set; }
        public bool IsUsed { get; set; }
    }
}
