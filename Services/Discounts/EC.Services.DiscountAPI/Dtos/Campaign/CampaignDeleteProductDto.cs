using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.Campaign
{
    public class CampaignDeleteProductDto:IDto
    {
        public string CampaignId { get; set; }
        public string ProductId { get; set; }
    }
}
