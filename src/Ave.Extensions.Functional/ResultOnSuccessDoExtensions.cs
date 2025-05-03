using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
    /// <summary>
    /// Provides extension methods for the Result monad that perform side effects when the Result is successful.
    /// </summary>
    public static class ResultOnSuccessDoExtensions
    {
        /// <summary>
        /// If the Result is successful, executes the provided action with the success value, then returns the original Result.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the success case of the Result.</typeparam>
        /// <typeparam name="E">The type of the error in the error case of the Result.</typeparam>
        /// <param name="source">The source Result instance.</param>
        /// <param name="action">The action to execute with the success value if the Result is successful.</param>
        /// <returns>The original Result, unchanged.</returns>
        public static Result<Tin, E> OnSuccessDo<Tin, E>(this Result<Tin, E> source, Action<Tin> action)
        {
            if (source.IsSuccess)
            {
                action(source.Value);
            }

            return source;
        }

        /// <summary>
        /// If the awaitable Result is successful, executes the provided action with the success value, then returns the original Result.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the success case of the Result.</typeparam>
        /// <typeparam name="E">The type of the error in the error case of the Result.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result instance.</param>
        /// <param name="action">The action to execute with the success value if the Result is successful.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<Tin, E>> OnSuccessDo<Tin, E>(this Task<Result<Tin, E>> awaitableSource, Action<Tin> action)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                action(source.Value);
            }

            return source;
        }

        /// <summary>
        /// If the Result is successful, executes the provided asynchronous action with the success value, then returns the original Result.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the success case of the Result.</typeparam>
        /// <typeparam name="E">The type of the error in the error case of the Result.</typeparam>
        /// <param name="source">The source Result instance.</param>
        /// <param name="awaitableAction">The asynchronous action to execute with the success value if the Result is successful.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<Tin, E>> OnSuccessDo<Tin, E>(this Result<Tin, E> source, Func<Tin, Task> awaitableAction)
        {
            if (source.IsSuccess)
            {
                await awaitableAction(source.Value).ConfigureAwait(false);
            }

            return source;
        }

        /// <summary>
        /// If the awaitable Result is successful, executes the provided asynchronous action with the success value, then returns the original Result.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the success case of the Result.</typeparam>
        /// <typeparam name="E">The type of the error in the error case of the Result.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result instance.</param>
        /// <param name="awaitableAction">The asynchronous action to execute with the success value if the Result is successful.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<Tin, E>> OnSuccessDo<Tin, E>(this Task<Result<Tin, E>> awaitableSource, Func<Tin, Task> awaitableAction)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                await awaitableAction(source.Value).ConfigureAwait(false);
            }

            return source;
        }
    }
}
