﻿using System;
using System.Threading.Tasks;

namespace FunctionalCSharp.Results
{
  /// <summary>
  /// Define the various Bind methods here for the Result classes as extension methods on the Result interfaces
  /// so that inheriting from Result classes isn't so difficult.
  ///
  /// What is a 'Bind'?
  /// A combinator, typically called bind (as in binding a variable), that unwraps a monadic variable, then inserts it into a monadic function/expression, resulting in a new monadic value
  /// </summary>
  public static class IResultExtensions
  {
    #region IResult

    /// <summary>
    /// Executes the given function if this is a successful Result
    /// </summary>
    /// <param name="this"></param>
    /// <param name="ifSuccess"></param>
    /// <returns></returns>
    public static IResult Bind(this IResult @this, Func<IResult> ifSuccess) =>
      @this.IsSuccess ? ifSuccess() : Result.Failure(@this.ErrorMessage);

    /// <summary>
    /// Executes the given async function if this is a successful Result
    /// </summary>
    /// <param name="this"></param>
    /// <param name="ifSuccessAsync"></param>
    /// <returns></returns>
    public static Task<IResult> BindAsync(this IResult @this, Func<Task<IResult>> ifSuccessAsync) =>
      @this.IsSuccess ? ifSuccessAsync() : Task.FromResult(Result.Failure(@this.ErrorMessage));

    /// <summary>
    /// Transforms the extended IResult object into an IResult object that holds ErrorCodes as well.
    /// If the extended IResult object is a successful IResult, then the new IResult will be also.
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="this"></param>
    /// <param name="errorCode"></param>
    /// <returns></returns>
    public static IResult<TEnum> ToErrorCodeResult<TEnum>(this IResult @this, TEnum errorCode) where TEnum : Enum =>
      @this.IsSuccess ? Result<TEnum>.Success(default) : Result<TEnum>.Failure(@this.ErrorMessage, errorCode);

    #endregion IResult

    #region IResult<T>

    /// <summary>
    /// Executes the given function on the returned Value of this Result if this is a successful Result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="ifSuccess"></param>
    /// <returns></returns>
    public static IResult Bind<T>(this IResult<T> @this, Func<T, IResult> ifSuccess) =>
      @this.IsSuccess ? ifSuccess(@this.Value) : Result.Failure(@this.ErrorMessage);

    /// <summary>
    /// Executes the given async function on the returned Value of this Result if this is a successful Result
    /// </summary>
    /// <example>
    ///
    /// </example>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="ifSuccessAsync"></param>
    /// <returns></returns>
    public static Task<IResult> BindAsync<T>(this IResult<T> @this, Func<T, Task<IResult>> ifSuccessAsync) =>
      @this.IsSuccess ? ifSuccessAsync(@this.Value) : Task.FromResult(Result.Failure(@this.ErrorMessage));

    /// <summary>
    /// Executes the given function on the returned Value of this Result if this is a successful Result
    /// </summary>
    /// <example>
    /// Result&lt;string&gt;.Success("hello").Bind(str => Result&lt;string&gt;.Success(str + " world"));
    /// </example>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="ifSuccess">Function to execute on the successful Result</param>
    /// <returns>If this Result was successful, the Result of the given function. Otherwise, this Result</returns>
    public static IResult<TResult> Bind<T, TResult>(this IResult<T> @this, Func<T, IResult<TResult>> ifSuccess) =>
      @this.IsSuccess ? ifSuccess(@this.Value) : Result<TResult>.Failure(@this.ErrorMessage);

    /// <summary>
    /// Executes the given async function on the returned Value of this Result if this is a successful Result
    /// </summary>
    /// <example>
    ///
    /// </example>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="ifSuccessAsync">Function to execute on the successful Result</param>
    /// <returns>If this Result was successful, the Result of the given function. Otherwise, this Result</returns>
    public static Task<IResult<TResult>> BindAsync<T, TResult>(this IResult<T> @this, Func<T, Task<IResult<TResult>>> ifSuccessAsync) =>
      @this.IsSuccess ? ifSuccessAsync(@this.Value) : Task.FromResult(Result<TResult>.Failure(@this.ErrorMessage));

    /// <summary>
    /// Transforms the extended IResult object into an IResult object that holds ErrorCodes as well.
    /// If the extended IResult object is a successful IResult, then the new IResult will be also.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="this"></param>
    /// <param name="errorCode"></param>
    /// <returns></returns>
    public static IResult<T, TEnum> ToErrorCodeResult<T, TEnum>(this IResult<T> @this, TEnum errorCode) where TEnum : struct, Enum =>
      @this.IsSuccess ? Result<T, TEnum>.Success(@this.Value) : Result<T, TEnum>.Failure(errorCode, @this.ErrorMessage, @this.Value);

    #endregion IResult<T>

    #region IResult<TEnum>

    /// <summary>
    /// Special case to try and preserve the error code through the method chain
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="this"></param>
    /// <param name="ifSuccess"></param>
    /// <returns></returns>
    public static IResult<TEnum> Bind<TEnum>(this IResult<TEnum> @this, Func<IResult<TEnum>> ifSuccess) where TEnum : struct, Enum =>
      @this.IsSuccess ? ifSuccess() : @this;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="this"></param>
    /// <param name="ifSuccessAsync"></param>
    /// <returns></returns>
    public static Task<IResult<TEnum>> BindAsync<TEnum>(this IResult<TEnum> @this, Func<Task<IResult<TEnum>>> ifSuccessAsync) where TEnum : struct, Enum =>
      @this.IsSuccess ? ifSuccessAsync() : Task.FromResult(@this);

    #endregion IResult<TEnum>

    #region IResult<T, TEnum>

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TEnum"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="ifSuccess"></param>
    /// <returns></returns>
    public static IResult<TEnum> Bind<T, TEnum, TResult>(this IResult<T, TEnum> @this, Func<T, IResult<TEnum>> ifSuccess) where TEnum : struct, Enum =>
      @this.IsSuccess ? ifSuccess(@this.Value) : Result<TEnum>.Failure(@this.ErrorCode.ToString());

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TEnum"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="ifSuccessAsync"></param>
    /// <returns></returns>
    public static Task<IResult<TEnum>> BindAsync<T, TEnum, TResult>(this IResult<T, TEnum> @this, Func<T, Task<IResult<TEnum>>> ifSuccessAsync)
      where TEnum : struct, Enum =>
        @this.IsSuccess ? ifSuccessAsync(@this.Value) : Task.FromResult(Result<TEnum>.Failure(@this.ErrorCode.ToString()));

    /// <summary>
    /// Executes the given function on the returned Value of this Result if this is a successful Result
    /// </summary>
    /// <example>
    /// Result&lt;string&gt;.Success("hello").Bind(str => Result&lt;string&gt;.Success(str + " world"));
    /// </example>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TEnum"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="ifSuccess">Function to execute on the successful Result</param>
    /// <returns>If this Result was successful, the Result of the given function. Otherwise, this Result</returns>
    public static IResult<TResult, TEnum> Bind<T, TEnum, TResult>(this IResult<T, TEnum> @this, Func<T, IResult<TResult, TEnum>> ifSuccess)
      where TEnum : struct, Enum =>
        @this.IsSuccess ? ifSuccess(@this.Value) : Result<TResult, TEnum>.Failure(@this.ErrorCode, @this.ErrorMessage);

    /// <summary>
    /// Executes the given async function on the returned Value of this Result if this is a successful Result
    /// </summary>
    /// <example>
    ///
    /// </example>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TEnum"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="ifSuccessAsync">Function to execute on the successful Result</param>
    /// <returns>If this Result was successful, the Result of the given function. Otherwise, this Result</returns>
    public static Task<IResult<TResult, TEnum>> BindAsync<T, TEnum, TResult>(this IResult<T, TEnum> @this,
      Func<T, Task<IResult<TResult, TEnum>>> ifSuccessAsync) where TEnum : struct, Enum =>
        @this.IsSuccess ? ifSuccessAsync(@this.Value) : Task.FromResult(Result<TResult, TEnum>.Failure(@this.ErrorCode, @this.ErrorMessage));

    #endregion IResult<T, TEnum>
  }
}