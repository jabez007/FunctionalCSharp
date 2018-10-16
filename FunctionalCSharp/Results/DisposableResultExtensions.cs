using System;
using System.Threading.Tasks;

namespace FunctionalCSharp.Results
{
  /// <summary>
  ///
  /// </summary>
  public static class DisposableResultExtensions
  {
    #region IResult

    /// <summary>
    /// Executes the given function on the disposable type embedded in the extended Result object
    /// </summary>
    /// <typeparam name="TDisposable"></typeparam>
    /// <param name="this"></param>
    /// <param name="actionResult"></param>
    /// <returns></returns>
    public static IResult UsingResult<TDisposable>(this IResult<TDisposable> @this, Func<TDisposable, IResult> actionResult) where TDisposable : IDisposable =>
      @this
        .Bind(actionResult.Using());

    #endregion IResult

    #region IResult<TResult>

    /// <summary>
    /// Executes the given function on the disposable type embedded in the extended Result object
    /// </summary>
    /// <typeparam name="TDisposable"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="functionResult"></param>
    /// <returns></returns>
    public static IResult<TResult> UsingResult<TDisposable, TResult>(this IResult<TDisposable> @this, Func<TDisposable, IResult<TResult>> functionResult)
      where TDisposable : IDisposable =>
        @this
          .Bind(functionResult.Using());

    #endregion IResult<TResult>

    #region Async

    #region IResult

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TDisposable"></typeparam>
    /// <param name="this"></param>
    /// <param name="actionResult"></param>
    /// <returns></returns>
    public static async Task<IResult> UsingResultAsync<TDisposable>(this Task<IResult<TDisposable>> @this, Func<TDisposable, IResult> actionResult)
      where TDisposable : IDisposable =>
        (await @this)
          .UsingResult(actionResult);

    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    /// feels redundant with the UsingResult extension method, but that would return Result&lt;Task&lt;Result&gt;&gt;
    /// </remarks>
    /// <typeparam name="TDisposable"></typeparam>
    /// <param name="this"></param>
    /// <param name="actionResultAsync"></param>
    /// <returns></returns>
    public static Task<IResult> UsingResultAsync<TDisposable>(this IResult<TDisposable> @this, Func<TDisposable, Task<IResult>> actionResultAsync)
      where TDisposable : IDisposable =>
        @this
          .BindAsync(actionResultAsync.UsingAsync());

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TDisposable"></typeparam>
    /// <param name="this"></param>
    /// <param name="actionResultAsync"></param>
    /// <returns></returns>
    public static async Task<IResult> UsingResultAsync<TDisposable>(this Task<IResult<TDisposable>> @this, Func<TDisposable, Task<IResult>> actionResultAsync)
      where TDisposable : IDisposable =>
        await (await @this)
          .UsingResultAsync(actionResultAsync);

    #endregion IResult

    #region IResult<TResult>

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TDisposable"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="functionResult"></param>
    /// <returns></returns>
    public static async Task<IResult<TResult>> UsingResultAsync<TDisposable, TResult>(this Task<IResult<TDisposable>> @this,
        Func<TDisposable, IResult<TResult>> functionResult) where TDisposable : IDisposable =>
            (await @this)
                .UsingResult(functionResult);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TDisposable"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="functionResultAsync"></param>
    /// <returns></returns>
    public static Task<IResult<TResult>> UsingResultAsync<TDisposable, TResult>(this IResult<TDisposable> @this,
        Func<TDisposable, Task<IResult<TResult>>> functionResultAsync) where TDisposable : IDisposable =>
            @this
                .BindAsync(functionResultAsync.UsingAsync());

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TDisposable"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="functionResultAsync"></param>
    /// <returns></returns>
    public static async Task<IResult<TResult>> UsingResultAsync<TDisposable, TResult>(this Task<IResult<TDisposable>> @this,
        Func<TDisposable, Task<IResult<TResult>>> functionResultAsync) where TDisposable : IDisposable =>
            await (await @this).UsingResultAsync(functionResultAsync);

    #endregion IResult<TResult>

    #endregion Async
  }
}