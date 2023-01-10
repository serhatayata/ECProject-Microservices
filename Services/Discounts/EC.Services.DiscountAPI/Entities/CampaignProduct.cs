using Core.Entities;

namespace EC.Services.DiscountAPI.Entities
{
    public class CampaignProduct:IEntity
    {
        /// <summary>
        /// Campaign Id of the many to many relation
        /// </summary>
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }
        /// <summary>
        /// Product Id which this campaign includes
        /// </summary>
        public string ProductId { get; set; }
    }
}
