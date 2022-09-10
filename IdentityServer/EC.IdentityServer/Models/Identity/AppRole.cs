using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace EC.IdentityServer.Models.Identity
{
    public class AppRole:IdentityRole,IEntity
    {
        public AppRole()
        {

        }

        public AppRole(string roleName) : this()
        {
            Name = roleName;
        }
    }
}
