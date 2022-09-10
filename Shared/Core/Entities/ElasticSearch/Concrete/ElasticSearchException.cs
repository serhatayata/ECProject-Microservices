using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.ElasticSearch.Concrete
{
    [Serializable]
    public class ElasticSearchException : Exception
    {
        public ElasticSearchException()
        {
        }
        public ElasticSearchException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
        {
        }
        public ElasticSearchException(string message) : base(message)
        {
        }
        public ElasticSearchException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
