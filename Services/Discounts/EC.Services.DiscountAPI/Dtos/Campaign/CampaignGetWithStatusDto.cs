using Core.Entities;
using EC.Services.DiscountAPI.Entities;

namespace EC.Services.DiscountAPI.Dtos.Campaign
{
    public class CampaignGetWithStatusDto:IDto
    {
        public int Id { get; set; }
        public CampaignStatus Status { get; set; }
    }
}
