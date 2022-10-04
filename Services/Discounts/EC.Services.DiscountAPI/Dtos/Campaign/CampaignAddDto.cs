using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.Campaign
{
    public class CampaignAddDto:IDto
    {
        public string Name { get; set; }
        public int CampaignType { get; set; }
    }
}
