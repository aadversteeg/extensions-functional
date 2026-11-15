using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
    /// <summary>
    /// Provides OnSuccessDo overloads for a Result holding a tuple of 2 to 6 values, passing the values as separate arguments.
    /// </summary>
    public static class ResultOnSuccessDoTupleExtensions
    {
        /// <summary>
        /// If the Result is successful, executes the provided action with its 2 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 2 values.</param>
        /// <param name="action">The action to execute with the 2 values if the Result is successful.</param>
        /// <returns>The original Result, unchanged.</returns>
        public static Result<(T1, T2), E> OnSuccessDo<T1, T2, E>(this Result<(T1, T2), E> source, Action<T1, T2> action)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                action(value.Item1, value.Item2);
            }

            return source;
        }

        /// <summary>
        /// If the awaitable Result is successful, executes the provided action with its 2 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 2 values.</param>
        /// <param name="action">The action to execute with the 2 values if the Result is successful.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<(T1, T2), E>> OnSuccessDo<T1, T2, E>(this Task<Result<(T1, T2), E>> awaitableSource, Action<T1, T2> action)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                action(value.Item1, value.Item2);
            }

            return source;
        }

        /// <summary>
        /// If the Result is successful, executes the provided asynchronous action with its 2 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 2 values.</param>
        /// <param name="awaitableAction">The asynchronous action to execute with the 2 values if the Result is successful.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<(T1, T2), E>> OnSuccessDo<T1, T2, E>(this Result<(T1, T2), E> source, Func<T1, T2, Task> awaitableAction)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                await awaitableAction(value.Item1, value.Item2).ConfigureAwait(false);
            }

            return source;
        }

        /// <summary>
        /// If the awaitable Result is successful, executes the provided asynchronous action with its 2 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 2 values.</param>
        /// <param name="awaitableAction">The asynchronous action to execute with the 2 values if the Result is successful.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<(T1, T2), E>> OnSuccessDo<T1, T2, E>(this Task<Result<(T1, T2), E>> awaitableSource, Func<T1, T2, Task> awaitableAction)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                await awaitableAction(value.Item1, value.Item2).ConfigureAwait(false);
            }

            return source;
        }

        /// <summary>
        /// If the Result is successful, executes the provided action with its 3 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 3 values.</param>
        /// <param name="action">The action to execute with the 3 values if the Result is successful.</param>
        /// <returns>The original Result, unchanged.</returns>
        public static Result<(T1, T2, T3), E> OnSuccessDo<T1, T2, T3, E>(this Result<(T1, T2, T3), E> source, Action<T1, T2, T3> action)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                action(value.Item1, value.Item2, value.Item3);
            }

            return source;
        }

        /// <summary>
        /// If the awaitable Result is successful, executes the provided action with its 3 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 3 values.</param>
        /// <param name="action">The action to execute with the 3 values if the Result is successful.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<(T1, T2, T3), E>> OnSuccessDo<T1, T2, T3, E>(this Task<Result<(T1, T2, T3), E>> awaitableSource, Action<T1, T2, T3> action)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                action(value.Item1, value.Item2, value.Item3);
            }

            return source;
        }

        /// <summary>
        /// If the Result is successful, executes the provided asynchronous action with its 3 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 3 values.</param>
        /// <param name="awaitableAction">The asynchronous action to execute with the 3 values if the Result is successful.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<(T1, T2, T3), E>> OnSuccessDo<T1, T2, T3, E>(this Result<(T1, T2, T3), E> source, Func<T1, T2, T3, Task> awaitableAction)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                await awaitableAction(value.Item1, value.Item2, value.Item3).ConfigureAwait(false);
            }

            return source;
        }

        /// <summary>
        /// If the awaitable Result is successful, executes the provided asynchronous action with its 3 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 3 values.</param>
        /// <param name="awaitableAction">The asynchronous action to execute with the 3 values if the Result is successful.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<(T1, T2, T3), E>> OnSuccessDo<T1, T2, T3, E>(this Task<Result<(T1, T2, T3), E>> awaitableSource, Func<T1, T2, T3, Task> awaitableAction)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                await awaitableAction(value.Item1, value.Item2, value.Item3).ConfigureAwait(false);
            }

            return source;
        }

        /// <summary>
        /// If the Result is successful, executes the provided action with its 4 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 4 values.</param>
        /// <param name="action">The action to execute with the 4 values if the Result is successful.</param>
        /// <returns>The original Result, unchanged.</returns>
        public static Result<(T1, T2, T3, T4), E> OnSuccessDo<T1, T2, T3, T4, E>(this Result<(T1, T2, T3, T4), E> source, Action<T1, T2, T3, T4> action)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                action(value.Item1, value.Item2, value.Item3, value.Item4);
            }

            return source;
        }

        /// <summary>
        /// If the awaitable Result is successful, executes the provided action with its 4 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 4 values.</param>
        /// <param name="action">The action to execute with the 4 values if the Result is successful.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<(T1, T2, T3, T4), E>> OnSuccessDo<T1, T2, T3, T4, E>(this Task<Result<(T1, T2, T3, T4), E>> awaitableSource, Action<T1, T2, T3, T4> action)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                action(value.Item1, value.Item2, value.Item3, value.Item4);
            }

            return source;
        }

        /// <summary>
        /// If the Result is successful, executes the provided asynchronous action with its 4 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 4 values.</param>
        /// <param name="awaitableAction">The asynchronous action to execute with the 4 values if the Result is successful.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<(T1, T2, T3, T4), E>> OnSuccessDo<T1, T2, T3, T4, E>(this Result<(T1, T2, T3, T4), E> source, Func<T1, T2, T3, T4, Task> awaitableAction)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                await awaitableAction(value.Item1, value.Item2, value.Item3, value.Item4).ConfigureAwait(false);
            }

            return source;
        }

        /// <summary>
        /// If the awaitable Result is successful, executes the provided asynchronous action with its 4 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 4 values.</param>
        /// <param name="awaitableAction">The asynchronous action to execute with the 4 values if the Result is successful.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<(T1, T2, T3, T4), E>> OnSuccessDo<T1, T2, T3, T4, E>(this Task<Result<(T1, T2, T3, T4), E>> awaitableSource, Func<T1, T2, T3, T4, Task> awaitableAction)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                await awaitableAction(value.Item1, value.Item2, value.Item3, value.Item4).ConfigureAwait(false);
            }

            return source;
        }

        /// <summary>
        /// If the Result is successful, executes the provided action with its 5 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 5 values.</param>
        /// <param name="action">The action to execute with the 5 values if the Result is successful.</param>
        /// <returns>The original Result, unchanged.</returns>
        public static Result<(T1, T2, T3, T4, T5), E> OnSuccessDo<T1, T2, T3, T4, T5, E>(this Result<(T1, T2, T3, T4, T5), E> source, Action<T1, T2, T3, T4, T5> action)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                action(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
            }

            return source;
        }

        /// <summary>
        /// If the awaitable Result is successful, executes the provided action with its 5 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 5 values.</param>
        /// <param name="action">The action to execute with the 5 values if the Result is successful.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<(T1, T2, T3, T4, T5), E>> OnSuccessDo<T1, T2, T3, T4, T5, E>(this Task<Result<(T1, T2, T3, T4, T5), E>> awaitableSource, Action<T1, T2, T3, T4, T5> action)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                action(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
            }

            return source;
        }

        /// <summary>
        /// If the Result is successful, executes the provided asynchronous action with its 5 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 5 values.</param>
        /// <param name="awaitableAction">The asynchronous action to execute with the 5 values if the Result is successful.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<(T1, T2, T3, T4, T5), E>> OnSuccessDo<T1, T2, T3, T4, T5, E>(this Result<(T1, T2, T3, T4, T5), E> source, Func<T1, T2, T3, T4, T5, Task> awaitableAction)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                await awaitableAction(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5).ConfigureAwait(false);
            }

            return source;
        }

        /// <summary>
        /// If the awaitable Result is successful, executes the provided asynchronous action with its 5 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 5 values.</param>
        /// <param name="awaitableAction">The asynchronous action to execute with the 5 values if the Result is successful.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<(T1, T2, T3, T4, T5), E>> OnSuccessDo<T1, T2, T3, T4, T5, E>(this Task<Result<(T1, T2, T3, T4, T5), E>> awaitableSource, Func<T1, T2, T3, T4, T5, Task> awaitableAction)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                await awaitableAction(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5).ConfigureAwait(false);
            }

            return source;
        }

        /// <summary>
        /// If the Result is successful, executes the provided action with its 6 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="T6">The type of the sixth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 6 values.</param>
        /// <param name="action">The action to execute with the 6 values if the Result is successful.</param>
        /// <returns>The original Result, unchanged.</returns>
        public static Result<(T1, T2, T3, T4, T5, T6), E> OnSuccessDo<T1, T2, T3, T4, T5, T6, E>(this Result<(T1, T2, T3, T4, T5, T6), E> source, Action<T1, T2, T3, T4, T5, T6> action)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                action(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
            }

            return source;
        }

        /// <summary>
        /// If the awaitable Result is successful, executes the provided action with its 6 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="T6">The type of the sixth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 6 values.</param>
        /// <param name="action">The action to execute with the 6 values if the Result is successful.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<(T1, T2, T3, T4, T5, T6), E>> OnSuccessDo<T1, T2, T3, T4, T5, T6, E>(this Task<Result<(T1, T2, T3, T4, T5, T6), E>> awaitableSource, Action<T1, T2, T3, T4, T5, T6> action)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                action(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
            }

            return source;
        }

        /// <summary>
        /// If the Result is successful, executes the provided asynchronous action with its 6 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="T6">The type of the sixth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 6 values.</param>
        /// <param name="awaitableAction">The asynchronous action to execute with the 6 values if the Result is successful.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<(T1, T2, T3, T4, T5, T6), E>> OnSuccessDo<T1, T2, T3, T4, T5, T6, E>(this Result<(T1, T2, T3, T4, T5, T6), E> source, Func<T1, T2, T3, T4, T5, T6, Task> awaitableAction)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                await awaitableAction(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6).ConfigureAwait(false);
            }

            return source;
        }

        /// <summary>
        /// If the awaitable Result is successful, executes the provided asynchronous action with its 6 values, passed as separate arguments, then returns the original Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="T6">The type of the sixth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 6 values.</param>
        /// <param name="awaitableAction">The asynchronous action to execute with the 6 values if the Result is successful.</param>
        /// <returns>A task that resolves to the original Result, unchanged.</returns>
        public static async Task<Result<(T1, T2, T3, T4, T5, T6), E>> OnSuccessDo<T1, T2, T3, T4, T5, T6, E>(this Task<Result<(T1, T2, T3, T4, T5, T6), E>> awaitableSource, Func<T1, T2, T3, T4, T5, T6, Task> awaitableAction)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                await awaitableAction(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6).ConfigureAwait(false);
            }

            return source;
        }
    }
}
