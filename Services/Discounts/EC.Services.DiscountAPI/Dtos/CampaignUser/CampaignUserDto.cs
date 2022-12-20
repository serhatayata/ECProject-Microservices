using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.CampaignUser
{
    public class CampaignUserDto:IDto
    {
        public int CampaignId { get; set; }
        public string UserId { get; set; }
        public bool IsUsed { get; set; }
    }
}
