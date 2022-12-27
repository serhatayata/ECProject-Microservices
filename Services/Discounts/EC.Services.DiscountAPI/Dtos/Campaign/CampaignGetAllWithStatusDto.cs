using Core.Entities;
using EC.Services.DiscountAPI.Entities;

namespace EC.Services.DiscountAPI.Dtos.Campaign
{
    public class CampaignGetAllWithStatusDto:IDto
    {
        public CampaignStatus Status { get; set; }
    }
}
