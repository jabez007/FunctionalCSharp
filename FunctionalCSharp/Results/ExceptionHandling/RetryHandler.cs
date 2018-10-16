using System;
using System.Threading;
using System.Threading.Tasks;

namespace FunctionalCSharp.Results.ExceptionHandling
{
  /// <summary>
  ///
  /// </summary>
  public static class RetryHandler
  {
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <param name="numberOfRetries"></param>
    /// <param name="timeBetweenRetries"></param>
    /// <returns></returns>
    public static Func<T1, IResult<TResult>> RetryOnException<T1, TResult, TException>(this Func<T1, TResult> @this, int numberOfRetries = 3,
      TimeSpan timeBetweenRetries = default) where TException : Exception =>
        (x) =>
        {
          var attempt = 0;
          string errorMessage = "";
          do
          {
            attempt++;
            if (attempt > numberOfRetries)
            {
              return Result<TResult>.Failure("Maximum number of retires({1}) reached:{0}{2}".Format(Environment.NewLine, numberOfRetries, errorMessage));
            }

            var result = @this.Catch<T1, TResult, TException>()(x);
            if (result.IsSuccess)
            {
              return result;
            }
            else
            {
              errorMessage = result.ErrorMessage;
            }

            Thread.Sleep(timeBetweenRetries);
          } while (true);
        };

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <param name="numberOfRetries"></param>
    /// <param name="timeBetweenRetries"></param>
    /// <returns></returns>
    public static Func<T1, Task<IResult<TResult>>> RetryOnExceptionAsync<T1, TResult, TException>(this Func<T1, Task<TResult>> @this, int numberOfRetries = 3,
      TimeSpan timeBetweenRetries = default) where TException : Exception =>
        async (x) =>
        {
          var attempt = 0;
          string errorMessage = "";
          do
          {
            attempt++;
            if (attempt > numberOfRetries)
            {
              return Result<TResult>.Failure("Maximum number of retires({1}) reached:{0}{2}".Format(Environment.NewLine, numberOfRetries, errorMessage));
            }

            var result = await @this.CatchAsync<T1, TResult, TException>()(x);
            if (result.IsSuccess)
            {
              return result;
            }
            else
            {
              errorMessage = result.ErrorMessage;
            }

            Thread.Sleep(timeBetweenRetries);
          } while (true);
        };
  }
}