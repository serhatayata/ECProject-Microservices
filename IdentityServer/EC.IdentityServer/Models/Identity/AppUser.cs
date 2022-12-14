using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace EC.IdentityServer.Models.Identity
{
    public class AppUser:IdentityUser,IEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CountryCode { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime LastSeen { get; set; }
    }

    public enum UserStatus
    {
        NotValidated=1,
        Validated=2,
        Suspended=3
    }
}
