namespace Common.Standard.Data
{
    //http://enterprisecraftsmanship.com/2015/03/20/functional-c-handling-failures-input-errors/
    //https://gist.github.com/vkhorikov/7852c7606f27c52bc288
    public class Result
    {
        public bool Success { get; }
        public string Error { get; }
        //public StatusCode Status { get; }
        //public Exception StatusExplanation { get; }

        public bool Failure => !Success;

        protected Result(bool success, string error)
        {
            Success = success;
            Error = error;
        }

        public static Result Fail(string message) => new Result(false, message);

        public static Result<T> Fail<T>(string message) => new Result<T>(default(T), false, message);

        public static Result Ok() => new Result(true, string.Empty);

        public static Result<T> Ok<T>(T value) => new Result<T>(value, true, string.Empty);

        public static Result IsOk(bool isOk, string message) => isOk ? Ok() : Fail(message);

        public static Result<T> IsOk<T>(T value, bool isOk, string message) => isOk ? Ok(value) : Fail<T>(message);

        public static Result Combine(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.Failure)
                    return result;
            }

            return Ok();
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; private set; }

        protected internal Result(T value, bool success, string error)
            : base(success, error)
        {
            Value = value;
        }
    }
}
