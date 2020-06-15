using System;
using System.Net;

namespace Yugen.Toolkit.Standard.Core.Models
{
    /// <summary>
    /// A wrapper class for results/responses that include an object T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T> : Result
    {
        /// <summary>
        /// An object T
        /// </summary>
        public T Value { get; private set; }

        internal Result(T value, bool success, string error, Exception exception, HttpStatusCode? httpStatusCode)
            : base(success, error, exception, httpStatusCode)
        {
            Value = value;
        }
    }
}
