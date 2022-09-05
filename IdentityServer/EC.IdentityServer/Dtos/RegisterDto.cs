using Shared.Entities;

namespace EC.IdentityServer.Dtos
{
    public class RegisterDto:IDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

    }
}
