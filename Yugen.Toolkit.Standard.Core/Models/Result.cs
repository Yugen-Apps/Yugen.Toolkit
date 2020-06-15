using System;
using System.Net;

namespace Yugen.Toolkit.Standard.Core.Models
{
    /// <summary>
    /// A wrapper class for results/responses
    /// </summary>
    public class Result
    {
        /// <summary>
        /// IsSuccess
        /// </summary>
        public bool IsSuccess { get; }
        /// <summary>
        /// Error
        /// </summary>
        public string Error { get; }
        /// <summary>
        /// HttpStatusCode
        /// </summary>
        public HttpStatusCode? HttpStatusCode { get; }
        /// <summary>
        /// Exception
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// IsFailure
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="success"></param>
        /// <param name="error"></param>
        /// <param name="exception"></param>
        /// <param name="httpStatusCode"></param>
        protected Result(bool success, string error, Exception exception, HttpStatusCode? httpStatusCode)
        {
            IsSuccess = success;
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
        /// Creates a Result object that produces a fail response with a message and an httpStatusCode.
        /// </summary>
        /// <returns></returns>
        public static Result Fail(string message, HttpStatusCode httpStatusCode) =>
            new Result(false, message, null, httpStatusCode);

        /// <summary>
        /// Creates a Result object that produces a fail response with a message
        /// an exception and an httpStatusCode.
        /// </summary>
        /// <returns></returns>
        public static Result Fail(string message, Exception exception, HttpStatusCode httpStatusCode) =>
            new Result(false, message, exception, httpStatusCode);


        /// <summary>
        /// Creates a Result object that produces a fail response with a message.
        /// </summary>
        /// <returns></returns>
        public static Result<T> Fail<T>(string message) =>
            new Result<T>(default, false, message, null, null);

        /// <summary>
        /// Creates a Result object that produces a fail response with a message and an exception.
        /// </summary>
        /// <returns></returns>
        public static Result<T> Fail<T>(string message, Exception exception) =>
            new Result<T>(default, false, message, exception, null);

        /// <summary>
        /// Creates a Result object that produces a fail response with a message and an httpStatusCode.
        /// </summary>
        /// <returns></returns>
        public static Result<T> Fail<T>(string message, HttpStatusCode httpStatusCode) =>
            new Result<T>(default, false, message, null, httpStatusCode);

        /// <summary>
        /// Creates a Result object that produces a fail response with a message
        /// an exception and an httpStatusCode.
        /// </summary>
        /// <returns></returns>
        public static Result<T> Fail<T>(string message, Exception exception, HttpStatusCode httpStatusCode) =>
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
        /// Creates a Result object that produces a response with a message.
        /// </summary>
        /// <returns></returns>
        public static Result IsOk(bool isOk, string message) =>
            isOk ? Ok() : Fail(message);

        /// <summary>
        /// Creates a Result object that produces a response with a message and an exception.
        /// </summary>
        /// <returns></returns>
        public static Result IsOk(bool isOk, string message, Exception exception) =>
            isOk ? Ok() : Fail(message, exception);

        /// <summary>
        /// Creates a Result object that produces a response with a message and an httpStatusCode.
        /// </summary>
        /// <returns></returns>
        public static Result IsOk(bool isOk, string message, HttpStatusCode httpStatusCode) =>
            isOk ? Ok() : Fail(message, httpStatusCode);

        /// <summary>
        /// Creates a Result object that produces a response with a message
        /// an exception and an httpStatusCode.
        /// </summary>
        /// <returns></returns>
        public static Result IsOk(bool isOk, string message, Exception exception, HttpStatusCode httpStatusCode) =>
            isOk ? Ok() : Fail(message, exception, httpStatusCode);


        /// <summary>
        /// Creates a Result object that produces a response with a message.
        /// </summary>
        /// <returns></returns>
        public static Result<T> IsOk<T>(bool isOk, T value, string message) =>
            isOk ? Ok(value) : Fail<T>(message);

        /// <summary>
        /// Creates a Result object that produces a response with a message and an exception.
        /// </summary>
        /// <returns></returns>
        public static Result<T> IsOk<T>(bool isOk, T value, string message, Exception exception) =>
            isOk ? Ok(value) : Fail<T>(message, exception);

        /// <summary>
        /// Creates a Result object that produces a response with a message and an httpStatusCode.
        /// </summary>
        /// <returns></returns>
        public static Result<T> IsOk<T>(bool isOk, T value, string message, HttpStatusCode httpStatusCode) =>
            isOk ? Ok(value) : Fail<T>(message, httpStatusCode);

        /// <summary>
        /// Creates a Result object that produces a response with a message
        /// an exception and an httpStatusCode.
        /// </summary>
        /// <returns></returns>
        public static Result<T> IsOk<T>(bool isOk, T value, string message, Exception exception, HttpStatusCode httpStatusCode) =>
            isOk ? Ok(value) : Fail<T>(message, exception, httpStatusCode);


        /// <summary>
        /// Combine Results
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static Result Combine(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.IsFailure)
                {
                    return result;
                }
            }

            return Ok();
        }
    }
}
