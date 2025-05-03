using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
    /// <summary>
    /// Provides extension methods for the Result monad that transform the error type when the Result is a failure.
    /// </summary>
    public static class ResultOnFailureExtensions
    {   
        /// <summary>
        /// If the Result is a failure, transforms its error using the provided mapping function.
        /// </summary>
        /// <typeparam name="T">The type of the value in the success case of the Result.</typeparam>
        /// <typeparam name="Ein">The original error type in the error case of the Result.</typeparam>
        /// <typeparam name="Eout">The new error type after transformation.</typeparam>
        /// <param name="source">The source Result instance.</param>
        /// <param name="mapError">A function that transforms the original error to a new error type.</param>
        /// <returns>A new Result with the same success value if successful, or with the transformed error if a failure.</returns>
        public static Result<T, Eout> OnFailure<T, Ein, Eout>(this Result<T, Ein> source, Func<Ein, Eout> mapError)
        {
            if(source.IsFailure)
            {
                return Result<T, Eout>.Failure(mapError(source.Error));
            }
            return Result<T, Eout>.Success(source.Value);
        }

        /// <summary>
        /// If the awaitable Result is a failure, transforms its error using the provided mapping function.
        /// </summary>
        /// <typeparam name="T">The type of the value in the success case of the Result.</typeparam>
        /// <typeparam name="Ein">The original error type in the error case of the Result.</typeparam>
        /// <typeparam name="Eout">The new error type after transformation.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result instance.</param>
        /// <param name="mapError">A function that transforms the original error to a new error type.</param>
        /// <returns>A task that resolves to a new Result with the same success value if successful, or with the transformed error if a failure.</returns>
        public static async Task<Result<T, Eout>> OnFailure<T, Ein, Eout>(this Task<Result<T, Ein>> awaitableSource, Func<Ein, Eout> mapError)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsFailure)
            {
                return Result<T, Eout>.Failure(mapError(source.Error));
            }
            return Result<T, Eout>.Success(source.Value);
        }

        /// <summary>
        /// If the Result is a failure, asynchronously transforms its error using the provided mapping function.
        /// </summary>
        /// <typeparam name="T">The type of the value in the success case of the Result.</typeparam>
        /// <typeparam name="Ein">The original error type in the error case of the Result.</typeparam>
        /// <typeparam name="Eout">The new error type after transformation.</typeparam>
        /// <param name="source">The source Result instance.</param>
        /// <param name="mapError">An asynchronous function that transforms the original error to a new error type.</param>
        /// <returns>A task that resolves to a new Result with the same success value if successful, or with the transformed error if a failure.</returns>
        public static async Task<Result<T, Eout>> OnFailure<T, Ein, Eout>(this Result<T, Ein> source, Func<Ein, Task<Eout>> mapError)
        {
            if (source.IsFailure)
            {
                return Result<T, Eout>.Failure(await mapError(source.Error).ConfigureAwait(false));
            }
            return Result<T, Eout>.Success(source.Value);
        }

        /// <summary>
        /// If the awaitable Result is a failure, asynchronously transforms its error using the provided mapping function.
        /// </summary>
        /// <typeparam name="T">The type of the value in the success case of the Result.</typeparam>
        /// <typeparam name="Ein">The original error type in the error case of the Result.</typeparam>
        /// <typeparam name="Eout">The new error type after transformation.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result instance.</param>
        /// <param name="mapError">An asynchronous function that transforms the original error to a new error type.</param>
        /// <returns>A task that resolves to a new Result with the same success value if successful, or with the transformed error if a failure.</returns>
        public static async Task<Result<T, Eout>> OnFailure<T, Ein, Eout>(this Task<Result<T, Ein>> awaitableSource, Func<Ein, Task<Eout>> mapError)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsFailure)
            {
                return Result<T, Eout>.Failure(await mapError(source.Error).ConfigureAwait(false));
            }
            return Result<T, Eout>.Success(source.Value);
        }
    }
}
