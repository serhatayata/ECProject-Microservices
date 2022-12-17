using Core.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EC.Services.DiscountAPI.Entities
{
    public class Campaign:IEntity
    {
        public int Id { get; set; }
        /// <summary>
        /// Campaign name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Campaign description, used for what
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Product id of the campaign
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// CampaignType gives us the type of campaign
        /// </summary>
        public CampaignTypes CampaignType { get; set; }
        /// <summary>
        /// If Campaign type is Price and Rate is 5, then it will be 5 
        /// </summary>
        public int Rate { get; set; }
        /// <summary>
        /// Amount of how many times this campaign can be used
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// Sponsor of the campaign
        /// </summary>
        public string Sponsor { get; set; }
        /// <summary>
        /// Whether the campaign is active or not
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// Created date of the campaign
        /// </summary>
        public DateTime CDate { get; set; }
        /// <summary>
        /// Update date of the campaign
        /// </summary>
        public DateTime UDate { get; set; }
        /// <summary>
        /// Expiration date
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// Users of the campaign
        /// </summary>
        public ICollection<CampaignUser> CampaignUsers { get; set; }
    }
}
