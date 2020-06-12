using System;
using System.Net;

namespace Yugen.Toolkit.Standard.Models
{
    public class Result<T> : Result
    {
        public T Value { get; private set; }

        internal Result(T value, bool success, string error, Exception exception, HttpStatusCode? httpStatusCode)
            : base(success, error, exception, httpStatusCode)
        {
            Value = value;
        }
    }
}
