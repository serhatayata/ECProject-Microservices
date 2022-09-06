using Core.Entities;

namespace EC.IdentityServer.Dtos
{
    public class ChangePasswordDto:IDto
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
