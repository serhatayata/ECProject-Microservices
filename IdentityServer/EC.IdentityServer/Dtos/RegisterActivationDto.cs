using Core.Entities;

namespace EC.IdentityServer.Dtos
{
    public class RegisterActivationDto:IDto
    {
        public string Id { get; set; }
        public string ActivationCode { get; set; }
    }
}
