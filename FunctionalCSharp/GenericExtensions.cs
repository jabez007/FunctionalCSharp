using System;
using System.Threading.Tasks;

namespace FunctionalCSharp
{
  /// <summary>
  /// Extension methods to provide some generic functionality
  /// </summary>
  public static class GenericExtensions
  {
    #region Tee

    /// <summary>
    /// Mimics the UNIX tee command by sending the extended object to the given action (void returning method),
    /// and then returns the object to continue the pipeline/method-chain. Can be used to log the object to
    /// some output or to effect the object itself.
    /// </summary>
    /// <example>
    /// "hello world".Tee(Console.WriteLine);
    /// //or
    /// var content = Disposable.Using(
    ///     () => new FileStream("file.txt", FileMode.Open),
    ///     stream => new byte[stream.Length].Tee(b => stream.Read(b, 0, (int) stream.Length))
    /// );
    /// </example>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="action">A void returning method to act on the extended object</param>
    /// <returns>The extended object after the action executes</returns>
    public static T Tee<T>(this T @this, Action<T> action)
    {
      action(@this);
      return @this;
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static async Task<T> TeeAsync<T>(this Task<T> @this, Action<T> action) =>
      (await @this)
        .Tee(action);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="actionAsync"></param>
    /// <returns></returns>
    public static async Task<T> TeeAsync<T>(this T @this, Func<T, Task> actionAsync)
    {
      await actionAsync(@this);
      return @this;
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="actionAsync"></param>
    /// <returns></returns>
    public static async Task<T> TeeAsync<T>(this Task<T> @this, Func<T, Task> actionAsync) =>
      await (await @this)
        .TeeAsync(actionAsync);

    #endregion Tee

    #region When

    /// <summary>
    /// Functionalizes the 'if' statement block to execute the given function on the extended object when the given condition is met
    /// </summary>
    /// <example>
    /// "hello".When(str => str.StartsWith("hello"), str => str + " world");
    /// </example>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="condition">A boolean function to evaluate</param>
    /// <param name="ifTrue">A function to execute on the extended object if the condition evaluates to true</param>
    /// <returns></returns>
    public static T When<T>(this T @this, Func<T, bool> condition, Func<T, T> ifTrue) =>
      condition(@this) ? ifTrue(@this) : @this;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="condition"></param>
    /// <param name="ifTrueAsync"></param>
    /// <returns></returns>
    public static Task<T> WhenAsync<T>(this T @this, Func<T, bool> condition, Func<T, Task<T>> ifTrueAsync) =>
      condition(@this) ? ifTrueAsync(@this) : Task.FromResult(@this);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="condition"></param>
    /// <param name="ifTrue"></param>
    /// <returns></returns>
    public static async Task<T> WhenAsync<T>(this Task<T> @this, Func<T, bool> condition, Func<T, T> ifTrue) =>
      (await @this)
        .When(condition, ifTrue);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="condition"></param>
    /// <param name="ifTrueAsync"></param>
    /// <returns></returns>
    public static async Task<T> WhenAsync<T>(this Task<T> @this, Func<T, bool> condition, Func<T, Task<T>> ifTrueAsync) =>
      await (await @this)
        .WhenAsync(condition, ifTrueAsync);

    #endregion When

    #region Map

    /// <summary>
    /// Applies the given transformation to the extended object
    /// </summary>
    /// <example>
    /// "hello world".Map(str => Encoding.UTF8.GetBytes(str));
    /// </example>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="transformation">A function to transform the extended object</param>
    /// <returns>the transformation of the extended object</returns>
    public static TResult Map<TSource, TResult>(this TSource @this, Func<TSource, TResult> transformation) =>
      transformation(@this);

    /// <summary>
    /// Technically the same as the Map extension?
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="transformationAsync"></param>
    /// <returns></returns>
    public static Task<TResult> MapAsync<TSource, TResult>(this TSource @this, Func<TSource, Task<TResult>> transformationAsync) =>
      @this
        .Map(transformationAsync);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="transformation"></param>
    /// <returns></returns>
    public static async Task<TResult> MapAsync<TSource, TResult>(this Task<TSource> @this, Func<TSource, TResult> transformation) =>
      (await @this)
        .Map(transformation);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="transformationAsync"></param>
    /// <returns></returns>
    public static async Task<TResult> MapAsync<TSource, TResult>(this Task<TSource> @this, Func<TSource, Task<TResult>> transformationAsync) =>
      await (await @this)
        .MapAsync(transformationAsync);

    #endregion Map

    #region Map When

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="condition"></param>
    /// <param name="transformationIfTrue"></param>
    /// <returns></returns>
    public static TResult MapWhen<TSource, TResult>(this TSource @this, Func<TSource, bool> condition, Func<TSource, TResult> transformationIfTrue) =>
      condition(@this) ? @this.Map(transformationIfTrue) : default;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="condition"></param>
    /// <param name="transformationIfTrueAsync"></param>
    /// <returns></returns>
    public static Task<TResult> MapWhenAsync<TSource, TResult>(this TSource @this, Func<TSource, bool> condition,
      Func<TSource, Task<TResult>> transformationIfTrueAsync) =>
        condition(@this) ? @this.MapAsync(transformationIfTrueAsync) : Task.FromResult(default(TResult));

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="condition"></param>
    /// <param name="transformationIfTrue"></param>
    /// <returns></returns>
    public static async Task<TResult> MapWhenAsync<TSource, TResult>(this Task<TSource> @this, Func<TSource, bool> condition,
      Func<TSource, TResult> transformationIfTrue) =>
        (await @this)
          .MapWhen(condition, transformationIfTrue);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="condition"></param>
    /// <param name="transformationIfTrueAsync"></param>
    /// <returns></returns>
    public static async Task<TResult> MapWhenAsync<TSource, TResult>(this Task<TSource> @this, Func<TSource, bool> condition,
      Func<TSource, Task<TResult>> transformationIfTrueAsync) =>
        await (await @this)
          .MapWhenAsync(condition, transformationIfTrueAsync);

    #endregion Map When
  }
}