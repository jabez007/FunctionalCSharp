using System;
using System.Threading.Tasks;

namespace FunctionalCSharp
{
    /// <summary>
    /// Allows the return of the state of an operation along with a message to give context to any failures of the operation
    /// </summary>
    public class Result
    {
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }
        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, string errorMessage = "")
        {
            if (isSuccess && !string.IsNullOrEmpty(errorMessage))
                throw new InvalidOperationException("Cannot have an error message for a successful Result"); 
            if (!isSuccess && string.IsNullOrEmpty(errorMessage))
                throw new InvalidOperationException("There must be an error message for an unsuccessful Result"); 

            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Creates a successful Result object
        /// </summary>
        /// <returns></returns>
        public static Result Success() => 
            new Result(true);

        /// <summary>
        /// Creates a failed Result object with the given error message
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Result Failure(string errorMessage) => 
            new Result(false, errorMessage);

        /// <summary>
        /// Creates a failed Result object for the given Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Result Failure(Exception ex, string errorMessage = "") =>
            new Result(false, string.Format("{0}{1}{2}", errorMessage, Environment.NewLine, ex.GetMessageStack()));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) =>
            Equals(obj as Result);

        /// <summary>
        /// Returns true iff both Result objects are of the same status (Success or Failure)
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Result other) =>
            other
                .WhenNotNull(  // If other is null (from the cast in the override method), then the default bool type is returned
                    @this => Result<bool>.Success(@this.IsSuccess == other.IsSuccess)
                )
                .Value;  // the default value for bool type is false

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ifSuccess"></param>
        /// <returns></returns>
        public Result Bind(Func<Result> ifSuccess) =>
            IsSuccess ? ifSuccess() : Failure(ErrorMessage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ifSuccess"></param>
        /// <returns></returns>
        public Task<Result> BindAsync(Func<Task<Result>> ifSuccess) =>
            IsSuccess ? ifSuccess() : Task.FromResult(Failure(ErrorMessage));
    }

    /// <summary>
    /// Allows the return of the state of an operation (including return value) along with a message to give context to any failures of the 
    /// operation
    /// </summary>
    /// <typeparam name="T">The class of the return value for your operation</typeparam>
    public class Result<T> : Result
    {
        public T Value { get; }

        protected Result(T value, bool isSuccess, string errorMessage = "")
            : base(isSuccess, errorMessage)
        {
            Value = value;
        }

        /// <summary>
        /// Creates a successful Result object for the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<T> Success(T value) => 
            new Result<T>(value, true);

        /// <summary>
        /// Creates a failed Result object with the given error message
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<T> Failure(string errorMessage, T value = default(T)) => 
            new Result<T>(value, false, errorMessage);

        /// <summary>
        /// Creates a failed Result object from the given Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="errorMessage"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<T> Failure(Exception ex, string errorMessage = "", T value = default(T)) =>
            new Result<T>(value, false, string.Format("{0}{1}{2}", errorMessage, Environment.NewLine, ex.GetMessageStack()));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            IsFailure ? ErrorMessage : Value.ToString();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) =>
            Equals(obj as Result<T>);

        /// <summary>
        /// If this Result on the other Result are both Success Results, then we use the Equals method of the Results' Values. 
        /// Otherwise this will only return true if both Result objects are Failure Results
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Result<T> other) =>
            other
                .WhenNotNull(  // If other is null (from the cast in the override method), then the default bool type is returned
                    @this => @this
                        .SwitchOn<Result<T>, bool>()
                            .Case(
                                t => t.IsSuccess && other.IsSuccess,
                                t => t.Value.Equals(other.Value)  // if both Results are Success Results, check the Equals of the Value objects
                            )
                            .Default(t => t.IsFailure && other.IsFailure)  // Otherwise check if both Results are Failure Results
                        .Map(r => Result<bool>.Success(r))
                )
                .Value;  // the default value for bool type is false

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ifSuccess"></param>
        /// <returns></returns>
        public Result Bind(Func<T, Result> ifSuccess) =>
            IsSuccess ? ifSuccess(Value) : Result.Failure(ErrorMessage);

        /// <summary>
        /// Executes the given function on the returned Value of this Result if this is a successful Result
        /// </summary>
        /// <example>
        /// Result<string>.Success("hello").Bind(str => Result<string>.Success(str + " world"));
        /// </example>
        /// <param name="ifSuccess">Function to execute on the successful Result</param>
        /// <returns>If this Result was successful, the Result of the given function. Otherwise, this Result</returns>
        public Result<TResult> Bind<TResult>(Func<T, Result<TResult>> ifSuccess) => 
            IsSuccess ? ifSuccess(Value) : Result<TResult>.Failure(ErrorMessage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ifSuccess"></param>
        /// <returns></returns>
        public Task<Result> BindAsync(Func<T, Task<Result>> ifSuccess) =>
            IsSuccess ? ifSuccess(Value) : Task.FromResult(Result.Failure(ErrorMessage));

        /// <summary>
        /// Executes the given async function on the returned Value of this Result if this is a successful Result
        /// </summary>
        /// <example>
        /// 
        /// </example>
        /// <param name="ifSuccess">Function to execute on the successful Result</param>
        /// <returns>If this Result was successful, the Result of the given function. Otherwise, this Result</returns>
        public Task<Result<TResult>> BindAsync<TResult>(Func<T, Task<Result<TResult>>> ifSuccess) =>
            IsSuccess ? ifSuccess(Value) : Task.FromResult(Result<TResult>.Failure(ErrorMessage));
    }
}
