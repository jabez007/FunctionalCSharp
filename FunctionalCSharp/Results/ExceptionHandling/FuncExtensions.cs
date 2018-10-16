using System;
using System.Threading.Tasks;

namespace FunctionalCSharp.Results.ExceptionHandling
{
  /// <summary>
  ///
  /// </summary>
  public static class FuncExtensions
  {
    #region 0 args

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<IResult<TResult>> Catch<TResult, TException>(this Func<TResult> @this) where TException : Exception =>
      Catch<TResult, TException>(() => Result<TResult>.Success(@this()));

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<IResult<TResult>> Catch<TResult, TException>(this Func<IResult<TResult>> @this) where TException : Exception =>
      @this.Catch<TResult, TException>(ex => Result<TResult>.Failure(ex));

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <param name="catchFunction"></param>
    /// <returns></returns>
    public static Func<IResult<TResult>> Catch<TResult, TException>(this Func<IResult<TResult>> @this, Func<TException, IResult<TResult>> catchFunction)
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
    /// <returns></returns>
    public static Func<T1, IResult<TResult>> Catch<T1, TResult, TException>(this Func<T1, TResult> @this) where TException : Exception =>
      Catch<T1, TResult, TException>((x) => Result<TResult>.Success(@this(x)));

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<T1, IResult<TResult>> Catch<T1, TResult, TException>(this Func<T1, IResult<TResult>> @this) where TException : Exception =>
      @this.Catch<T1, TResult, TException>(ex => Result<TResult>.Failure(ex));

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <param name="catchFunction"></param>
    /// <returns></returns>
    public static Func<T1, IResult<TResult>> Catch<T1, TResult, TException>(this Func<T1, IResult<TResult>> @this, 
      Func<TException, IResult<TResult>> catchFunction) where TException : Exception =>
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
    /// <returns></returns>
    public static Func<T1, T2, IResult<TResult>> Catch<T1, T2, TResult, TException>(this Func<T1, T2, TResult> @this) where TException : Exception =>
      Catch<T1, T2, TResult, TException>((x, y) => Result<TResult>.Success(@this(x, y)));

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<T1, T2, IResult<TResult>> Catch<T1, T2, TResult, TException>(this Func<T1, T2, IResult<TResult>> @this) where TException : Exception =>
      @this.Catch<T1, T2, TResult, TException>(ex => Result<TResult>.Failure(ex));

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
    public static Func<T1, T2, IResult<TResult>> Catch<T1, T2, TResult, TException>(this Func<T1, T2, IResult<TResult>> @this,
      Func<TException, IResult<TResult>> catchFunction) where TException : Exception =>
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
    /// <returns></returns>
    public static Func<Task<IResult<TResult>>> CatchAsync<TResult, TException>(this Func<Task<TResult>> @this) where TException : Exception =>
      CatchAsync<TResult, TException>(async () => Result<TResult>.Success(await @this()));

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<Task<IResult<TResult>>> CatchAsync<TResult, TException>(this Func<Task<IResult<TResult>>> @this) where TException : Exception =>
      @this.CatchAsync<TResult, TException>(ex => Result<TResult>.Failure(ex));

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <param name="catchFunction"></param>
    /// <returns></returns>
    public static Func<Task<IResult<TResult>>> CatchAsync<TResult, TException>(this Func<Task<IResult<TResult>>> @this, 
      Func<TException, IResult<TResult>> catchFunction) where TException : Exception =>
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
    /// <returns></returns>
    public static Func<T1, Task<IResult<TResult>>> CatchAsync<T1, TResult, TException>(this Func<T1, Task<TResult>> @this) where TException : Exception =>
      CatchAsync<T1, TResult, TException>(async (x) => Result<TResult>.Success(await @this(x)));

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<T1, Task<IResult<TResult>>> CatchAsync<T1, TResult, TException>(this Func<T1, Task<IResult<TResult>>> @this) 
      where TException : Exception =>
        @this.CatchAsync<T1, TResult, TException>(ex => Result<TResult>.Failure(ex));

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <param name="catchFunction"></param>
    /// <returns></returns>
    public static Func<T1, Task<IResult<TResult>>> CatchAsync<T1, TResult, TException>(this Func<T1, Task<IResult<TResult>>> @this,
      Func<TException, IResult<TResult>> catchFunction) where TException : Exception =>
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
    /// <returns></returns>
    public static Func<T1, T2, Task<IResult<TResult>>> CatchAsync<T1, T2, TResult, TException>(this Func<T1, T2, Task<TResult>> @this)
      where TException : Exception =>
        CatchAsync<T1, T2, TResult, TException>(async (x, y) => Result<TResult>.Success(await @this(x, y)));

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<T1, T2, Task<IResult<TResult>>> CatchAsync<T1, T2, TResult, TException>(this Func<T1, T2, Task<IResult<TResult>>> @this)
      where TException : Exception =>
        @this.CatchAsync<T1, T2, TResult, TException>(ex => Result<TResult>.Failure(ex));

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
    public static Func<T1, T2, Task<IResult<TResult>>> CatchAsync<T1, T2, TResult, TException>(this Func<T1, T2, Task<IResult<TResult>>> @this,
      Func<TException, IResult<TResult>> catchFunction) where TException : Exception =>
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