using System;
using System.Threading.Tasks;

namespace FunctionalCSharp.FuncExtensions
{
  /// <summary>
  ///
  /// </summary>
  public static class UsingExtensions
  {
    /// <summary>
    /// Executes the extended function on a disposable object within a using-block
    /// </summary>
    /// <typeparam name="TDisposable"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<TDisposable, TResult> Using<TDisposable, TResult>(this Func<TDisposable, TResult> @this)
      where TDisposable : IDisposable =>
        (disposable) =>
        {
          using (disposable)
          {
            return @this(disposable);
          }
        };

    /// <summary>
    /// Executes the extended async function on a disposable object within a using-block
    /// </summary>
    /// <typeparam name="TDisposable"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<TDisposable, Task> UsingAsync<TDisposable, TResult>(this Func<TDisposable, Task> @this)
      where TDisposable : IDisposable =>
        async (disposable) =>
        {
          using (disposable)
          {
            await @this(disposable);
          }
        };

    /// <summary>
    /// Executes the extended async function on a disposable object within a using-block
    /// </summary>
    /// <typeparam name="TDisposable"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<TDisposable, Task<TResult>> UsingAsync<TDisposable, TResult>(this Func<TDisposable, Task<TResult>> @this)
      where TDisposable : IDisposable =>
        async (disposable) =>
        {
          using (disposable)
          {
            return await @this(disposable);
          }
        };
  }
}