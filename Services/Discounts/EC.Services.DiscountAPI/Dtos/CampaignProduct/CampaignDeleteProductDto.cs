using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.CampaignProduct
{
    public class CampaignDeleteProductDto : IDto
    {
        public int CampaignId { get; set; }
        public int ProductId { get; set; }
    }
}
