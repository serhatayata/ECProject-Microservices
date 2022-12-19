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
        /// <summary>
        /// This campaign is used by user or not
        /// </summary>
        public bool IsUsed { get; set; }
        /// <summary>
        /// Campaign code
        /// </summary>
        public string Code { get; set; }
    }
}
