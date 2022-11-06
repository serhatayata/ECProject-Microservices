using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IResult
    {
        bool Success { get; }
        object Message { get; }
        string MessageCode { get; }
        int StatusCode { get; }
    }
}
