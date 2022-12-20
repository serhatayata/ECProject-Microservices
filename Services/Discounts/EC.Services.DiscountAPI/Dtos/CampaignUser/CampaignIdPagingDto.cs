using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.CampaignUser
{
    public class CampaignIdPagingDto:IDto
    {
        public int CampaignId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 8;
        public bool IsUsed { get; set; }
    }
}
