using System.Collections.Generic;
using System.Linq;

namespace FunctionalCSharp
{
    /// <summary>
    /// Extensions methods to provide some list functionality similar to that found in Python
    /// </summary>
    public static class IEnumerableSliceExtensions
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
                return @this.ElementAt(@this.Count() + index);
            else
                return @this.ElementAt(index);
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
                return @this.ElementAtOrDefault(@this.Count() + index);
            else
                return @this.ElementAtOrDefault(index);
        }

        /// <summary>
        /// Slice IEnumerable objects similar to how you would lists in Python
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="startingIndex"></param>
        /// <param name="exclusiveEndIndex"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IEnumerable<T> Slice<T>(this IEnumerable<T> @this, int? startingIndex, int? exclusiveEndIndex, int step = 1)
        {
            if (step == 0)
                yield break;

            int _startingIndex = startingIndex ?? (step < 0 ? @this.Count() - 1 : 0);
            if (_startingIndex < 0)
                _startingIndex += @this.Count();

            int _exclusiveEndIndex = exclusiveEndIndex ?? (step < 0 ? -1 : @this.Count());
            if (exclusiveEndIndex.HasValue && _exclusiveEndIndex < 0)  // the negative was specifically passed in
                _exclusiveEndIndex += @this.Count();

            if (step < 0)
            {
                if (_startingIndex < _exclusiveEndIndex)
                    yield break;

                for (int i = _startingIndex; i > _exclusiveEndIndex; i += step)
                    yield return @this.ElementAt(i);
            }

            if (step > 0)
            {
                if (_startingIndex > _exclusiveEndIndex)
                    yield break;

                for (int i = _startingIndex; i < _exclusiveEndIndex; i += step)
                    yield return @this.ElementAt(i);
            }
        }
    }
}
