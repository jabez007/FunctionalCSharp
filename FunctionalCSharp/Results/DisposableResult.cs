using System;
using System.Threading.Tasks;

namespace FunctionalCSharp.Results
{
    /// <summary>
    /// 
    /// </summary>
    public static class DisposableResult
    {
        #region IResult
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
                    .Bind(disposable => disposable.Using(actionResult));

        /// <summary>
        /// Executes the given function on a disposable type created from a Result object
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="disposableCreateResult"></param>
        /// <param name="actionResult"></param>
        /// <returns></returns>
        public static IResult UsingResult<TDisposable>(Func<IResult<TDisposable>> disposableCreateResult, Func<TDisposable, IResult> actionResult)
            where TDisposable : IDisposable =>
                disposableCreateResult()
                    .UsingResult(actionResult);

        /// <summary>
        /// Uses the extended object as input for the function that returns an instance of the Result type with our disposable
        /// </summary>
        /// <typeparam name="TDisposableOptions"></typeparam>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="this"></param>
        /// <param name="disposableCreateResult"></param>
        /// <param name="actionResult"></param>
        /// <returns></returns>
        public static IResult UsingResult<TDisposableOptions, TDisposable>(this TDisposableOptions @this,
            Func<TDisposableOptions, IResult<TDisposable>> disposableCreateResult, Func<TDisposable, IResult> actionResult)
                where TDisposable : IDisposable =>
                    disposableCreateResult(@this)
                        .UsingResult(actionResult);
        #endregion

        #region IResult<TResult>
        /// <summary>
        /// Executes the given function on the disposable type embedded in the extended Result object
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="functionResult"></param>
        /// <returns></returns>
        public static IResult<TResult> UsingResult<TDisposable, TResult>(this IResult<TDisposable> @this,
            Func<TDisposable, IResult<TResult>> functionResult) where TDisposable : IDisposable =>
                    @this
                        .Bind(disposable => disposable.Using(functionResult));

        /// <summary>
        /// Executes the given function on a disposable type created from a Result object
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="disposableCreateResult"></param>
        /// <param name="functionResult"></param>
        /// <returns></returns>
        public static IResult<TResult> UsingResult<TDisposable, TResult>(Func<IResult<TDisposable>> disposableCreateResult,
            Func<TDisposable, IResult<TResult>> functionResult) where TDisposable : IDisposable =>
                    disposableCreateResult()
                        .UsingResult(functionResult);

        /// <summary>
        /// Uses the extended object as input for the function that returns an instance of the Result type with our disposable
        /// </summary>
        /// <typeparam name="TDisposableOptions"></typeparam>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="disposableCreateResult"></param>
        /// <param name="functionResult"></param>
        /// <returns></returns>
        public static IResult<TResult> UsingResult<TDisposableOptions, TDisposable, TResult>(this TDisposableOptions @this,
            Func<TDisposableOptions, IResult<TDisposable>> disposableCreateResult, Func<TDisposable, IResult<TResult>> functionResult)
                where TDisposable : IDisposable =>
                    disposableCreateResult(@this)
                        .UsingResult(functionResult);
        #endregion

        #region Async

        #region IResult
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="this"></param>
        /// <param name="actionResult"></param>
        /// <returns></returns>
        public static async Task<IResult> UsingResultAsync<TDisposable>(this Task<IResult<TDisposable>> @this,
            Func<TDisposable, IResult> actionResult) where TDisposable : IDisposable =>
                (await @this)
                    .UsingResult(actionResult);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="disposableCreateResultAsync"></param>
        /// <param name="actionResult"></param>
        /// <returns></returns>
        public static Task<IResult> UsingResultAsync<TDisposable>(Func<Task<IResult<TDisposable>>> disposableCreateResultAsync,
            Func<TDisposable, IResult> actionResult) where TDisposable : IDisposable =>
                disposableCreateResultAsync()
                    .UsingResultAsync(actionResult);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposableOptions"></typeparam>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="this"></param>
        /// <param name="disposableCreateResultAsync"></param>
        /// <param name="actionResult"></param>
        /// <returns></returns>
        public static Task<IResult> UsingResultAsync<TDisposableOptions, TDisposable>(this TDisposableOptions @this,
            Func<TDisposableOptions, Task<IResult<TDisposable>>> disposableCreateResultAsync, Func<TDisposable, IResult> actionResult)
                where TDisposable : IDisposable =>
                    disposableCreateResultAsync(@this)
                        .UsingResultAsync(actionResult);

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
        public static Task<IResult> UsingResultAsync<TDisposable>(this IResult<TDisposable> @this,
            Func<TDisposable, Task<IResult>> actionResultAsync) where TDisposable : IDisposable =>
                @this
                    .BindAsync(disposable => disposable.UsingAsync(actionResultAsync));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="disposableCreateResult"></param>
        /// <param name="actionResultAsync"></param>
        /// <returns></returns>
        public static Task<IResult> UsingResultAsync<TDisposable>(Func<IResult<TDisposable>> disposableCreateResult,
            Func<TDisposable, Task<IResult>> actionResultAsync) where TDisposable : IDisposable =>
                disposableCreateResult()
                    .UsingResultAsync(actionResultAsync);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposableOptions"></typeparam>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="this"></param>
        /// <param name="disposableCreateResult"></param>
        /// <param name="actionResultAsync"></param>
        /// <returns></returns>
        public static Task<IResult> UsingResultAsync<TDisposableOptions, TDisposable>(this TDisposableOptions @this,
            Func<TDisposableOptions, IResult<TDisposable>> disposableCreateResult, Func<TDisposable, Task<IResult>> actionResultAsync)
                where TDisposable : IDisposable =>
                    disposableCreateResult(@this)
                        .UsingResultAsync(actionResultAsync);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="this"></param>
        /// <param name="actionResultAsync"></param>
        /// <returns></returns>
        public static async Task<IResult> UsingResultAsync<TDisposable>(this Task<IResult<TDisposable>> @this,
            Func<TDisposable, Task<IResult>> actionResultAsync) where TDisposable : IDisposable =>
                await (await @this).UsingResultAsync(actionResultAsync);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="disposableCreateResultAsync"></param>
        /// <param name="actionResultAsync"></param>
        /// <returns></returns>
        public static Task<IResult> UsingResultAsync<TDisposable>(Func<Task<IResult<TDisposable>>> disposableCreateResultAsync,
            Func<TDisposable, Task<IResult>> actionResultAsync) where TDisposable : IDisposable =>
                disposableCreateResultAsync()
                    .UsingResultAsync(actionResultAsync);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposableOptions"></typeparam>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="this"></param>
        /// <param name="disposableCreateResultAsync"></param>
        /// <param name="actionResultAsync"></param>
        /// <returns></returns>
        public static Task<IResult> UsingResultAsync<TDisposableOptions, TDisposable>(this TDisposableOptions @this,
            Func<TDisposableOptions, Task<IResult<TDisposable>>> disposableCreateResultAsync, Func<TDisposable, Task<IResult>> actionResultAsync)
                where TDisposable : IDisposable =>
                    disposableCreateResultAsync(@this)
                        .UsingResultAsync(actionResultAsync);
        #endregion

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
        /// <param name="disposableCreateResultAsync"></param>
        /// <param name="functionResult"></param>
        /// <returns></returns>
        public static Task<IResult<TResult>> UsingResultAsync<TDisposable, TResult>(Func<Task<IResult<TDisposable>>> disposableCreateResultAsync,
            Func<TDisposable, IResult<TResult>> functionResult) where TDisposable : IDisposable =>
                disposableCreateResultAsync()
                    .UsingResultAsync(functionResult);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposableOptions"></typeparam>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="disposableCreateResultAsync"></param>
        /// <param name="functionResult"></param>
        /// <returns></returns>
        public static Task<IResult<TResult>> UsingResultAsync<TDisposableOptions, TDisposable, TResult>(this TDisposableOptions @this,
            Func<TDisposableOptions, Task<IResult<TDisposable>>> disposableCreateResultAsync, Func<TDisposable, IResult<TResult>> functionResult)
                where TDisposable : IDisposable =>
                    disposableCreateResultAsync(@this)
                        .UsingResultAsync(functionResult);

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
                    .BindAsync(disposable => disposable.UsingAsync(functionResultAsync));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="disposableCreateResult"></param>
        /// <param name="functionResultAsync"></param>
        /// <returns></returns>
        public static Task<IResult<TResult>> UsingResultAsync<TDisposable, TResult>(Func<IResult<TDisposable>> disposableCreateResult,
            Func<TDisposable, Task<IResult<TResult>>> functionResultAsync) where TDisposable : IDisposable =>
                disposableCreateResult()
                    .UsingResultAsync(functionResultAsync);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposableOptions"></typeparam>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="disposableCreateResult"></param>
        /// <param name="functionResultAsync"></param>
        /// <returns></returns>
        public static Task<IResult<TResult>> UsingResultAsync<TDisposableOptions, TDisposable, TResult>(this TDisposableOptions @this,
            Func<TDisposableOptions, IResult<TDisposable>> disposableCreateResult, Func<TDisposable, Task<IResult<TResult>>> functionResultAsync)
                where TDisposable : IDisposable =>
                    disposableCreateResult(@this)
                        .UsingResultAsync(functionResultAsync);

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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="disposableCreateResultAsync"></param>
        /// <param name="functionResultAsync"></param>
        /// <returns></returns>
        public static Task<IResult<TResult>> UsingResultAsync<TDisposable, TResult>(Func<Task<IResult<TDisposable>>> disposableCreateResultAsync,
            Func<TDisposable, Task<IResult<TResult>>> functionResultAsync) where TDisposable : IDisposable =>
                disposableCreateResultAsync()
                    .UsingResultAsync(functionResultAsync);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposableOptions"></typeparam>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="disposableCreateResultAsync"></param>
        /// <param name="functionResultAsync"></param>
        /// <returns></returns>
        public static Task<IResult<TResult>> UsingResultAsync<TDisposableOptions, TDisposable, TResult>(this TDisposableOptions @this,
            Func<TDisposableOptions, Task<IResult<TDisposable>>> disposableCreateResultAsync, Func<TDisposable, Task<IResult<TResult>>> functionResultAsync)
                where TDisposable : IDisposable =>
                    disposableCreateResultAsync(@this)
                        .UsingResultAsync(functionResultAsync);
        #endregion

        #endregion
    }
}
