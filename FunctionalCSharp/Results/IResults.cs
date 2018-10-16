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
  }
}