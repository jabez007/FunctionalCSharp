using System;
using System.Threading;
using System.Threading.Tasks;

namespace FunctionalCSharp.FuncExtensions
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
    public static Func<int, Func<TimeSpan, Func<T, TResult>>> RetryOnException<T, TResult, TException>(this Func<T, TResult> @this)
      where TException : Exception
    {
      return (maxAttempts) => (sleepBetween) => (input) =>
      {
        if (maxAttempts > 1)
        {
          return @this.Catch<T, TResult, TException>()(ex =>
          {
            Thread.Sleep(sleepBetween);
            return RetryOnException<T, TResult, TException>(@this)(maxAttempts - 1)(sleepBetween)(input);
          }
          )(input);
        }
        else
        {
          return @this(input);
        }
      };
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<int, Func<TimeSpan, Func<T, Task<TResult>>>> RetryOnExceptionAsync<T, TResult, TException>(this Func<T, Task<TResult>> @this)
      where TException : Exception
    {
      return (maxAttempts) => (sleepBetween) => async (input) =>
      {
        if (maxAttempts > 1)
        {
          return await @this.CatchAsync<T, TResult, TException>()(ex =>
          {
            Thread.Sleep(sleepBetween);
            return RetryOnExceptionAsync<T, TResult, TException>(@this)(maxAttempts - 1)(sleepBetween)(input);
          }
          )(input);
        }
        else
        {
          return await @this(input);
        }
      };
    }
  }
}