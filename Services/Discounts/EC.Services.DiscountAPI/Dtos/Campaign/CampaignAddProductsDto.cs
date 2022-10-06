using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.Campaign
{
    public class CampaignAddProductsDto:IDto
    {
        public string CampaignId { get; set; }
        public int Rate { get; set; }
        public List<string> ProductIds { get; set; }
    }
}
