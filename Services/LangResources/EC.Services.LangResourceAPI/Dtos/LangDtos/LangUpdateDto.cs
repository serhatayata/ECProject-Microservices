using Core.Entities;

namespace EC.Services.LangResourceAPI.Dtos.LangDtos
{
    public class LangUpdateDto:IDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
