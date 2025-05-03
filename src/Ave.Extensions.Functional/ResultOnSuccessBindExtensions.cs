using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
    /// <summary>
    /// Provides extension methods for the Result monad that apply a binding function when the Result is successful.
    /// </summary>
    public static class ResultOnSuccessBindExtensions
    {
        /// <summary>
        /// If the Result is successful, applies a binding function that returns a new Result.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the source Result.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error in both Results.</typeparam>
        /// <param name="source">The source Result instance.</param>
        /// <param name="bind">A function that takes the value from the source Result and returns a new Result.</param>
        /// <returns>The result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static Result<Tout, E> OnSuccessBind<Tin, Tout, E>(this Result<Tin, E> source, Func<Tin, Result<Tout, E>> bind)
        {
            if (source.IsSuccess)
            {
                return bind(source.Value);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, applies a binding function that returns a new Result.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the source Result.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error in both Results.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result instance.</param>
        /// <param name="bind">A function that takes the value from the source Result and returns a new Result.</param>
        /// <returns>A task that resolves to the result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static async Task<Result<Tout, E>> OnSuccessBind<Tin, Tout, E>(this Task<Result<Tin, E>> awaitableSource, Func<Tin, Result<Tout, E>> bind)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                return bind(source.Value);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, applies an asynchronous binding function that returns a new Result.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the source Result.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error in both Results.</typeparam>
        /// <param name="source">The source Result instance.</param>
        /// <param name="awaitableBind">An asynchronous function that takes the value from the source Result and returns a new Result.</param>
        /// <returns>A task that resolves to the result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static async Task<Result<Tout, E>> OnSuccessBind<Tin, Tout, E>(this Result<Tin, E> source, Func<Tin, Task<Result<Tout, E>>> awaitableBind)
        {
            if (source.IsSuccess)
            {
                return await awaitableBind(source.Value).ConfigureAwait(false);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, applies an asynchronous binding function that returns a new Result.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the source Result.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error in both Results.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result instance.</param>
        /// <param name="awaitableBind">An asynchronous function that takes the value from the source Result and returns a new Result.</param>
        /// <returns>A task that resolves to the result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static async Task<Result<Tout, E>> OnSuccessBind<Tin, Tout, E>(this Task<Result<Tin, E>> awaitableSource, Func<Tin, Task<Result<Tout, E>>> awaitableBind)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                return await awaitableBind(source.Value).ConfigureAwait(false);
            }
            return Result<Tout, E>.Failure(source.Error);
        }
    }
}
