using FunctionalCSharp.FuncExtensions;
using System;
using System.Threading.Tasks;

namespace FunctionalCSharp.Results.ObjectExtensions
{
  /// <summary>
  ///
  /// </summary>
  public static class IDisposableExtensions
  {
    /// <summary>
    /// Executes the given function on the disposable type embedded in the extended Result object
    /// </summary>
    /// <typeparam name="TDisposable"></typeparam>
    /// <param name="this"></param>
    /// <param name="actionResult"></param>
    /// <returns></returns>
    public static IResult UsingResult<TDisposable>(this IResult<TDisposable> @this, Func<TDisposable, IResult> actionResult)
      where TDisposable : IDisposable =>
        @this
          .Bind(actionResult.Using());

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

    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    /// feels redundant with the UsingResult extension method, but that would return Result&lt;Task&lt;Result&gt;&gt;
    /// </remarks>
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
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="functionResultAsync"></param>
    /// <returns></returns>
    public static Task<IResult<TResult>> UsingResultAsync<TDisposable, TResult>(this IResult<TDisposable> @this, Func<TDisposable,
      Task<IResult<TResult>>> functionResultAsync) where TDisposable : IDisposable =>
        @this
            .BindAsync(functionResultAsync.UsingAsync());
  }
}