using System;

namespace FunctionalCSharp
{
    /// <summary>
    /// Extension methods to functionalize checking if an object is null or default
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
        /// <returns></returns>
        public static Result<T> IsNotNull<T>(this T @this) => 
            @this != null ? Result<T>.Success(@this) : 
                Result<T>.Failure("Value of {0} cannot be null".Format(typeof(T)));

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
        public static Result<T> IsNotNull<T>(this T @this, string failureMessage) =>
            @this != null ? Result<T>.Success(@this) : Result<T>.Failure(failureMessage);

        /// <summary>
        /// Checks that the extended object is not null or the deafult for its type
        /// </summary>
        /// <example>
        /// 
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static Result<T> IsNotDefault<T>(this T @this) =>
            @this
                .IsNotNull()
                .Bind(t => 
                    !t.Equals(default(T)) ? Result<T>.Success(t) : 
                        Result<T>.Failure("Value of {0} cannot be the default for {0}".Format(typeof(T)))
                );

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
        public static Result<T> IsNotDefault<T>(this T @this, string failureMessage) =>
            @this
                .IsNotNull(failureMessage)
                .Bind(t => 
                    !t.Equals(default(T)) ? Result<T>.Success(t) : 
                        Result<T>.Failure(failureMessage)
            );

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
        public static Result<TResult> WhenNotNull<T, TResult>(this T @this, Func<T, Result<TResult>> whenNotNull) =>
            @this
                .IsNotNull()
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
        /// <param name="failureMessage"></param>
        /// <returns></returns>
        public static Result<TResult> WhenNotNull<T, TResult>(this T @this, Func<T, Result<TResult>> whenNotNull, 
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
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="whenNotDefault"></param>
        /// <returns></returns>
        public static Result<TResult> WhenNotDefault<T, TResult>(this T @this, Func<T, Result<TResult>> whenNotDefault) =>
            @this
                .IsNotDefault()
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
        /// <param name="failureMessage"></param>
        /// <returns></returns>
        public static Result<TResult> WhenNotDefault<T, TResult>(this T @this, Func<T, Result<TResult>> whenNotDefault, 
            string failureMessage) =>
                @this
                    .IsNotDefault(failureMessage)
                    .Bind(t => whenNotDefault(t));

        /// <summary>
        /// Executes the given function to return an object of the extended object's type 
        /// if the extended object is null
        /// </summary>
        /// <example>
        /// 
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="whenNull">The function to execute when the extended object is null</param>
        /// <returns></returns>
        public static T WhenNull<T>(this T @this, Func<T> whenNull) =>
            @this == null ? whenNull() : @this;

        /// <summary>
        /// Executes the given function to return an object of the extended object's type 
        /// if the extended object is null or the default for its type
        /// </summary>
        /// <example>
        /// 
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="whenDefault">The function to execute when the extended object is null or default</param>
        /// <returns></returns>
        public static T WhenDefault<T>(this T @this, Func<T> whenDefault) =>
            @this == null || @this.Equals(default(T)) ? whenDefault() : @this;
    }
}