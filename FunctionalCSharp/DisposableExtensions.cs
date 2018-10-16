using System;
using System.Threading.Tasks;

namespace FunctionalCSharp
{
  /// <summary>
  /// Static and extension methods for functionalizing the 'using' block
  /// </summary>
  public static class DisposableExtensions
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
      {
        action(@this);
      }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TDisposable"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Action<TDisposable> Using<TDisposable>(this Action<TDisposable> @this) where TDisposable : IDisposable =>
      disposable => disposable.Using(@this);

    #endregion Action<TDisposable>

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
      {
        return function(@this);
      }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TDisposable"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<TDisposable, TResult> Using<TDisposable, TResult>(this Func<TDisposable, TResult> @this) where TDisposable : IDisposable =>
      disposable => disposable.Using(@this);

    #endregion Func<TDisposable, TResult>

    #region Async

    #region Action and Func<TDisposable, Task>

    /// <summary>
    /// Applies the Using extension method to a disposable type object embedded in a Task
    /// </summary>
    /// <typeparam name="TDisposable"></typeparam>
    /// <param name="this"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static async Task UsingAsync<TDisposable>(this Task<TDisposable> @this, Action<TDisposable> action) where TDisposable : IDisposable =>
      action.Using()(await @this);

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
    public static async Task UsingAsync<TDisposable>(this Task<TDisposable> @this, Func<TDisposable, Task> actionAsync) where TDisposable : IDisposable =>
      await actionAsync.Using()(await @this);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TDisposable"></typeparam>
    /// <param name="this"></param>
    /// <param name="actionAsync"></param>
    /// <returns></returns>
    public static async Task UsingAsync<TDisposable>(this TDisposable @this, Func<TDisposable, Task> actionAsync) where TDisposable : IDisposable =>
      await actionAsync.Using()(@this);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TDisposable"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<TDisposable, Task> UsingAsync<TDisposable>(this Func<TDisposable, Task> @this) where TDisposable : IDisposable =>
      disposable => disposable.UsingAsync(@this);

    #endregion Action and Func<TDisposable, Task>

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
        function.Using()(await @this);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TDisposable"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="functionAsync"></param>
    /// <returns></returns>
    public static async Task<TResult> UsingAsync<TDisposable, TResult>(this TDisposable @this, Func<TDisposable, Task<TResult>> functionAsync)
      where TDisposable : IDisposable
    {
      using (@this)
      {
        return await functionAsync(@this);
      }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TDisposable"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<TDisposable, Task<TResult>> UsingAsync<TDisposable, TResult>(this Func<TDisposable, Task<TResult>> @this)
      where TDisposable : IDisposable =>
        disposable => disposable.UsingAsync(@this);

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
    public static async Task<TResult> UsingAsync<TDisposable, TResult>(this Task<TDisposable> @this, Func<TDisposable, Task<TResult>> functionAsync)
      where TDisposable : IDisposable =>
        await functionAsync.Using()(await @this);

    #endregion Func<TDisposable, TResult> and Func<TDisposable, Task<TResult>>

    #endregion Async
  }
}