using System;
using System.Net;

namespace Yugen.Toolkit.Standard.Models
{
    public class Result
    {
        public bool Success { get; }
        public string Error { get; }
        public HttpStatusCode? HttpStatusCode { get; }
        public Exception Exception { get; }

        public bool Failure => !Success;

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="success"></param>
        /// <param name="error"></param>
        /// <param name="exception"></param>
        /// <param name="httpStatusCode"></param>
        protected Result(bool success, string error, Exception exception, HttpStatusCode? httpStatusCode)
        {
            Success = success;
            Error = error;
            Exception = exception;
            HttpStatusCode = httpStatusCode;
        }

        /// <summary>
        /// Creates a Result object that produces a fail response with a message.
        /// </summary>
        /// <returns></returns>
        public static Result Fail(string message) =>
            new Result(false, message, null, null);

        /// <summary>
        /// Creates a Result object that produces a fail response with a message and an exception.
        /// </summary>
        /// <returns></returns>
        public static Result Fail(string message, Exception exception) =>
            new Result(false, message, exception, null);

        /// <summary>
        /// Creates a Result object that produces a fail response with a message
        /// an exception and an httpStatusCode.
        /// </summary>
        /// <returns></returns>
        public static Result Fail(string message, Exception exception, HttpStatusCode? httpStatusCode) =>
            new Result(false, message, exception, httpStatusCode);


        /// <summary>
        /// Creates a Result object that produces a fail response with a message.
        /// </summary>
        /// <returns></returns>
        public static Result<T> Fail<T>(string message) =>
            new Result<T>(default, false, message, null, null);

        /// <summary>
        /// Creates a Result object that produces a fail response with a message  and an exception.
        /// </summary>
        /// <returns></returns>
        public static Result<T> Fail<T>(string message, Exception exception) =>
            new Result<T>(default, false, message, exception, null);

        /// <summary>
        /// Creates a Result object that produces a fail response with a message
        /// an exception and an httpStatusCode.
        /// </summary>
        /// <returns></returns>
        public static Result<T> Fail<T>(string message, Exception exception, HttpStatusCode? httpStatusCode) =>
            new Result<T>(default, false, message, exception, httpStatusCode);

        /// <summary>
        /// Creates a Result object that produces a success response with an empty message.
        /// </summary>
        /// <returns></returns>
        public static Result Ok() =>
            new Result(true, string.Empty, null, null);

        /// <summary>
        /// Creates a Result object that produces a success response with an empty message.
        /// </summary>
        /// <returns></returns>
        public static Result<T> Ok<T>(T value) =>
            new Result<T>(value, true, string.Empty, null, null);


        /// <summary>
        /// Creates a Result object that produces a response with an empty message.
        /// </summary>
        /// <returns></returns>
        public static Result IsOk(bool isOk, string message) =>
            isOk ? Ok() : Fail(message);

        /// <summary>
        /// Creates a Result object that produces a response with an empty message.
        /// </summary>
        /// <returns></returns>
        public static Result<T> IsOk<T>(T value, bool isOk, string message) =>
            isOk ? Ok(value) : Fail<T>(message);


        public static Result Combine(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.Failure)
                {
                    return result;
                }
            }

            return Ok();
        }
    }
}
