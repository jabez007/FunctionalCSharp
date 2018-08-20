using System;
using System.Threading.Tasks;

namespace FunctionalCSharp.Results
{
    /// <summary>
    /// Extension methods for exceptions and functionalizing the 'try-catch' block
    /// </summary>
    public static class ExceptionHandling
    {
        #region object extensions

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="tryFunction"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static IResult Try<T, TException>(this T @this, Func<T, IResult> tryFunction, string errorMessage = "")
            where TException : Exception
        {
            try
            {
                return tryFunction(@this);
            }
            catch (TException ex)
            {
                return Result.Failure(ex, errorMessage);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="tryFunction"></param>
        /// <param name="catchFunction"></param>
        /// <returns></returns>
        public static IResult Try<T, TException>(this T @this, Func<T, IResult> tryFunction, Func<TException, IResult> catchFunction)
            where TException : Exception
        {
            try
            {
                return tryFunction(@this);
            }
            catch (TException ex)
            {
                return catchFunction(ex);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="tryFunction"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static IResult<TResult> Try<T, TResult, TException>(this T @this, Func<T, IResult<TResult>> tryFunction, string errorMessage = "")
            where TException : Exception
        {
            try
            {
                return tryFunction(@this);
            }
            catch (TException ex)
            {
                return Result<TResult>.Failure(ex, errorMessage);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="tryFunction"></param>
        /// <param name="catchFunction"></param>
        /// <returns></returns>
        public static IResult<TResult> Try<T, TResult, TException>(this T @this,
            Func<T, IResult<TResult>> tryFunction, Func<TException, IResult<TResult>> catchFunction)
            where TException : Exception
        {
            try
            {
                return tryFunction(@this);
            }
            catch (TException ex)
            {
                return catchFunction(ex);
            }
        }

        #region Async

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="tryFunction"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static async Task<IResult> TryAsync<T, TException>(this T @this, Func<T, Task<IResult>> tryFunction, string errorMessage = "")
            where TException : Exception
        {
            try
            {
                return await tryFunction(@this);
            }
            catch (TException ex)
            {
                return Result.Failure(ex, errorMessage);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="tryFunction"></param>
        /// <param name="catchFunction"></param>
        /// <returns></returns>
        public static async Task<IResult> TryAsync<T, TException>(this T @this,
            Func<T, Task<IResult>> tryFunction, Func<TException, IResult> catchFunction)
            where TException : Exception
        {
            try
            {
                return await tryFunction(@this);
            }
            catch (TException ex)
            {
                return catchFunction(ex);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="tryFunction"></param>
        /// <param name="catchFunction"></param>
        /// <returns></returns>
        public static async Task<IResult> TryAsync<T, TException>(this T @this,
            Func<T, Task<IResult>> tryFunction, Func<TException, Task<IResult>> catchFunction)
            where TException : Exception
        {
            try
            {
                return await tryFunction(@this);
            }
            catch (TException ex)
            {
                return await catchFunction(ex);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="tryFunction"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static async Task<IResult<TResult>> TryAsync<T, TResult, TException>(this T @this,
            Func<T, Task<IResult<TResult>>> tryFunction, string errorMessage = "")
            where TException : Exception
        {
            try
            {
                return await tryFunction(@this);
            }
            catch (TException ex)
            {
                return Result<TResult>.Failure(ex, errorMessage);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="tryFunction"></param>
        /// <param name="catchFunction"></param>
        /// <returns></returns>
        public static async Task<IResult<TResult>> TryAsync<T, TResult, TException>(this T @this,
            Func<T, Task<IResult<TResult>>> tryFunction, Func<TException, IResult<TResult>> catchFunction)
            where TException : Exception
        {
            try
            {
                return await tryFunction(@this);
            }
            catch (TException ex)
            {
                return catchFunction(ex);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="tryFunction"></param>
        /// <param name="catchFunction"></param>
        /// <returns></returns>
        public static async Task<IResult<TResult>> TryAsync<T, TResult, TException>(this T @this,
            Func<T, Task<IResult<TResult>>> tryFunction, Func<TException, Task<IResult<TResult>>> catchFunction)
            where TException : Exception
        {
            try
            {
                return await tryFunction(@this);
            }
            catch (TException ex)
            {
                return await catchFunction(ex);
            }
        }

        #endregion Async

        #endregion object extensions

        #region Func extensions

        #region 0 args

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static Func<IResult> Catch<TException>(this Func<IResult> @this)
            where TException : Exception
        {
            return () =>
            {
                try
                {
                    return @this();
                }
                catch (TException ex)
                {
                    return Result.Failure(ex);
                }
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="catchFunction"></param>
        /// <returns></returns>
        public static Func<IResult> Catch<TException>(this Func<IResult> @this, Func<TException, IResult> catchFunction)
            where TException : Exception
        {
            return () =>
            {
                try
                {
                    return @this();
                }
                catch (TException ex)
                {
                    return catchFunction(ex);
                }
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static Func<IResult<TResult>> Catch<TResult, TException>(this Func<IResult<TResult>> @this)
            where TException : Exception
        {
            return () =>
            {
                try
                {
                    return @this();
                }
                catch (TException ex)
                {
                    return Result<TResult>.Failure(ex);
                }
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="catchFunction"></param>
        /// <returns></returns>
        public static Func<IResult<TResult>> Catch<TResult, TException>(this Func<IResult<TResult>> @this,
            Func<TException, IResult<TResult>> catchFunction)
            where TException : Exception
        {
            return () =>
            {
                try
                {
                    return @this();
                }
                catch (TException ex)
                {
                    return catchFunction(ex);
                }
            };
        }

        #endregion 0 args

        #region 1 arg

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static Func<T, IResult> Catch<T, TException>(this Func<T, IResult> @this)
            where TException : Exception
        {
            return (x) =>
            {
                try
                {
                    return @this(x);
                }
                catch (TException ex)
                {
                    return Result.Failure(ex);
                }
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static Func<T, Task<IResult>> CatchAsync<T, TException>(this Func<T, Task<IResult>> @this)
            where TException : Exception
        {
            return async (x) =>
            {
                try
                {
                    return await @this(x);
                }
                catch (TException ex)
                {
                    return Result.Failure(ex);
                }
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="catchFunction"></param>
        /// <returns></returns>
        public static Func<T, IResult> Catch<T, TException>(this Func<T, IResult> @this, Func<TException, IResult> catchFunction)
            where TException : Exception
        {
            return (x) =>
            {
                try
                {
                    return @this(x);
                }
                catch (TException ex)
                {
                    return catchFunction(ex);
                }
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="catchFunction"></param>
        /// <returns></returns>
        public static Func<T, Task<IResult>> CatchAsync<T, TException>(this Func<T, Task<IResult>> @this, Func<TException, IResult> catchFunction)
            where TException : Exception
        {
            return async (x) =>
            {
                try
                {
                    return await @this(x);
                }
                catch (TException ex)
                {
                    return catchFunction(ex);
                }
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static Func<T, IResult<TResult>> Catch<T, TResult, TException>(this Func<T, IResult<TResult>> @this)
            where TException : Exception
        {
            return (x) =>
            {
                try
                {
                    return @this(x);
                }
                catch (TException ex)
                {
                    return Result<TResult>.Failure(ex);
                }
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static Func<T, Task<IResult<TResult>>> CatchAsync<T, TResult, TException>(this Func<T, Task<IResult<TResult>>> @this)
            where TException : Exception
        {
            return async (x) =>
            {
                try
                {
                    return await @this(x);
                }
                catch (TException ex)
                {
                    return Result<TResult>.Failure(ex);
                }
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="catchFunction"></param>
        /// <returns></returns>
        public static Func<T, IResult<TResult>> Catch<T, TResult, TException>(this Func<T, IResult<TResult>> @this,
            Func<TException, IResult<TResult>> catchFunction)
            where TException : Exception
        {
            return (x) =>
            {
                try
                {
                    return @this(x);
                }
                catch (TException ex)
                {
                    return catchFunction(ex);
                }
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="catchFunction"></param>
        /// <returns></returns>
        public static Func<T, Task<IResult<TResult>>> CatchAsync<T, TResult, TException>(this Func<T, Task<IResult<TResult>>> @this,
            Func<TException, IResult<TResult>> catchFunction)
            where TException : Exception
        {
            return async (x) =>
            {
                try
                {
                    return await @this(x);
                }
                catch (TException ex)
                {
                    return catchFunction(ex);
                }
            };
        }

        #endregion 1 arg

        #region 2 args

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static Func<T1, T2, IResult> Catch<T1, T2, TException>(this Func<T1, T2, IResult> @this)
            where TException : Exception
        {
            return (x, y) =>
            {
                try
                {
                    return @this(x, y);
                }
                catch (TException ex)
                {
                    return Result.Failure(ex);
                }
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="catchFunction"></param>
        /// <returns></returns>
        public static Func<T1, T2, IResult> Catch<T1, T2, TException>(this Func<T1, T2, IResult> @this, Func<TException, IResult> catchFunction)
            where TException : Exception
        {
            return (x, y) =>
            {
                try
                {
                    return @this(x, y);
                }
                catch (TException ex)
                {
                    return catchFunction(ex);
                }
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static Func<T1, T2, IResult<TResult>> Catch<T1, T2, TResult, TException>(this Func<T1, T2, IResult<TResult>> @this)
            where TException : Exception
        {
            return (x, y) =>
            {
                try
                {
                    return @this(x, y);
                }
                catch (TException ex)
                {
                    return Result<TResult>.Failure(ex);
                }
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="catchFunction"></param>
        /// <returns></returns>
        public static Func<T1, T2, IResult<TResult>> Catch<T1, T2, TResult, TException>(this Func<T1, T2, IResult<TResult>> @this,
            Func<TException, IResult<TResult>> catchFunction)
            where TException : Exception
        {
            return (x, y) =>
            {
                try
                {
                    return @this(x, y);
                }
                catch (TException ex)
                {
                    return catchFunction(ex);
                }
            };
        }

        #endregion 2 args

        #endregion Func extensions
    }
}