using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.CampaignProduct
{
    public class CampaignProductDto:IDto
    {
        public int CampaignId { get; set; }
        public string ProductId { get; set; }
    }
}
