using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
    /// <summary>
    /// Provides extension methods for pattern matching on Result types.
    /// </summary>
    public static class ResultMatchExtensions
    {
        /// <summary>
        /// Applies pattern matching to a Result by executing one of two functions based on whether the Result is successful or not.
        /// </summary>
        /// <typeparam name="TIn">The type of the value in the success case of the Result.</typeparam>
        /// <typeparam name="TOut">The type of the value returned by both match functions.</typeparam>
        /// <typeparam name="E">The type of the error in the error case of the Result.</typeparam>
        /// <param name="source">The source Result instance.</param>
        /// <param name="onSuccess">The function to execute if the Result is successful, taking the success value as input.</param>
        /// <param name="onError">The function to execute if the Result is an error, taking the error value as input.</param>
        /// <returns>The result of either the onSuccess or onError function, depending on the state of the Result.</returns>
        public static TOut Match<TIn, TOut, E>(this Result<TIn, E> source, Func<TIn, TOut> onSuccess, Func<E, TOut> onError)
        {
            if(source.IsSuccess) {
                return onSuccess(source.Value);
            }

            return onError(source.Error);
        }

        /// <summary>
        /// Applies pattern matching to an awaitable Result by executing one of two functions based on whether the Result is successful or not.
        /// </summary>
        /// <typeparam name="TIn">The type of the value in the success case of the Result.</typeparam>
        /// <typeparam name="TOut">The type of the value returned by both match functions.</typeparam>
        /// <typeparam name="E">The type of the error in the error case of the Result.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result instance.</param>
        /// <param name="onSuccess">The function to execute if the Result is successful, taking the success value as input.</param>
        /// <param name="onError">The function to execute if the Result is an error, taking the error value as input.</param>
        /// <returns>A task that resolves to the result of either the onSuccess or onError function, depending on the state of the Result.</returns>
        public static async Task<TOut> Match<TIn, TOut, E>(this Task<Result<TIn, E>> awaitableSource, Func<TIn, TOut> onSuccess, Func<E, TOut> onError)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                return onSuccess(source.Value);
            }

            return onError(source.Error);
        }
    }
}
