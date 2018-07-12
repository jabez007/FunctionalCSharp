using System;

namespace FunctionalCSharp.Results
{
    /// <summary>
    /// 
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// Checks that the extended object is not null
        /// </summary>
        /// <example>
        /// 
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="failureMessage"></param>
        /// <returns></returns>
        public static IResult<T> IsNotNull<T>(this T @this, string failureMessage) =>
            @this != null ? Result<T>.Success(@this) : Result<T>.Failure(failureMessage);

        /// <summary>
        /// Checks that the extended object is not null or the deafult for its type
        /// </summary>
        /// <example>
        /// 
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="failureMessage"></param>
        /// <returns></returns>
        public static IResult<T> IsNotDefault<T>(this T @this, string failureMessage) =>
            @this
                .IsNotNull(failureMessage)
                .Bind(t => !t.Equals(default(T)) ? Result<T>.Success(t) : Result<T>.Failure(failureMessage));

        /// <summary>
        /// Executes the given function on the extended object when that object is not null
        /// </summary>
        /// <example>
        /// 
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="whenNotNull">The function to execute on the extended object when it is not null</param>
        /// <returns></returns>
        public static IResult WhenNotNull<T>(this T @this, Func<T, IResult> whenNotNull) =>
            @this.IsNotNull() ? whenNotNull(@this) : Result.Failure(new ArgumentNullException());

        /// <summary>
        /// Executes the given function on the extended object when that object is not null
        /// </summary>
        /// <example>
        /// 
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="whenNotNull">The function to execute on the extended object when it is not null</param>
        /// <param name="failureMessage"></param>
        /// <returns></returns>
        public static IResult WhenNotNull<T>(this T @this, Func<T, IResult> whenNotNull,
            string failureMessage) =>
                @this
                    .IsNotNull(failureMessage)
                    .Bind(t => whenNotNull(t));

        /// <summary>
        /// Executes the given function on the extended object when that object is not null
        /// </summary>
        /// <example>
        /// 
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="whenNotNull">The function to execute on the extended object when it is not null</param>
        /// <returns></returns>
        public static IResult<TResult> WhenNotNull<T, TResult>(this T @this, Func<T, IResult<TResult>> whenNotNull) =>
            @this.IsNotNull() ? whenNotNull(@this) : Result<TResult>.Failure(new ArgumentNullException());

        /// <summary>
        /// Executes the given function on the extended object when that object is not null
        /// </summary>
        /// <example>
        /// 
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="whenNotNull">The function to execute on the extended object when it is not null</param>
        /// <param name="failureMessage"></param>
        /// <returns></returns>
        public static IResult<TResult> WhenNotNull<T, TResult>(this T @this, Func<T, IResult<TResult>> whenNotNull,
            string failureMessage) =>
                @this
                    .IsNotNull(failureMessage)
                    .Bind(t => whenNotNull(t));

        /// <summary>
        /// Executes the given function on the extended object when that object is not its type default
        /// </summary>
        /// <example>
        /// 
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="whenNotDefault"></param>
        /// <returns></returns>
        public static IResult WhenNotDefault<T>(this T @this, Func<T, IResult> whenNotDefault) =>
            @this.IsNotDefault() ? whenNotDefault(@this) : Result.Failure(new ArgumentException());

        /// <summary>
        /// Executes the given function on the extended object when that object is not its type default
        /// </summary>
        /// <example>
        /// 
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="whenNotDefault"></param>
        /// <param name="failureMessage"></param>
        /// <returns></returns>
        public static IResult WhenNotDefault<T>(this T @this, Func<T, IResult> whenNotDefault,
            string failureMessage) =>
                @this
                    .IsNotDefault(failureMessage)
                    .Bind(t => whenNotDefault(t));

        /// <summary>
        /// Executes the given function on the extended object when that object is not its type default
        /// </summary>
        /// <example>
        /// 
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="whenNotDefault"></param>
        /// <returns></returns>
        public static IResult<TResult> WhenNotDefault<T, TResult>(this T @this, Func<T, IResult<TResult>> whenNotDefault) =>
            @this.IsNotDefault() ? whenNotDefault(@this) : Result<TResult>.Failure(new ArgumentException());

        /// <summary>
        /// Executes the given function on the extended object when that object is not its type default
        /// </summary>
        /// <example>
        /// 
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="whenNotDefault"></param>
        /// <param name="failureMessage"></param>
        /// <returns></returns>
        public static IResult<TResult> WhenNotDefault<T, TResult>(this T @this, Func<T, IResult<TResult>> whenNotDefault,
            string failureMessage) =>
                @this
                    .IsNotDefault(failureMessage)
                    .Bind(t => whenNotDefault(t));
    }
}
