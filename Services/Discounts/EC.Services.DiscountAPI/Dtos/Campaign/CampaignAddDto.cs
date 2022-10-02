using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.Campaign
{
    public class CampaignAddDto:IDto
    {
        public int Name { get; set; }
        public int CampaignType { get; set; }
    }
}
