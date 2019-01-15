using System.Collections.Generic;
using System.Linq;

namespace FunctionalCSharp.ObjectExtensions.IEnumerableExtensions
{
  /// <summary>
  /// Extensions methods to provide some list functionality similar to that found in Python
  /// </summary>
  public static class SliceExtensions
  {
    /// <summary>
    /// An extension of the System.Linq IEnumberable.ElementAt that allows the use of negetive indices like Python.
    /// </summary>
    /// <example>
    ///
    /// </example>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static T AtIndex<T>(this IEnumerable<T> @this, int index)
    {
      if (index < 0)
      {
        return @this.ElementAt(@this.Count() + index);
      }
      else
      {
        return @this.ElementAt(index);
      }
    }

    /// <summary>
    /// An extension of the System.Linq IEnumberable.ElementAtOrDefault that allows the use of negetive indices like Python.
    /// </summary>
    /// <example>
    ///
    /// </example>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static T AtIndexOrDefault<T>(this IEnumerable<T> @this, int index)
    {
      if (index < 0)
      {
        return @this.ElementAtOrDefault(@this.Count() + index);
      }
      else
      {
        return @this.ElementAtOrDefault(index);
      }
    }

    /// <summary>
    /// Slice IEnumerable objects similar to how you would lists in Python
    /// </summary>
    /// <example>
    ///
    /// </example>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="startingIndex"></param>
    /// <param name="exclusiveEndIndex"></param>
    /// <param name="step"></param>
    /// <returns></returns>
    public static IEnumerable<T> Slice<T>(this IEnumerable<T> @this, int? startingIndex, int? exclusiveEndIndex, int step = 1)
    {
      if (step == 0)
      {
        return new T[0];
      }

      int count = @this.Count();

      int _startingIndex = startingIndex ?? (step < 0 ? count - 1 : 0);
      if (_startingIndex < 0)
      {
        _startingIndex += count;
      }

      int _exclusiveEndIndex = exclusiveEndIndex ?? (step < 0 ? -1 : count);
      if (exclusiveEndIndex.HasValue && _exclusiveEndIndex < 0)  // the negative was specifically passed in
      {
        _exclusiveEndIndex += count;
      }

      if (step < 0)
      {
        if (_startingIndex < _exclusiveEndIndex)
        {
          return new T[0];
        }

        return @this.Where((t, i) =>
                    (i <= _startingIndex) && (i > _exclusiveEndIndex) && (((_startingIndex - i) % step) == 0))
                    .Reverse();
      }

      if (step > 0)
      {
        if (_startingIndex > _exclusiveEndIndex)
        {
          return new T[0];
        }

        return @this.Where((t, i) =>
                    (i >= _startingIndex) && (i < _exclusiveEndIndex) && (((i - _startingIndex) % step) == 0));
      }

      return new T[0];
    }
  }
}