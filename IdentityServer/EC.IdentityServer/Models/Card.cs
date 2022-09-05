using Shared.Entities;

namespace EC.IdentityServer.Models
{
    public class Card:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string CardNumber { get; set; }
        public DateTime Expiration { get; set; }
        public string Cvv { get; set; }
    }
}
