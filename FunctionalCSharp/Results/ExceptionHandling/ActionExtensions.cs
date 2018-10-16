using System;
using System.Threading.Tasks;

namespace FunctionalCSharp.Results.ExceptionHandling
{
  /// <summary>
  ///
  /// </summary>
  public static class ActionExtensions
  {
    #region 0 args

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<IResult> Catch<TException>(this Action @this) where TException : Exception =>
      () =>
      {
        try
        {
          @this();
          return Result.Success();
        }
        catch (TException ex)
        {
          return Result.Failure(ex);
        }
      };

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <param name="catchFunction"></param>
    /// <returns></returns>
    public static Func<IResult> Catch<TException>(this Action @this, Func<TException, IResult> catchFunction) where TException : Exception =>
      () =>
      {
        try
        {
          @this();
          return Result.Success();
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
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<T1, IResult> Catch<T1, TException>(this Action<T1> @this) where TException : Exception =>
      (x) =>
      {
        try
        {
          @this(x);
          return Result.Success();
        }
        catch (TException ex)
        {
          return Result.Failure(ex);
        }
      };

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <param name="catchFunction"></param>
    /// <returns></returns>
    public static Func<T1, IResult> Catch<T1, TException>(this Action<T1> @this, Func<TException, IResult> catchFunction) where TException : Exception =>
      (x) =>
      {
        try
        {
          @this(x);
          return Result.Success();
        }
        catch (TException ex)
        {
          return catchFunction(ex);
        }
      };

    #endregion 1 arg

    #region 2 arg

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<T1, T2, IResult> Catch<T1, T2, TException>(this Action<T1, T2> @this) where TException : Exception =>
      (x, y) =>
      {
        try
        {
          @this(x, y);
          return Result.Success();
        }
        catch (TException ex)
        {
          return Result.Failure(ex);
        }
      };

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <param name="catchFunction"></param>
    /// <returns></returns>
    public static Func<T1, T2, IResult> Catch<T1, T2, TException>(this Action<T1, T2> @this, Func<TException, IResult> catchFunction)
      where TException : Exception =>
        (x, y) =>
        {
          try
          {
            @this(x, y);
            return Result.Success();
          }
          catch (TException ex)
          {
            return catchFunction(ex);
          }
        };

    #endregion 2 arg

    #region Async (Func<Task>)

    #region 0 args

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<Task<IResult>> CatchAsync<TException>(this Func<Task> @this) where TException : Exception =>
      async () =>
      {
        try
        {
          await @this();
          return Result.Success();
        }
        catch (TException ex)
        {
          return Result.Failure(ex);
        }
      };

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <param name="catchFunction"></param>
    /// <returns></returns>
    public static Func<Task<IResult>> CatchAsync<TException>(this Func<Task> @this, Func<TException, IResult> catchFunction) where TException : Exception =>
      async () =>
      {
        try
        {
          await @this();
          return Result.Success();
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
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<T1, Task<IResult>> CatchAsync<T1, TException>(this Func<T1, Task> @this) where TException : Exception =>
      async (x) =>
      {
        try
        {
          await @this(x);
          return Result.Success();
        }
        catch (TException ex)
        {
          return Result.Failure(ex);
        }
      };

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <param name="catchFunction"></param>
    /// <returns></returns>
    public static Func<T1, Task<IResult>> CatchAsync<T1, TException>(this Func<T1, Task> @this, Func<TException, IResult> catchFunction)
      where TException : Exception =>
      async (x) =>
      {
        try
        {
          await @this(x);
          return Result.Success();
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
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<T1, T2, Task<IResult>> CatchAsync<T1, T2, TException>(this Func<T1, T2, Task> @this) where TException : Exception =>
      async (x, y) =>
      {
        try
        {
          await @this(x, y);
          return Result.Success();
        }
        catch (TException ex)
        {
          return Result.Failure(ex);
        }
      };

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="this"></param>
    /// <param name="catchFunction"></param>
    /// <returns></returns>
    public static Func<T1, T2, Task<IResult>> CatchAsync<T1, T2, TException>(this Func<T1, T2, Task> @this, Func<TException, IResult> catchFunction)
      where TException : Exception =>
        async (x, y) =>
        {
          try
          {
            await @this(x, y);
            return Result.Success();
          }
          catch (TException ex)
          {
            return catchFunction(ex);
          }
        };

    #endregion 2 args

    #endregion Async (Func<Task>)
  }
}