using System;
using System.Threading;
using System.Threading.Tasks;

namespace FunctionalCSharp.Results.FuncExtensions
{
  /// <summary>
  ///
  /// </summary>
  public static class RetryExtensions
  {
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<int, Func<TimeSpan, Func<T, IResult<TResult>>>> RetryOnFailure<T, TResult, TException>(this Func<T, IResult<TResult>> @this)
      where TException : Exception =>
        (maxAttempts) => (sleepBetween) => (input) =>
        {
          var result = @this.Catch<T, TResult, TException>()(input);
          if (result.IsSuccess)
          {
            return result;
          }
          else
          {
            if (maxAttempts > 1)
            {
              Thread.Sleep(sleepBetween);
              return RetryOnFailure<T, TResult, TException>(@this)(maxAttempts - 1)(sleepBetween)(input);
            }
            else
            {
              return Result<TResult>.Failure(string.Format("Maximum number of retires reached:{0}{1}", Environment.NewLine, result.ErrorMessage));
            }
          }
        };

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<int, Func<TimeSpan, Func<T, Task<IResult<TResult>>>>> RetryOnFailureAsync<T, TResult, TException>(this Func<T, Task<IResult<TResult>>> @this)
      where TException : Exception =>
        (maxAttempts) => (sleepBetween) => async (input) =>
        {
          var result = await @this.CatchAsync<T, TResult, TException>()(input);
          if (result.IsSuccess)
          {
            return result;
          }
          else
          {
            if (maxAttempts > 1)
            {
              Thread.Sleep(sleepBetween);
              return await RetryOnFailureAsync<T, TResult, TException>(@this)(maxAttempts - 1)(sleepBetween)(input);
            }
            else
            {
              return Result<TResult>.Failure(string.Format("Maximum number of retires reached:{0}{1}", Environment.NewLine, result.ErrorMessage));
            }
          }
        };
  }
}