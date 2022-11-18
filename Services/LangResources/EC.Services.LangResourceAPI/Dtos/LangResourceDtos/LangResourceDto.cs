using Core.Entities;
using EC.Services.LangResourceAPI.Entities;

namespace EC.Services.LangResourceAPI.Dtos.LangResourceDtos
{
    public class LangResourceDto:IDto
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; }
        public string MessageCode { get; set; }
        public int LangId { get; set; }
        public Lang Lang { get; set; }
    }
}
