using AutoMapper;
using EC.IdentityServer.Dtos;
using EC.IdentityServer.Models.Identity;

namespace EC.IdentityServer.Mappings
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<AppUser, RegisterDto>().ReverseMap();



        }

    }
}
