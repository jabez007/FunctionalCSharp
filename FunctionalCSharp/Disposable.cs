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

        #region Async

        #region Action and Func<TDisposable, Task>
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
        /// if the action is an async function, then we need to await it to keep the using block open
        /// </remarks>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="this"></param>
        /// <param name="actionAsync"></param>
        /// <returns></returns>
        public static async Task UsingAsync<TDisposable>(this Task<TDisposable> @this, Func<TDisposable, Task> actionAsync)
            where TDisposable : IDisposable
        {
            using (var disposable = await @this)
                await actionAsync(disposable);
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="this"></param>
        /// <param name="actionAsync"></param>
        /// <returns></returns>
        public static async Task UsingAsync<TDisposable>(this TDisposable @this, Func<TDisposable, Task> actionAsync)
            where TDisposable : IDisposable
        {
            using (@this)
                await actionAsync(@this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <param name="disposableCreateAsync"></param>
        /// <param name="actionAsync"></param>
        /// <returns></returns>
        public static Task UsingAsync<TDisposable>(Func<TDisposable> disposableCreateAsync, Func<TDisposable, Task> actionAsync)
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
            Func<TDisposableOptions, TDisposable> disposableCreateAsync, Func<TDisposable, Task> actionAsync)
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
        /// if the function is async, then we need to await it for the using block to stay open
        /// </remarks>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="functionAsync"></param>
        /// <returns></returns>
        public static async Task<TResult> UsingAsync<TDisposable, TResult>(this Task<TDisposable> @this,
            Func<TDisposable, Task<TResult>> functionAsync) where TDisposable : IDisposable
        {
            using (var disposable = await @this)
                return await functionAsync(disposable);
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="functionAsync"></param>
        /// <returns></returns>
        public static async Task<TResult> UsingAsync<TDisposable, TResult>(this TDisposable @this,
            Func<TDisposable, Task<TResult>> functionAsync) where TDisposable : IDisposable
        {
            using (@this)
                return await functionAsync(@this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="disposableCreateAsync"></param>
        /// <param name="functionAsync"></param>
        /// <returns></returns>
        public static Task<TResult> UsingAsync<TDisposable, TResult>(Func<TDisposable> disposableCreateAsync,
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
            Func<TDisposableOptions, TDisposable> disposableCreateAsync, Func<TDisposable, Task<TResult>> functionAsync)
                where TDisposable : IDisposable =>
                    disposableCreateAsync(@this)
                        .UsingAsync(functionAsync);
        #endregion

        #endregion
    }
}
