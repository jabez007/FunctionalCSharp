using FunctionalCSharp.FuncExtensions;
using System;
using System.Threading.Tasks;

namespace FunctionalCSharp.Results.FuncExtensions
{
  /// <summary>
  ///
  /// </summary>
  public static class ExceptionExtensions
  {
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<T, IResult<TResult>> Catch<T, TResult, TException>(this Func<T, IResult<TResult>> @this)
      where TException : Exception =>
        @this.Catch<T, IResult<TResult>, TException>()(ex => Result<TResult>.Failure(ex));

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<T, Task<IResult<TResult>>> CatchAsync<T, TResult, TException>(this Func<T, Task<IResult<TResult>>> @this)
      where TException : Exception =>
        @this.CatchAsync<T, IResult<TResult>, TException>()(ex => Task.FromResult(Result<TResult>.Failure(ex)));
  }
}