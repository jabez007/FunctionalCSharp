using System;
using System.Threading.Tasks;

namespace FunctionalCSharp
{
    /// <summary>
    /// Static and extension methods for functionalizing the 'using' block
    /// </summary>
    public static class Disposable
    {
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
            where TDisposable : IDisposable
        {
            using (var disposable = disposableCreate())
                return function(disposable);
        }

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
                where TDisposable : IDisposable
        {
            using (var disposable = disposableCreate(@this))
                return function(disposable);
        }

        /// <summary>
        /// Functionalizes the 'using' statement block
        /// </summary>
        /// <typeparam name="TDisposable">A class the implements the IDisposable interface</typeparam>
        /// <param name="disposableCreate">A function or method that will return an instance of your disposable type</param>
        /// <param name="action">An action or method to interact with your disposable instance</param>
        public static void Using<TDisposable>(Func<TDisposable> disposableCreate, Action<TDisposable> action)
            where TDisposable : IDisposable
        {
            using (var disposable = disposableCreate())
                action(disposable);
        }

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
                where TDisposable : IDisposable
        {
            using (var disposable = disposableCreate(@this))
                action(disposable);
        }

        #region Result
        /// <summary>
        /// Executes the given function on a disposable type created from a Result object
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="disposableCreateResult"></param>
        /// <param name="functionResult"></param>
        /// <returns></returns>
        public static Result UsingResult<TDisposable>(Func<Result<TDisposable>> disposableCreateResult, Func<TDisposable, Result> functionResult)
            where TDisposable : IDisposable =>
                disposableCreateResult()
                    .Bind(
                        disposable =>
                        {
                            using (disposable)
                                return functionResult(disposable);
                        }
                    );

        /// <summary>
        /// Uses the extended object as input for the function that returns an instance of the Result type with our disposable
        /// </summary>
        /// <typeparam name="TDisposableOptions"></typeparam>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="this"></param>
        /// <param name="disposableCreateResult"></param>
        /// <param name="functionResult"></param>
        /// <returns></returns>
        public static Result UsingResult<TDisposableOptions, TDisposable>(this TDisposableOptions @this,
            Func<TDisposableOptions, Result<TDisposable>> disposableCreateResult, Func<TDisposable, Result> functionResult)
                where TDisposable : IDisposable =>
                    disposableCreateResult(@this)
                        .Bind(
                            disposable =>
                            {
                                using (disposable)
                                    return functionResult(disposable);
                            }
                        );

        /// <summary>
        /// Executes the given function on a disposable type created from a Result object
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="disposableCreateResult"></param>
        /// <param name="functionResult"></param>
        /// <returns></returns>
        public static Result<TResult> UsingResult<TDisposable, TResult>(Func<Result<TDisposable>> disposableCreateResult, 
            Func<TDisposable, Result<TResult>> functionResult)
                where TDisposable : IDisposable =>
                    disposableCreateResult()
                        .Bind(
                            disposable =>
                            {
                                using (disposable)
                                    return functionResult(disposable);
                            }
                        );

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
                        .Bind(
                            disposable =>
                            {
                                using (disposable)
                                    return functionResult(disposable);
                            }
                        );
        #endregion

        #region Async
        /// <summary>
        /// Allows async functions inside the 'using' block 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="disposableCreate"></param>
        /// <param name="asyncFunction"></param>
        /// <returns></returns>
        public static async Task<TResult> UsingAsync<TDisposable, TResult>(Func<TDisposable> disposableCreate, 
            Func<TDisposable, Task<TResult>> asyncFunction)
                where TDisposable : IDisposable
        {
            using (var disposable = disposableCreate())
                return await asyncFunction(disposable);
        }

        /// <summary>
        /// Allows async constructor for disposable type 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="disposableAsyncCreate"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public static async Task<TResult> UsingAsync<TDisposable, TResult>(Func<Task<TDisposable>> disposableAsyncCreate, 
            Func<TDisposable, TResult> function)
                where TDisposable : IDisposable
        {
            using (var disposable = await disposableAsyncCreate())
                return function(disposable);
        }

        /// <summary>
        /// Allows async constructor for disposable type and async function inside 'using' block 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="disposableAsyncCreate"></param>
        /// <param name="asyncFunction"></param>
        /// <returns></returns>
        public static async Task<TResult> UsingAsync<TDisposable, TResult>(Func<Task<TDisposable>> disposableAsyncCreate, 
            Func<TDisposable, Task<TResult>> asyncFunction)
                where TDisposable : IDisposable
        {
            using (var disposable = await disposableAsyncCreate())
                return await asyncFunction(disposable);
        }
        #endregion

        #region Async Result
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="disposableAsyncCreateResult"></param>
        /// <param name="functionResult"></param>
        /// <returns></returns>
        public static async Task<Result> UsingAsyncResult<TDisposable>(Func<Task<Result<TDisposable>>> disposableAsyncCreateResult, 
            Func<TDisposable, Result> functionResult)
                where TDisposable : IDisposable
        {
            return (await disposableAsyncCreateResult())
                .Bind(disposable =>
                {
                    using (var d = disposable)
                        return functionResult(d);
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="disposableCreateResult"></param>
        /// <param name="asyncFunctionResult"></param>
        /// <returns></returns>
        public static Task<Result> UsingAsyncResult<TDisposable>(Func<Result<TDisposable>> disposableCreateResult, 
            Func<TDisposable, Task<Result>> asyncFunctionResult)
                where TDisposable : IDisposable
        {
            return disposableCreateResult()
                .BindAsync(async disposable =>
                {
                    using (var d = disposable)
                        return await asyncFunctionResult(d);
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="disposableAsyncCreateResult"></param>
        /// <param name="asyncFunctionResult"></param>
        /// <returns></returns>
        public static async Task<Result> UsingAsyncResult<TDisposable>(Func<Task<Result<TDisposable>>> disposableAsyncCreateResult, 
            Func<TDisposable, Task<Result>> asyncFunctionResult)
                where TDisposable : IDisposable
        {
            return await (await disposableAsyncCreateResult())
                .BindAsync(async disposable =>
                {
                    using (var d = disposable)
                        return await asyncFunctionResult(d);
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="disposableAsyncCreateResult"></param>
        /// <param name="functionResult"></param>
        /// <returns></returns>
        public static async Task<Result<TResult>> UsingAsyncResult<TDisposable, TResult>(Func<Task<Result<TDisposable>>> disposableAsyncCreateResult, 
            Func<TDisposable, Result<TResult>> functionResult)
                where TDisposable : IDisposable
        {
            return (await disposableAsyncCreateResult())
                .Bind(disposable =>
                {
                    using (var d = disposable)
                        return functionResult(d);
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="disposableCreateResult"></param>
        /// <param name="asyncFunctionResult"></param>
        /// <returns></returns>
        public static Task<Result<TResult>> UsingAsyncResult<TDisposable, TResult>(Func<Result<TDisposable>> disposableCreateResult, 
            Func<TDisposable, Task<Result<TResult>>> asyncFunctionResult)
                where TDisposable : IDisposable
        {
            return disposableCreateResult()
                .BindAsync(async disposable => 
                {
                    using (var d = disposable)
                        return await asyncFunctionResult(d);
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="disposableAsyncCreateResult"></param>
        /// <param name="asyncFunctionResult"></param>
        /// <returns></returns>
        public static async Task<Result<TResult>> UsingAsyncResult<TDisposable, TResult>(Func<Task<Result<TDisposable>>> disposableAsyncCreateResult, 
            Func<TDisposable, Task<Result<TResult>>> asyncFunctionResult)
                where TDisposable : IDisposable
        {
            return await (await disposableAsyncCreateResult())
                .BindAsync(async disposable =>
                {
                    using (var d = disposable)
                        return await asyncFunctionResult(d);
                });
        }
        #endregion
    }
}
