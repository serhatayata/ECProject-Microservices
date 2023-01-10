using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.CampaignProduct
{
    public class CampaignAddProductDto : IDto
    {
        public int CampaignId { get; set; }
        public string ProductId { get; set; }
    }
}
