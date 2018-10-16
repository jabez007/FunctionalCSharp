using System;

namespace FunctionalCSharp.Results
{
  /// <summary>
  /// Allows the return of the state of an operation along with a message to give context to any failures of the operation
  /// </summary>
  public class Result : IResult
  {
    /// <summary>
    ///
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    ///
    /// </summary>
    public string ErrorMessage { get; }

    /// <summary>
    ///
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    ///
    /// </summary>
    /// <param name="isSuccess"></param>
    /// <param name="errorMessage"></param>
    protected internal Result(bool isSuccess, string errorMessage = "")
    {
      if (isSuccess && !string.IsNullOrEmpty(errorMessage))
      {
        throw new InvalidOperationException("Cannot have an error message for a successful Result");
      }

      if (!isSuccess && string.IsNullOrEmpty(errorMessage))
      {
        throw new InvalidOperationException("There must be an error message for an unsuccessful Result");
      }

      IsSuccess = isSuccess;
      ErrorMessage = errorMessage;
    }

    /// <summary>
    /// Creates a successful Result object
    /// </summary>
    /// <returns></returns>
    public static IResult Success() =>
      new Result(true);

    /// <summary>
    /// Creates a failed Result object with the given error message
    /// </summary>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    public static IResult Failure(string errorMessage) =>
      new Result(false, errorMessage);

    /// <summary>
    /// Creates a failed Result object for the given Exception
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    public static IResult Failure(Exception ex, string errorMessage = "") =>
      new Result(false, ex.GetMessageStack(errorMessage));

    /// <summary>
    ///
    /// </summary>
    /// <param name="r1"></param>
    /// <param name="r2"></param>
    /// <returns></returns>
    public static IResult operator +(Result r1, Result r2) =>
      new Result(r1.IsSuccess && r2.IsSuccess,
        (r1.IsFailure ? string.Format("{1}{0}", Environment.NewLine, r1.ErrorMessage) : "") + (r2.IsFailure ? r2.ErrorMessage : ""));

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
      IsFailure ? ErrorMessage : "Success";

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
  }

  /// <summary>
  /// Allows the return of the state of an operation (including return value) along with a message to give context to any failures of the
  /// operation
  /// </summary>
  /// <typeparam name="T">The class of the return value for your operation</typeparam>
  public class Result<T> : Result, IResult<T>
  {
    /// <summary>
    /// The returned object of a successful function
    /// </summary>
    public T Value { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="value"></param>
    /// <param name="isSuccess"></param>
    /// <param name="errorMessage"></param>
    protected internal Result(T value, bool isSuccess, string errorMessage = "")
      : base(isSuccess, errorMessage)
    {
      Value = value;
    }

    /// <summary>
    /// Creates a successful Result object for the given value
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static IResult<T> Success(T value) =>
      new Result<T>(value, true);

    /// <summary>
    /// Creates a failed Result object with the given error message
    /// </summary>
    /// <param name="errorMessage"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static IResult<T> Failure(string errorMessage, T value = default) =>
      new Result<T>(value, false, errorMessage);

    /// <summary>
    /// Creates a failed Result object from the given Exception
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="errorMessage"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static IResult<T> Failure(Exception ex, string errorMessage = "", T value = default) =>
      new Result<T>(value, false, ex.GetMessageStack(errorMessage));

    /// <summary>
    ///
    /// </summary>
    /// <param name="r1"></param>
    /// <param name="r2"></param>
    /// <returns></returns>
    public static IResult<T> operator +(Result<T> r1, Result<T> r2) =>
      new Result<T>(
        typeof(T).IsEnum ? (T)Enum.ToObject(typeof(T), (int)(object)r1.Value | (int)(object)r2.Value) : default,
        r1.IsSuccess && r2.IsSuccess,
        (r1.IsFailure ? string.Format("{1}{0}", Environment.NewLine, r1.ErrorMessage) : "") + (r2.IsFailure ? r2.ErrorMessage : "")
      );

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
  }

  /// <summary>
  /// Allows the return of an error code along with an error message
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="TEnum">An Enum type that specifies the different errors possible to be returned</typeparam>
  public class Result<T, TEnum> : Result<T>, IResult<T, TEnum>
    where TEnum : struct, Enum
  {
    /// <summary>
    ///
    /// </summary>
    public TEnum ErrorCode { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="value"></param>
    /// <param name="isSuccess"></param>
    /// <param name="errorCode"></param>
    /// <param name="errorMessage"></param>
    protected internal Result(T value, bool isSuccess, TEnum errorCode = default, string errorMessage = "")
      : base(value, isSuccess, errorMessage)
    {
      if (isSuccess && errorCode.IsNotDefault())
      {
        throw new InvalidOperationException("Cannot have an error code for a successful Result");
      }

      if (!isSuccess && !errorCode.IsNotDefault())
      {
        throw new InvalidOperationException("There must be an error code for an unsuccessful Result");
      }

      ErrorCode = errorCode;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static new IResult<T, TEnum> Success(T value) =>
      new Result<T, TEnum>(value, true);

    /// <summary>
    /// Creates a failed Result object with the given error code and error message
    /// </summary>
    /// <param name="errorCode"></param>
    /// <param name="errorMessage"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static IResult<T, TEnum> Failure(TEnum errorCode, string errorMessage = "", T value = default) =>
      new Result<T, TEnum>(value, false, errorCode, errorMessage.IsNotNullOrEmpty() ? errorMessage : errorCode.ToString());

    /// <summary>
    /// Creates a failed Result object from the given Exception with the given error code and error message
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="errorCode"></param>
    /// <param name="errorMessage"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static IResult<T, TEnum> Failure(Exception ex, TEnum errorCode, string errorMessage = "", T value = default) =>
      new Result<T, TEnum>(value, false, errorCode,
        ex.GetMessageStack(errorMessage.IsNotNullOrEmpty() ? errorMessage : errorCode.ToString()));

    /// <summary>
    ///
    /// </summary>
    /// <param name="r1"></param>
    /// <param name="r2"></param>
    /// <returns></returns>
    public static IResult<T, TEnum> operator +(Result<T, TEnum> r1, Result<T, TEnum> r2) =>
      new Result<T, TEnum>(
        default,
        r1.IsSuccess && r2.IsSuccess,
        (TEnum)Enum.ToObject(typeof(TEnum), ((int)(object)r1.ErrorCode | (int)(object)r2.ErrorCode)),  // bitwise or to combine the error codes
        (r1.IsFailure ? string.Format("{1}{0}", Environment.NewLine, r1.ErrorMessage) : "") + (r2.IsFailure ? r2.ErrorMessage : "")
      );

    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj) =>
      Equals(obj as Result<T, TEnum>);

    /// <summary>
    /// If this Result on the other Result are both Success Results, then we use the Equals method of the Results' Values.
    /// Otherwise this will only return true if both Result objects are Failure Results
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Result<T, TEnum> other) =>
      other
        .WhenNotNull(  // If other is null (from the cast in the override method), then the default bool type is returned
          @this => @this
            .SwitchOn<Result<T, TEnum>, bool>()
              .Case(
                t => t.IsSuccess && other.IsSuccess,
                t => t.Value.Equals(other.Value)  // if both Results are Success Results, check the Equals of the Value objects
              )
              .Case(
                t => t.IsFailure && other.IsFailure,  // elif both Results are Failure Results
                t => t.ErrorCode.Equals(other.ErrorCode)  // are they are the same error code?
              )
              .Default(t => false)  // else these aren't equal
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
  }
}