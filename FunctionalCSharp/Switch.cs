using System;

namespace FunctionalCSharp
{
    /// <summary>
    /// Static and extension methods to functionalize the 'switch' block
    /// </summary>
    public static class Switch
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="switchOn"></param>
        /// <returns></returns>
        public static Switch<T, TResult> On<T, TResult>(T switchOn) =>
            new Switch<T, TResult>(switchOn);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static Switch<T ,TResult> SwitchOn<T, TResult>(this T @this) =>
            On<T, TResult>(@this);
    }

    /// <summary>
    /// Functionalizes the 'switch' block
    /// </summary>
    /// <example>
    /// var message =  Switch
    ///     .On&lt;string, string&gt;("world")
    ///         .Case(
    ///             @this => @this.StartsWith("world"),
    ///             @this => "hello " + @this
    ///         )
    ///         .Case(
    ///             () => false,
    ///             @this => "never hits"
    ///         )
    ///         .Default(
    ///             @this => @this.ToUpper()
    ///         );
    /// </example>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class Switch<T, TResult>
    {
        private readonly T value;
        private readonly bool isSuccess = false;
        private readonly TResult result;

        internal Switch(T value)
        {
            this.value = value;
        }

        private Switch(TResult result)
        {
            isSuccess = true;
            this.result = result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparison"></param>
        /// <param name="ifTrue"></param>
        /// <returns></returns>
        public Switch<T, TResult> Case(Func<T, bool> comparison, Func<T, TResult> ifTrue) =>
            isSuccess ? this :
                comparison(value) ? new Switch<T, TResult>(ifTrue(value)) : this;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="default"></param>
        /// <returns></returns>
        public TResult Default(Func<T, TResult> @default) =>
            isSuccess ? result : @default(value);
    }
}
