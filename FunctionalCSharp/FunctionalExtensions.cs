using System;

namespace FunctionalCSharp
{
    public static class FunctionalExtensions
    {
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
    }
}
