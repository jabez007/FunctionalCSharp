using System;
using System.Threading.Tasks;

namespace FunctionalCSharp.FuncExtensions
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
    public static Func<Func<TException, TResult>, Func<T, TResult>> Catch<T, TResult, TException>(this Func<T, TResult> @this)
      where TException : Exception =>
        (catchFunction) => (x) =>
        {
          try
          {
            return @this(x);
          }
          catch (TException ex)
          {
            return catchFunction(ex);
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
    public static Func<Func<TException, Task<TResult>>, Func<T, Task<TResult>>> CatchAsync<T, TResult, TException>(this Func<T, Task<TResult>> @this)
      where TException : Exception =>
        (catchFunction) => async (x) =>
        {
          try
          {
            return await @this(x);
          }
          catch (TException ex)
          {
            return await catchFunction(ex);
          }
        };
  }
}