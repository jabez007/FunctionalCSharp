using System;
using System.Threading.Tasks;

namespace FunctionalCSharp
{
    /// <summary>
    /// Static and extension methods for functionalizing the 'using' block
    /// </summary>
    public static class Disposable
    {
        #region Action<TDisposable>
        /// <summary>
        /// Executes the given action on the extended disposable object then disposes of the object
        /// </summary>
        /// <example>
        /// 
        /// </example>
        /// <typeparam name="TDisposable">A class the implements the IDisposable interface</typeparam>
        /// <param name="this"></param>
        /// <param name="action">An action or method to interact with your disposable instance</param>
        public static void Using<TDisposable>(this TDisposable @this, Action<TDisposable> action)
            where TDisposable : IDisposable
        {
            using (@this)
                action(@this);
        }

        /// <summary>
        /// Functionalizes the 'using' statement block
        /// </summary>
        /// <typeparam name="TDisposable">A class the implements the IDisposable interface</typeparam>
        /// <param name="disposableCreate">A function or method that will return an instance of your disposable type</param>
        /// <param name="action">An action or method to interact with your disposable instance</param>
        public static void Using<TDisposable>(Func<TDisposable> disposableCreate, Action<TDisposable> action)
            where TDisposable : IDisposable =>
                disposableCreate()
                    .Using(action);

        /// <summary>
        /// Uses the extended object as input for the function to create an instance of the disposable 
        /// then executes the given action on the disposable instance
        /// </summary>
        /// <typeparam name="TDisposableOptions"></typeparam>
        /// <typeparam name="TDisposable">A class the implements the IDisposable interface</typeparam>
        /// <param name="this"></param>
        /// <param name="disposableCreate">A function or method that will return an instance of your disposable type</param>
        /// <param name="action">An action or method to interact with your disposable instance</param>
        public static void Using<TDisposableOptions, TDisposable>(this TDisposableOptions @this,
            Func<TDisposableOptions, TDisposable> disposableCreate, Action<TDisposable> action)
                where TDisposable : IDisposable =>
                    disposableCreate(@this)
                        .Using(action);
        #endregion

        #region Func<TDisposable, TResult>
        /// <summary>
        /// Executes the given function on the extended disposable object then disposes of the object
        /// </summary>
        /// <example>
        /// 
        /// </example>
        /// <typeparam name="TDisposable">A class the implements the IDisposable interface</typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="function">A function or method to interact with your disposable instance and return a result</param>
        /// <returns></returns>
        public static TResult Using<TDisposable, TResult>(this TDisposable @this, Func<TDisposable, TResult> function)
            where TDisposable : IDisposable
        {
            using (@this)
                return function(@this);
        }

        /// <summary>
        /// Functionalizes the 'using' statement block
        /// </summary>
        /// <example>
        /// var content = Disposable.Using(
        ///     () => new FileStream("file.txt", FileMode.Open), 
        ///     stream =>
        ///     {
        ///         var b = new byte[stream.Length];
        ///         stream.Read(b, 0, (int) stream.Length);
        ///         return b;
        ///     }
        /// );
        /// </example>
        /// <typeparam name="TDisposable">A class the implements the IDisposable interface</typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="disposableCreate">A function or method that will return an instance of your disposable type</param>
        /// <param name="function">A function or method to interact with your disposable instance and return a result</param>
        /// <returns>The result of the given function acting on the created instance of the disposable</returns>
        public static TResult Using<TDisposable, TResult>(Func<TDisposable> disposableCreate, Func<TDisposable, TResult> function)
            where TDisposable : IDisposable =>
                disposableCreate()
                    .Using(function);

        /// <summary>
        /// Uses the extended object as input for the function to create an instance of the disposable 
        /// then executes the given function on the disposable instance
        /// </summary>
        /// <typeparam name="TDisposableOptions"></typeparam>
        /// <typeparam name="TDisposable">A class the implements the IDisposable interface</typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="disposableCreate">A function or method that will return an instance of your disposable type</param>
        /// <param name="function">A function or method to interact with your disposable instance and return a result</param>
        /// <returns></returns>
        public static TResult Using<TDisposableOptions, TDisposable, TResult>(this TDisposableOptions @this,
            Func<TDisposableOptions, TDisposable> disposableCreate, Func<TDisposable, TResult> function)
                where TDisposable : IDisposable =>
                    disposableCreate(@this)
                        .Using(function);
        #endregion

        #region Result
        /// <summary>
        /// Executes the given function on the disposable type embedded in the extended Result object
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="this"></param>
        /// <param name="actionResult"></param>
        /// <returns></returns>
        public static Result UsingResult<TDisposable>(this Result<TDisposable> @this, Func<TDisposable, Result> actionResult)
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
        public static Result UsingResult<TDisposable>(Func<Result<TDisposable>> disposableCreateResult, Func<TDisposable, Result> actionResult)
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
        public static Result UsingResult<TDisposableOptions, TDisposable>(this TDisposableOptions @this,
            Func<TDisposableOptions, Result<TDisposable>> disposableCreateResult, Func<TDisposable, Result> actionResult)
                where TDisposable : IDisposable =>
                    disposableCreateResult(@this)
                        .UsingResult(actionResult);
        #endregion

        #region Result<TResult>
        /// <summary>
        /// Executes the given function on the disposable type embedded in the extended Result object
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="functionResult"></param>
        /// <returns></returns>
        public static Result<TResult> UsingResult<TDisposable, TResult>(this Result<TDisposable> @this,
            Func<TDisposable, Result<TResult>> functionResult) where TDisposable : IDisposable =>
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
        public static Result<TResult> UsingResult<TDisposable, TResult>(Func<Result<TDisposable>> disposableCreateResult,
            Func<TDisposable, Result<TResult>> functionResult) where TDisposable : IDisposable =>
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
        public static Result<TResult> UsingResult<TDisposableOptions, TDisposable, TResult>(this TDisposableOptions @this,
            Func<TDisposableOptions, Result<TDisposable>> disposableCreateResult, Func<TDisposable, Result<TResult>> functionResult)
                where TDisposable : IDisposable =>
                    disposableCreateResult(@this)
                        .UsingResult(functionResult);
        #endregion

        #region Async

        #region Action
        /// <summary>
        /// Applies the Using extension method to a disposable type object embedded in a Task
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="this"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task UsingAsync<TDisposable>(this Task<TDisposable> @this, Action<TDisposable> action)
            where TDisposable : IDisposable =>
                (await @this)
                    .Using(action);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="disposableCreateAsync"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Task UsingAsync<TDisposable>(Func<Task<TDisposable>> disposableCreateAsync, Action<TDisposable> action)
            where TDisposable : IDisposable =>
                disposableCreateAsync()
                    .UsingAsync(action);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposableOptions"></typeparam>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="this"></param>
        /// <param name="disposableCreateAsync"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Task UsingAsync<TDisposableOptions, TDisposable>(this TDisposableOptions @this,
            Func<TDisposableOptions, Task<TDisposable>> disposableCreateAsync, Action<TDisposable> action)
                where TDisposable : IDisposable =>
                    disposableCreateAsync(@this)
                        .UsingAsync(action);

        /// <summary>
        /// Applies the Using extension method to a disposable type object embedded in a Task for an async Action
        /// </summary>
        /// <remarks>
        /// seems redundant with the above UsingAsync extension, but that would return Task&lt;Task&gt;
        /// </remarks>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="this"></param>
        /// <param name="actionAsync"></param>
        /// <returns></returns>
        public static async Task UsingAsync<TDisposable>(this Task<TDisposable> @this, Func<TDisposable, Task> actionAsync)
            where TDisposable : IDisposable =>
                await (await @this)
                    .Using(actionAsync);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="disposableCreateAsync"></param>
        /// <param name="actionAsync"></param>
        /// <returns></returns>
        public static Task UsingAsync<TDisposable>(Func<Task<TDisposable>> disposableCreateAsync, Func<TDisposable, Task> actionAsync)
            where TDisposable : IDisposable =>
                disposableCreateAsync()
                    .UsingAsync(actionAsync);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposableOptions"></typeparam>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="this"></param>
        /// <param name="disposableCreateAsync"></param>
        /// <param name="actionAsync"></param>
        /// <returns></returns>
        public static Task UsingAsync<TDisposableOptions, TDisposable>(this TDisposableOptions @this,
            Func<TDisposableOptions, Task<TDisposable>> disposableCreateAsync, Func<TDisposable, Task> actionAsync)
                where TDisposable : IDisposable =>
                    disposableCreateAsync(@this)
                        .UsingAsync(actionAsync);
        #endregion

        #region Func<TDisposable, TResult> and Func<TDisposable, Task<TResult>>
        /// <summary>
        /// Applies the Using extension method to a disposable type object embedded in a Task
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public static async Task<TResult> UsingAsync<TDisposable, TResult>(this Task<TDisposable> @this, Func<TDisposable, TResult> function)
            where TDisposable : IDisposable =>
                (await @this)
                    .Using(function);

        /// <summary>
        /// Allows async constructor for disposable type 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="disposableCreateAsync"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public static Task<TResult> UsingAsync<TDisposable, TResult>(Func<Task<TDisposable>> disposableCreateAsync,
            Func<TDisposable, TResult> function) where TDisposable : IDisposable =>
                    disposableCreateAsync()
                        .UsingAsync(function);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposableOptions"></typeparam>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="disposableCreateAsync"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public static Task<TResult> UsingAsync<TDisposableOptions, TDisposable, TResult>(this TDisposableOptions @this,
            Func<TDisposableOptions, Task<TDisposable>> disposableCreateAsync, Func<TDisposable, TResult> function)
                where TDisposable : IDisposable =>
                    disposableCreateAsync(@this)
                        .UsingAsync(function);

        /// <summary>
        /// Applies the Using extension method to a disposable type object embedded in a Task
        /// </summary>
        /// <remarks>
        /// seems redundant with the above UsingAsync extension, but that would return Task&lt;Task&lt;TResult&gt;&gt;
        /// </remarks>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="functionAsync"></param>
        /// <returns></returns>
        public static async Task<TResult> UsingAsync<TDisposable, TResult>(this Task<TDisposable> @this,
            Func<TDisposable, Task<TResult>> functionAsync) where TDisposable : IDisposable =>
                    await (await @this)
                        .Using(functionAsync);

        /// <summary>
        /// Allows async constructor for disposable type and async function inside 'using' block 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="disposableCreateAsync"></param>
        /// <param name="functionAsync"></param>
        /// <returns></returns>
        public static Task<TResult> UsingAsync<TDisposable, TResult>(Func<Task<TDisposable>> disposableCreateAsync,
            Func<TDisposable, Task<TResult>> functionAsync) where TDisposable : IDisposable =>
                    disposableCreateAsync()
                        .UsingAsync(functionAsync);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposableOptions"></typeparam>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="disposableCreateAsync"></param>
        /// <param name="functionAsync"></param>
        /// <returns></returns>
        public static Task<TResult> UsingAsync<TDisposableOptions, TDisposable, TResult>(this TDisposableOptions @this,
            Func<TDisposableOptions, Task<TDisposable>> disposableCreateAsync, Func<TDisposable, Task<TResult>> functionAsync)
                where TDisposable : IDisposable =>
                    disposableCreateAsync(@this)
                        .UsingAsync(functionAsync);
        #endregion

        #region Result
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="this"></param>
        /// <param name="actionResult"></param>
        /// <returns></returns>
        public static async Task<Result> UsingResultAsync<TDisposable>(this Task<Result<TDisposable>> @this,
            Func<TDisposable, Result> actionResult) where TDisposable : IDisposable =>
                (await @this)
                    .UsingResult(actionResult);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="disposableCreateResultAsync"></param>
        /// <param name="actionResult"></param>
        /// <returns></returns>
        public static Task<Result> UsingResultAsync<TDisposable>(Func<Task<Result<TDisposable>>> disposableCreateResultAsync,
            Func<TDisposable, Result> actionResult) where TDisposable : IDisposable =>
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
        public static Task<Result> UsingResultAsync<TDisposableOptions, TDisposable>(this TDisposableOptions @this,
            Func<TDisposableOptions, Task<Result<TDisposable>>> disposableCreateResultAsync, Func<TDisposable, Result> actionResult)
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
        public static Task<Result> UsingResultAsync<TDisposable>(this Result<TDisposable> @this,
            Func<TDisposable, Task<Result>> actionResultAsync) where TDisposable : IDisposable =>
                @this
                    .BindAsync(disposable => disposable.Using(actionResultAsync));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="disposableCreateResult"></param>
        /// <param name="actionResultAsync"></param>
        /// <returns></returns>
        public static Task<Result> UsingResultAsync<TDisposable>(Func<Result<TDisposable>> disposableCreateResult,
            Func<TDisposable, Task<Result>> actionResultAsync) where TDisposable : IDisposable =>
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
        public static Task<Result> UsingResultAsync<TDisposableOptions, TDisposable>(this TDisposableOptions @this,
            Func<TDisposableOptions, Result<TDisposable>> disposableCreateResult, Func<TDisposable, Task<Result>> actionResultAsync)
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
        public static async Task<Result> UsingResultAsync<TDisposable>(this Task<Result<TDisposable>> @this,
            Func<TDisposable, Task<Result>> actionResultAsync) where TDisposable : IDisposable =>
                await (await @this)
                    .UsingResultAsync(actionResultAsync);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="disposableCreateResultAsync"></param>
        /// <param name="actionResultAsync"></param>
        /// <returns></returns>
        public static Task<Result> UsingResultAsync<TDisposable>(Func<Task<Result<TDisposable>>> disposableCreateResultAsync,
            Func<TDisposable, Task<Result>> actionResultAsync) where TDisposable : IDisposable =>
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
        public static Task<Result> UsingResultAsync<TDisposableOptions, TDisposable>(this TDisposableOptions @this,
            Func<TDisposableOptions, Task<Result<TDisposable>>> disposableCreateResultAsync, Func<TDisposable, Task<Result>> actionResultAsync)
                where TDisposable : IDisposable =>
                    disposableCreateResultAsync(@this)
                        .UsingResultAsync(actionResultAsync);
        #endregion

        #region Result<TResult>
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="functionResult"></param>
        /// <returns></returns>
        public static async Task<Result<TResult>> UsingResultAsync<TDisposable, TResult>(this Task<Result<TDisposable>> @this,
            Func<TDisposable, Result<TResult>> functionResult) where TDisposable : IDisposable =>
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
        public static Task<Result<TResult>> UsingResultAsync<TDisposable, TResult>(Func<Task<Result<TDisposable>>> disposableCreateResultAsync,
            Func<TDisposable, Result<TResult>> functionResult) where TDisposable : IDisposable =>
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
        public static Task<Result<TResult>> UsingResultAsync<TDisposableOptions, TDisposable, TResult>(this TDisposableOptions @this,
            Func<TDisposableOptions, Task<Result<TDisposable>>> disposableCreateResultAsync, Func<TDisposable, Result<TResult>> functionResult)
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
        public static Task<Result<TResult>> UsingResultAsync<TDisposable, TResult>(this Result<TDisposable> @this,
            Func<TDisposable, Task<Result<TResult>>> functionResultAsync) where TDisposable : IDisposable =>
                @this
                    .BindAsync(disposable => disposable.Using(functionResultAsync));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="disposableCreateResult"></param>
        /// <param name="functionResultAsync"></param>
        /// <returns></returns>
        public static Task<Result<TResult>> UsingResultAsync<TDisposable, TResult>(Func<Result<TDisposable>> disposableCreateResult,
            Func<TDisposable, Task<Result<TResult>>> functionResultAsync) where TDisposable : IDisposable =>
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
        public static Task<Result<TResult>> UsingResultAsync<TDisposableOptions, TDisposable, TResult>(this TDisposableOptions @this,
            Func<TDisposableOptions, Result<TDisposable>> disposableCreateResult, Func<TDisposable, Task<Result<TResult>>> functionResultAsync)
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
        public static async Task<Result<TResult>> UsingResultAsync<TDisposable, TResult>(this Task<Result<TDisposable>> @this,
            Func<TDisposable, Task<Result<TResult>>> functionResultAsync) where TDisposable : IDisposable =>
                await (await @this)
                    .UsingResultAsync(functionResultAsync);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="disposableCreateResultAsync"></param>
        /// <param name="functionResultAsync"></param>
        /// <returns></returns>
        public static Task<Result<TResult>> UsingResultAsync<TDisposable, TResult>(Func<Task<Result<TDisposable>>> disposableCreateResultAsync,
            Func<TDisposable, Task<Result<TResult>>> functionResultAsync) where TDisposable : IDisposable =>
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
        public static Task<Result<TResult>> UsingResultAsync<TDisposableOptions, TDisposable, TResult>(this TDisposableOptions @this,
            Func<TDisposableOptions, Task<Result<TDisposable>>> disposableCreateResultAsync, Func<TDisposable, Task<Result<TResult>>> functionResultAsync)
                where TDisposable : IDisposable =>
                    disposableCreateResultAsync(@this)
                        .UsingResultAsync(functionResultAsync);
        #endregion

        #endregion
    }
}
