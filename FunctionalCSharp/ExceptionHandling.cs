using System;
using System.Threading.Tasks;

namespace FunctionalCSharp
{
    public static class ExceptionHandling
    {
        public static string GetMessageStack(this Exception @this, string message = "") =>
            message
                .WhenNotNullOrEmpty(m => string.Format("{0}{1}", m, Environment.NewLine))
                .Append(@this._GetMessageStack())
                .TrimEnd();

        private static string _GetMessageStack(this Exception @this) =>
            ""
                .AppendFormat("\tException type: {1}{0}\tException Message: {2}{0}\tStack trace: {3}{0}",
                    Environment.NewLine, @this.GetType(), @this.Message, @this.StackTrace)
                .When(
                    m => @this.InnerException != null,
                    m => m.AppendFormat("\t---- BEGIN Inner Exception----{0}{1}\t---- END Inner Exception ----{0}",
                        Environment.NewLine, @this.InnerException._GetMessageStack())
                );

        #region object extensions
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="tryFunction"></param>
        /// <returns></returns>
        public static Result Try<T, TException>(this T @this, Func<T, Result> tryFunction, string errorMessage = "")
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
        public static Result Try<T, TException>(this T @this, Func<T, Result> tryFunction, Func<TException, Result> catchFunction)
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
        /// <returns></returns>
        public static Result<TResult> Try<T, TResult, TException>(this T @this, Func<T, Result<TResult>> tryFunction, string errorMessage)
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
        public static Result<TResult> Try<T, TResult, TException>(this T @this, 
            Func<T, Result<TResult>> tryFunction, Func<TException, Result<TResult>> catchFunction)
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
        /// <returns></returns>
        public static async Task<Result> TryAsync<T, TException>(this T @this, Func<T, Task<Result>> tryFunction, string errorMessage)
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
        public static async Task<Result> TryAsync<T, TException>(this T @this, 
            Func<T, Task<Result>> tryFunction, Func<TException, Result> catchFunction)
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
        public static async Task<Result> TryAsync<T, TException>(this T @this, 
            Func<T, Task<Result>> tryFunction, Func<TException, Task<Result>> catchFunction)
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
        /// <returns></returns>
        public static async Task<Result<TResult>> TryAsync<T, TResult, TException>(this T @this, 
            Func<T, Task<Result<TResult>>> tryFunction, string errorMessage)
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
        public static async Task<Result<TResult>> TryAsync<T, TResult, TException>(this T @this, 
            Func<T, Task<Result<TResult>>> tryFunction, Func<TException, Result<TResult>> catchFunction)
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
        public static async Task<Result<TResult>> TryAsync<T, TResult, TException>(this T @this, 
            Func<T, Task<Result<TResult>>> tryFunction, Func<TException, Task<Result<TResult>>> catchFunction)
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
        #endregion

        #endregion

        #region Func extensions

        #region 0 args
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static Func<Result> Catch<TException>(this Func<Result> @this)
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
        public static Func<Result> Catch<TException>(this Func<Result> @this, Func<TException, Result> catchFunction)
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
        public static Func<Result<TResult>> Catch<TResult, TException>(this Func<Result<TResult>> @this)
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
        public static Func<Result<TResult>> Catch<TResult, TException>(this Func<Result<TResult>> @this, 
            Func<TException, Result<TResult>> catchFunction)
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
        #endregion

        #region 1 arg
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static Func<T, Result> Catch<T, TException>(this Func<T, Result> @this)
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
        /// <param name="catchFunction"></param>
        /// <returns></returns>
        public static Func<T, Result> Catch<T, TException>(this Func<T, Result> @this, Func<TException, Result> catchFunction)
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
        /// <returns></returns>
        public static Func<T, Result<TResult>> Catch<T, TResult, TException>(this Func<T, Result<TResult>> @this)
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
        /// <param name="catchFunction"></param>
        /// <returns></returns>
        public static Func<T, Result<TResult>> Catch<T, TResult, TException>(this Func<T, Result<TResult>> @this, 
            Func<TException, Result<TResult>> catchFunction)
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
        #endregion

        #region 2 args
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static Func<T1, T2, Result> Catch<T1, T2, TException>(this Func<T1 ,T2, Result> @this)
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
        public static Func<T1, T2, Result> Catch<T1, T2, TException>(this Func<T1, T2, Result> @this, Func<TException, Result> catchFunction)
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
        public static Func<T1, T2, Result<TResult>> Catch<T1, T2, TResult, TException>(this Func<T1, T2, Result<TResult>> @this)
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
        public static Func<T1, T2, Result<TResult>> Catch<T1, T2, TResult, TException>(this Func<T1, T2, Result<TResult>> @this, 
            Func<TException, Result<TResult>> catchFunction)
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
        #endregion

        #endregion
    }
}
