using Core.Entities;

namespace EC.Services.LangResourceAPI.Entities
{
    public class Lang:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<LangResource> LangResources { get; set; }
    }
}
