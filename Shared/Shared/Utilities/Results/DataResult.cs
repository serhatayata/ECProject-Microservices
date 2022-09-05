using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, bool success, object message, int statusCode) : base(success, message, statusCode)
        {
            Data = data;
        }

        public DataResult(T data, bool success) : base(success)
        {
            Data = data;
        }

        [JsonConstructor]
        public DataResult(T data) : base(true)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
