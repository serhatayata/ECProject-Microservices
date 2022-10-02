using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.Campaign
{
    public class CampaignUpdateDto:IDto
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int CampaignType { get; set; }
        public List<int> Products { get; set; }
    }
}
