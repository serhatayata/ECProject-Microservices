using Core.Entities;

namespace EC.Services.LangResourceAPI.Entities
{
    public class LangResource:IEntity
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; }
        public string MessageCode { get; set; }
        public int LangId { get; set; }
        public Lang Lang { get; set; }
    }
}
