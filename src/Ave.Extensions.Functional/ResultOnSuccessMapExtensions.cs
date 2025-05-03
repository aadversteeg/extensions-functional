using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
    /// <summary>
    /// Provides extension methods for the Result monad that transform the success value when the Result is successful.
    /// </summary>
    public static class ResultOnSuccessMapExtensions
    {
        /// <summary>
        /// If the Result is successful, transforms its value using the provided mapping function.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the source Result.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error in both Results.</typeparam>
        /// <param name="source">The source Result instance.</param>
        /// <param name="map">A function that transforms the success value to a different type.</param>
        /// <returns>A new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static Result<Tout, E> OnSuccessMap<Tin, Tout, E>(this Result<Tin, E> source, Func<Tin, Tout> map)
        {
            if(source.IsSuccess)
            {
                return Result<Tout, E>.Success(map(source.Value));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, transforms its value using the provided mapping function.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the source Result.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error in both Results.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result instance.</param>
        /// <param name="map">A function that transforms the success value to a different type.</param>
        /// <returns>A task that resolves to a new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static async Task<Result<Tout, E>> OnSuccessMap<Tin, Tout, E>(this Task<Result<Tin, E>> awaitableSource, Func<Tin, Tout> map)
        {
            var source = await awaitableSource.ConfigureAwait(false);

            if (source.IsSuccess)
            {
                return Result<Tout, E>.Success(map(source.Value));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, asynchronously transforms its value using the provided mapping function.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the source Result.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error in both Results.</typeparam>
        /// <param name="source">The source Result instance.</param>
        /// <param name="map">An asynchronous function that transforms the success value to a different type.</param>
        /// <returns>A task that resolves to a new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static async Task<Result<Tout, E>> OnSuccessMap<Tin, Tout, E>(this Result<Tin, E> source, Func<Tin, Task<Tout>> map)
        {
            if (source.IsSuccess)
            {
                return Result<Tout, E>.Success(await map(source.Value).ConfigureAwait(false));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, asynchronously transforms its value using the provided mapping function.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the source Result.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error in both Results.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result instance.</param>
        /// <param name="map">An asynchronous function that transforms the success value to a different type.</param>
        /// <returns>A task that resolves to a new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static async Task<Result<Tout, E>> OnSuccessMap<Tin, Tout, E>(this Task<Result<Tin, E>> awaitableSource, Func<Tin, Task<Tout>> map)
        {
            var source = await awaitableSource.ConfigureAwait(false);

            if (source.IsSuccess)
            {
                return Result<Tout, E>.Success(await map(source.Value).ConfigureAwait(false));
            }
            return Result<Tout, E>.Failure(source.Error);
        }
    }
}
