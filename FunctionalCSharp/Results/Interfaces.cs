using System;

namespace FunctionalCSharp.Results
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// 
        /// </summary>
        string ErrorMessage { get; }

        /// <summary>
        /// Convert this Failure result to another base Failure result. 
        /// The ErrorMessage should be preserved in the conversion.
        /// </summary>
        /// <returns></returns>
        IResult Failure();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResult<T> : IResult
    {
        /// <summary>
        /// 
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Convert this Failure result to a base Failure result. 
        /// The ErrorMessage should be preserved in the conversion.
        /// </summary>
        /// <returns></returns>
        new IResult Failure();

        /// <summary>
        /// Convert this Failure result to another Failure result. 
        /// The ErrorMessage should be preserved in the conversion.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        IResult<TResult> Failure<TResult>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TEnum"></typeparam>
    public interface IResult<T, TEnum> : IResult<T>
        where TEnum : Enum
    {
        /// <summary>
        /// 
        /// </summary>
        TEnum ErrorCode { get; }

        /// <summary>
        /// Convert this Failure result to another Failure result. 
        /// The ErrorCode and ErrorMessage should be preseved in the conversion.
        /// </summary>
        /// <returns></returns>
        new IResult<TEnum> Failure();

        /// <summary>
        /// Convert this Failure result to another Failure result. 
        /// The ErrorCode and ErrorMessage should be preseved in the conversion.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        new IResult<TResult, TEnum> Failure<TResult>();
    }
}
