using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.Campaign
{
    public class CampaignUpdateDto:IDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int CampaignType { get; set; }
    }
}