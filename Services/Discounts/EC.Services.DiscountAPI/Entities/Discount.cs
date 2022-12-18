using Core.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EC.Services.DiscountAPI.Entities
{
    public class Discount:IEntity
    {
        /// <summary>
        /// Id of discount
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the discount
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description of the discount
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Discount type with enum
        /// </summary>
        public DiscountTypes DiscountType { get; set; }
        /// <summary>
        /// Sponsor name
        /// </summary>
        public string Sponsor { get; set; }
        /// <summary>
        /// Rate of the discount
        /// </summary>
        public int Rate { get; set; }
        /// <summary>
        /// Discount code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Status of  the count, whether it is active or not
        /// </summary>
        public DiscountStatus Status { get; set; }
        /// <summary>
        /// Created date of the discount
        /// </summary>
        public DateTime CDate { get; set; }
        /// <summary>
        /// Updated date of the discount
        /// </summary>
        public DateTime UDate { get; set; }
        /// <summary>
        /// Expiration date
        /// </summary>
        public DateTime ExpirationDate { get; set; }
    }

    public enum DiscountStatus
    {
        Active = 0,
        Deleted = 1
    }
}