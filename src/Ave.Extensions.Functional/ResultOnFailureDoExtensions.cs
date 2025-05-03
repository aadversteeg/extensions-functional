using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
    /// <summary>
    /// Provides extension methods for the Result monad that perform side effects when the Result is a failure.
    /// </summary>
    public static class ResultOnFailureDoExtensions
    {
        /// <summary>
        /// If the Result is a failure, executes the provided action with the error value, then returns the original Result.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the success case of the Result.</typeparam>
        /// <typeparam name="E">The type of the error in the error case of the Result.</typeparam>
        /// <param name="source">The source Result instance.</param>
        /// <param name="action">The action to execute with the error value if the Result is a failure.</param>
        /// <returns>The original Result, unchanged.</returns>
        public static Result<Tin, E> OnFailureDo<Tin, E>(this Result<Tin, E> source, Action<E> action)
        {
            if (source.IsFailure)
            {
                action(source.Error);
            }

            return source;
        }

        /// <summary>
        /// If the awaitable Result is a failure, executes the provided action with the error value, then returns the original Result.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the success case of the Result.</typeparam>
        /// <typeparam name="E">The type of the error in the error case of the Result.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result instance.</param>
        /// <param name="action">The action to execute with the error value if the Result is a failure.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<Tin, E>> OnFailureDo<Tin, E>(this Task<Result<Tin, E>> awaitableSource, Action<E> action)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsFailure)
            {
                action(source.Error);
            }

            return source;
        }

        /// <summary>
        /// If the Result is a failure, executes the provided asynchronous action with the error value, then returns the original Result.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the success case of the Result.</typeparam>
        /// <typeparam name="E">The type of the error in the error case of the Result.</typeparam>
        /// <param name="source">The source Result instance.</param>
        /// <param name="awaitableAction">The asynchronous action to execute with the error value if the Result is a failure.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<Tin, E>> OnFailureDo<Tin, E>(this Result<Tin, E> source, Func<E, Task> awaitableAction)
        {
            if (source.IsFailure)
            {
                await awaitableAction(source.Error).ConfigureAwait(false);
            }

            return source;
        }

        /// <summary>
        /// If the awaitable Result is a failure, executes the provided asynchronous action with the error value, then returns the original Result.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the success case of the Result.</typeparam>
        /// <typeparam name="E">The type of the error in the error case of the Result.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result instance.</param>
        /// <param name="awaitableAction">The asynchronous action to execute with the error value if the Result is a failure.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<Tin, E>> OnFailureDo<Tin, E>(this Task<Result<Tin, E>> awaitableSource, Func<E, Task> awaitableAction)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsFailure)
            {
                await awaitableAction(source.Error).ConfigureAwait(false);
            }

            return source;
        }
    }
}
