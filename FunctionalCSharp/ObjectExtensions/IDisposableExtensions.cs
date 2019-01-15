using System;
using System.Threading.Tasks;

namespace FunctionalCSharp.ObjectExtensions
{
  /// <summary>
  /// Static and extension methods for functionalizing the 'using' block
  /// </summary>
  public static class IDisposableExtensions
  {
    /// <summary>
    /// Executes the given action on the extended disposable object then disposes of the object
    /// </summary>
    /// <example>
    ///
    /// </example>
    /// <typeparam name="TDisposable"></typeparam>
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
    /// Executes the given function on the extended disposable object then disposes of the object
    /// </summary>
    /// <example>
    ///
    /// </example>
    /// <typeparam name="TDisposable"></typeparam>
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
    /// <param name="this"></param>
    /// <param name="actionAsync"></param>
    /// <returns></returns>
    public static async Task UsingAsync<TDisposable>(this TDisposable @this, Func<TDisposable, Task> actionAsync)
      where TDisposable : IDisposable
    {
      using (@this)
      {
        await actionAsync(@this);
      }
    }

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
  }
}