using Core.Entities;

namespace EC.Services.LangResourceAPI.Dtos.LangResourceDtos
{
    public class LangResourceUpdateDto:IDto
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; }
        public string MessageCode { get; set; }
        public int LangId { get; set; }
    }
}
