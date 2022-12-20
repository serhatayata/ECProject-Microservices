using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.CampaignUser
{
    public class CampaignUserAddDto:IDto
    {
        public int CampaignId { get; set; }
        public string UserId { get; set; }
    }
}
