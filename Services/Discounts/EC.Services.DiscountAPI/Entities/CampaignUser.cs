using Core.Entities;

namespace EC.Services.DiscountAPI.Entities
{
    public class CampaignUser:IEntity
    {
        /// <summary>
        /// Campaign Id of the many to many relation
        /// </summary>
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }
        /// <summary>
        /// User Id who is able to use this campaign
        /// </summary>
        public string UserId { get; set; }
    }
}
