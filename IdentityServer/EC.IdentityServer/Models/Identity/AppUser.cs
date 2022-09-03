using Microsoft.AspNetCore.Identity;
using Shared.Entities;

namespace EC.IdentityServer.Models.Identity
{
    public class AppUser:IdentityUser,IEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime LastSeen { get; set; }
    }
}
