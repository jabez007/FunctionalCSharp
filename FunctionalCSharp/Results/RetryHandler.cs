using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunctionalCSharp.Results
{
    /// <summary>
    ///
    /// </summary>
    public static class RetryHandler
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="Tin"></typeparam>
        /// <typeparam name="Tout"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="input"></param>
        /// <param name="numberOfRetries"></param>
        /// <param name="timeBetweenRetries"></param>
        /// <returns></returns>
        public static IResult<Tout> RetryOnException<Tin, Tout, TException>(this Func<Tin, IResult<Tout>> @this, Tin input,
            int numberOfRetries = 3, TimeSpan timeBetweenRetries = default)
                where TException : Exception
        {
            var attempts = 0;
            string errorMessage = "";
            do
            {
                attempts++;
                if (attempts > numberOfRetries)
                    return Result<Tout>.Failure("Maximum number of retires({1}) reached:{0}{2}"
                        .Format(Environment.NewLine, numberOfRetries, errorMessage));

                var result = @this.Catch<Tin, Tout, TException>()(input);
                if (result.IsSuccess)
                    return result;
                else
                    errorMessage = result.ErrorMessage;

                Thread.Sleep(timeBetweenRetries);
            } while (true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="Tin"></typeparam>
        /// <typeparam name="Tout"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="this"></param>
        /// <param name="input"></param>
        /// <param name="numberOfRetries"></param>
        /// <param name="timeBetweenRetries"></param>
        /// <returns></returns>
        public static async Task<IResult<Tout>> RetryOnExceptionAsync<Tin, Tout, TException>(this Func<Tin, Task<IResult<Tout>>> @this, Tin input,
            int numberOfRetries = 3, TimeSpan timeBetweenRetries = default)
                where TException : Exception
        {
            var attempts = 0;
            string errorMessage = "";
            do
            {
                attempts++;
                if (attempts > numberOfRetries)
                    return Result<Tout>.Failure("Maximum number of retires({1}) reached:{0}{2}"
                        .Format(Environment.NewLine, numberOfRetries, errorMessage));

                var result = await @this.CatchAsync<Tin, Tout, TException>()(input);
                if (result.IsSuccess)
                    return result;
                else
                    errorMessage = result.ErrorMessage;

                Thread.Sleep(timeBetweenRetries);
            } while (true);
        }
    }
}