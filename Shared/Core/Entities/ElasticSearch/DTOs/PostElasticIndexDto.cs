using Core.Entities.ElasticSearch.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.ElasticSearch.DTOs
{
    public class PostElasticIndexDto : ElasticEntity<int>, IDto
    {
        public PostElasticIndexDto()
        {
            TagNameValues = new List<string>();
            TagNameIds = new List<int>();
        }

        public virtual string Title { get; set; }
        public virtual string CategoryName { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual List<string> TagNameValues { get; set; }
        public virtual List<int> TagNameIds { get; set; }
        public virtual string Url { get; set; }
        public virtual string UserInfo { get; set; }
        public virtual int UserId { get; set; }
        public virtual DateTime CreatedDate { get; set; }
    }
}
