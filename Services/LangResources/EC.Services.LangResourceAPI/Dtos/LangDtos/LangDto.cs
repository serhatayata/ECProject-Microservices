using Core.Entities;
using EC.Services.LangResourceAPI.Dtos.LangResourceDtos;

namespace EC.Services.LangResourceAPI.Dtos.LangDtos
{
    public class LangDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<LangResourceDto> LangResources { get; set; }
    }
}
