using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.Campaign
{
    public class CampaignDeleteProductDto:IDto
    {
        public int CampaignId { get; set; }
        public int ProductId { get; set; }
    }
}
