using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalCSharp.Results
{
    /// <summary>
    /// 
    /// </summary>
    public static class TaskIResultExtensions
    {
        #region IResult
        /// <summary>
        /// 
        /// </summary>
        /// <param name="this"></param>
        /// <param name="ifSuccess"></param>
        /// <returns></returns>
        public static async Task<IResult> BindAsync(this Task<IResult> @this, Func<IResult> ifSuccess) =>
            (await @this).Bind(ifSuccess);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="this"></param>
        /// <param name="ifSuccessAsync"></param>
        /// <returns></returns>
        public static async Task<IResult> BindAsync(this Task<IResult> @this, Func<Task<IResult>> ifSuccessAsync) =>
            await (await @this).BindAsync(ifSuccessAsync);
        #endregion

        #region IResult<T>
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="ifSuccess"></param>
        /// <returns></returns>
        public static async Task<IResult> BindAsync<T>(this Task<IResult<T>> @this, Func<T, IResult> ifSuccess) =>
            (await @this).Bind(ifSuccess);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="ifSuccessAsync"></param>
        /// <returns></returns>
        public static async Task<IResult> BindAsync<T>(this Task<IResult<T>> @this, Func<T, Task<IResult>> ifSuccessAsync) =>
            await (await @this).BindAsync(ifSuccessAsync);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="ifSuccess"></param>
        /// <returns></returns>
        public static async Task<IResult<TResult>> BindAsync<T, TResult>(this Task<IResult<T>> @this, Func<T, IResult<TResult>> ifSuccess) =>
            (await @this).Bind(ifSuccess);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="ifSuccessAsync"></param>
        /// <returns></returns>
        public static async Task<IResult<TResult>> BindAsync<T, TResult>(this Task<IResult<T>> @this, Func<T, Task<IResult<TResult>>> ifSuccessAsync) =>
            await (await @this).BindAsync(ifSuccessAsync);
        #endregion

        #region IResult<TEnum>
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="this"></param>
        /// <param name="ifSuccess"></param>
        /// <returns></returns>
        public static async Task<IResult<TEnum>> BindAsync<TEnum>(this Task<IResult<TEnum>> @this, Func<IResult<TEnum>> ifSuccess)
            where TEnum : Enum =>
                (await @this).Bind(ifSuccess);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="this"></param>
        /// <param name="ifSuccessAsync"></param>
        /// <returns></returns>
        public static async Task<IResult<TEnum>> BindAsync<TEnum>(this Task<IResult<TEnum>> @this, Func<Task<IResult<TEnum>>> ifSuccessAsync)
            where TEnum : Enum =>
                await (await @this).BindAsync(ifSuccessAsync);
        #endregion

        #region IResult<T, TEnum>
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="ifSuccess"></param>
        /// <returns></returns>
        public static async Task<IResult<TEnum>> BindAsync<T, TEnum, TResult>(this Task<IResult<T, TEnum>> @this,
            Func<T, IResult<TEnum>> ifSuccess) where TEnum : Enum =>
                (await @this).Bind(ifSuccess);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="ifSuccessAsync"></param>
        /// <returns></returns>
        public static async Task<IResult<TEnum>> BindAsync<T, TEnum, TResult>(this Task<IResult<T, TEnum>> @this,
            Func<T, Task<IResult<TEnum>>> ifSuccessAsync) where TEnum : Enum =>
                await (await @this).BindAsync(ifSuccessAsync);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="ifSuccess"></param>
        /// <returns></returns>
        public static async Task<IResult<TResult, TEnum>> BindAsync<T, TEnum, TResult>(this Task<IResult<T, TEnum>> @this,
            Func<T, IResult<TResult, TEnum>> ifSuccess) where TEnum : Enum =>
                (await @this).Bind(ifSuccess);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="ifSuccessAsync"></param>
        /// <returns></returns>
        public static async Task<IResult<TResult, TEnum>> BindAsync<T, TEnum, TResult>(this Task<IResult<T, TEnum>> @this,
            Func<T, Task<IResult<TResult, TEnum>>> ifSuccessAsync) where TEnum : Enum =>
                await (await @this).BindAsync(ifSuccessAsync);
        #endregion
    }
}
