using System;
using System.Threading.Tasks;

namespace FunctionalCSharp
{
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

        #region Result
        /// <summary>
        /// Executes the given function on a disposable type created from a Result object
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="disposableCreateResult"></param>
        /// <param name="functionResult"></param>
        /// <returns></returns>
        public static Result UsingResult<TDisposable>(Func<Result<TDisposable>> disposableCreateResult, Func<TDisposable, Result> functionResult)
            where TDisposable : IDisposable
        {
            return disposableCreateResult()
                .Bind(disposable =>
                {
                    using (var d = disposable)
                        return functionResult(d);
                });
        }

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
                where TDisposable : IDisposable
        {
            return disposableCreateResult()
                .Bind(disposable =>
                {
                    using (var d = disposable)
                        return functionResult(d);
                });
        }        
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
