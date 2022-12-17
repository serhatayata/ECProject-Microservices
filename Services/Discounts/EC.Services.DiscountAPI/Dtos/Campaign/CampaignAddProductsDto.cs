using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.Campaign
{
    public class CampaignAddProductsDto:IDto
    {
        public int CampaignId { get; set; }
        public List<int> ProductIds { get; set; }
    }
}
