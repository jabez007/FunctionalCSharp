using System;
using System.Threading.Tasks;

namespace FunctionalCSharp
{
  /// <summary>
  /// 
  /// </summary>
  public static class FuncExceptionExtensions
  {
    #region 0 args

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <param name="catchFunction"></param>
    /// <returns></returns>
    public static Func<TResult> Catch<TResult, TException>(this Func<TResult> @this, Func<TException, TResult> catchFunction)
      where TException : Exception =>
        () =>
        {
          try
          {
            return @this();
          }
          catch (TException ex)
          {
            return catchFunction(ex);
          }
        };

    #endregion 0 args

    #region 1 arg

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <param name="catchFunction"></param>
    /// <returns></returns>
    public static Func<T1, TResult> Catch<T1, TResult, TException>(this Func<T1, TResult> @this, Func<TException, TResult> catchFunction)
      where TException : Exception =>
        (x) =>
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

    #endregion 1 arg

    #region 2 args

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <param name="catchFunction"></param>
    /// <returns></returns>
    public static Func<T1, T2, TResult> Catch<T1, T2, TResult, TException>(this Func<T1, T2, TResult> @this,
      Func<TException, TResult> catchFunction) where TException : Exception =>
        (x, y) =>
        {
          try
          {
            return @this(x, y);
          }
          catch (TException ex)
          {
            return catchFunction(ex);
          }
        };

    #endregion 2 args

    #region Async

    #region 0 args

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <param name="catchFunction"></param>
    /// <returns></returns>
    public static Func<Task<TResult>> CatchAsync<TResult, TException>(this Func<Task<TResult>> @this, Func<TException, TResult> catchFunction)
      where TException : Exception =>
        async () =>
        {
          try
          {
            return await @this();
          }
          catch (TException ex)
          {
            return catchFunction(ex);
          }
        };

    #endregion 0 args

    #region 1 arg

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <param name="catchFunction"></param>
    /// <returns></returns>
    public static Func<T1, Task<TResult>> CatchAsync<T1, TResult, TException>(this Func<T1, Task<TResult>> @this,
      Func<TException, TResult> catchFunction) where TException : Exception =>
        async (x) =>
        {
          try
          {
            return await @this(x);
          }
          catch (TException ex)
          {
            return catchFunction(ex);
          }
        };

    #endregion 1 arg

    #region 2 args

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <param name="catchFunction"></param>
    /// <returns></returns>
    public static Func<T1, T2, Task<TResult>> Catch<T1, T2, TResult, TException>(this Func<T1, T2, Task<TResult>> @this,
      Func<TException, TResult> catchFunction) where TException : Exception =>
        async (x, y) =>
        {
          try
          {
            return await @this(x, y);
          }
          catch (TException ex)
          {
            return catchFunction(ex);
          }
        };

    #endregion 2 args

    #endregion Async
  }
}
