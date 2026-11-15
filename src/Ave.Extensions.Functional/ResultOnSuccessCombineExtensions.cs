using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
    /// <summary>
    /// Provides extension methods for the Result monad that run a next step when the Result is successful and keep the earlier values: the success value grows into a tuple, so a later step can use several earlier values without nesting.
    /// </summary>
    public static class ResultOnSuccessCombineExtensions
    {
        /// <summary>
        /// If the Result is successful, runs the next step with the value and returns the earlier value together with the step's value as a tuple of 2 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result.</param>
        /// <param name="combine">The next step; it receives the value and returns a Result with the second value.</param>
        /// <returns>A Result with the 2 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static Result<(T1, T2), E> OnSuccessCombine<T1, T2, E>(this Result<T1, E> source, Func<T1, Result<T2, E>> combine)
        {
            if (source.IsFailure)
            {
                return Result<(T1, T2), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = combine(value);
            if (next.IsFailure)
            {
                return Result<(T1, T2), E>.Failure(next.Error);
            }
            return Result<(T1, T2), E>.Success((value, next.Value));
        }

        /// <summary>
        /// If the awaitable Result is successful, runs the next step with the value and returns the earlier value together with the step's value as a tuple of 2 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result.</param>
        /// <param name="combine">The next step; it receives the value and returns a Result with the second value.</param>
        /// <returns>A task that resolves to a Result with the 2 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static async Task<Result<(T1, T2), E>> OnSuccessCombine<T1, T2, E>(this Task<Result<T1, E>> awaitableSource, Func<T1, Result<T2, E>> combine)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsFailure)
            {
                return Result<(T1, T2), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = combine(value);
            if (next.IsFailure)
            {
                return Result<(T1, T2), E>.Failure(next.Error);
            }
            return Result<(T1, T2), E>.Success((value, next.Value));
        }

        /// <summary>
        /// If the Result is successful, runs the asynchronous next step with the value and returns the earlier value together with the step's value as a tuple of 2 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result.</param>
        /// <param name="awaitableCombine">The asynchronous next step; it receives the value and returns a Result with the second value.</param>
        /// <returns>A task that resolves to a Result with the 2 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static async Task<Result<(T1, T2), E>> OnSuccessCombine<T1, T2, E>(this Result<T1, E> source, Func<T1, Task<Result<T2, E>>> awaitableCombine)
        {
            if (source.IsFailure)
            {
                return Result<(T1, T2), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = await awaitableCombine(value).ConfigureAwait(false);
            if (next.IsFailure)
            {
                return Result<(T1, T2), E>.Failure(next.Error);
            }
            return Result<(T1, T2), E>.Success((value, next.Value));
        }

        /// <summary>
        /// If the awaitable Result is successful, runs the asynchronous next step with the value and returns the earlier value together with the step's value as a tuple of 2 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result.</param>
        /// <param name="awaitableCombine">The asynchronous next step; it receives the value and returns a Result with the second value.</param>
        /// <returns>A task that resolves to a Result with the 2 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static async Task<Result<(T1, T2), E>> OnSuccessCombine<T1, T2, E>(this Task<Result<T1, E>> awaitableSource, Func<T1, Task<Result<T2, E>>> awaitableCombine)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsFailure)
            {
                return Result<(T1, T2), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = await awaitableCombine(value).ConfigureAwait(false);
            if (next.IsFailure)
            {
                return Result<(T1, T2), E>.Failure(next.Error);
            }
            return Result<(T1, T2), E>.Success((value, next.Value));
        }

        /// <summary>
        /// If the Result is successful, runs the next step with the 2 values as separate arguments and returns the earlier values together with the step's value as a tuple of 3 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 2 values.</param>
        /// <param name="combine">The next step; it receives the 2 values as separate arguments and returns a Result with the third value.</param>
        /// <returns>A Result with the 3 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static Result<(T1, T2, T3), E> OnSuccessCombine<T1, T2, T3, E>(this Result<(T1, T2), E> source, Func<T1, T2, Result<T3, E>> combine)
        {
            if (source.IsFailure)
            {
                return Result<(T1, T2, T3), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = combine(value.Item1, value.Item2);
            if (next.IsFailure)
            {
                return Result<(T1, T2, T3), E>.Failure(next.Error);
            }
            return Result<(T1, T2, T3), E>.Success((value.Item1, value.Item2, next.Value));
        }

        /// <summary>
        /// If the awaitable Result is successful, runs the next step with the 2 values as separate arguments and returns the earlier values together with the step's value as a tuple of 3 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 2 values.</param>
        /// <param name="combine">The next step; it receives the 2 values as separate arguments and returns a Result with the third value.</param>
        /// <returns>A task that resolves to a Result with the 3 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static async Task<Result<(T1, T2, T3), E>> OnSuccessCombine<T1, T2, T3, E>(this Task<Result<(T1, T2), E>> awaitableSource, Func<T1, T2, Result<T3, E>> combine)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsFailure)
            {
                return Result<(T1, T2, T3), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = combine(value.Item1, value.Item2);
            if (next.IsFailure)
            {
                return Result<(T1, T2, T3), E>.Failure(next.Error);
            }
            return Result<(T1, T2, T3), E>.Success((value.Item1, value.Item2, next.Value));
        }

        /// <summary>
        /// If the Result is successful, runs the asynchronous next step with the 2 values as separate arguments and returns the earlier values together with the step's value as a tuple of 3 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 2 values.</param>
        /// <param name="awaitableCombine">The asynchronous next step; it receives the 2 values as separate arguments and returns a Result with the third value.</param>
        /// <returns>A task that resolves to a Result with the 3 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static async Task<Result<(T1, T2, T3), E>> OnSuccessCombine<T1, T2, T3, E>(this Result<(T1, T2), E> source, Func<T1, T2, Task<Result<T3, E>>> awaitableCombine)
        {
            if (source.IsFailure)
            {
                return Result<(T1, T2, T3), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = await awaitableCombine(value.Item1, value.Item2).ConfigureAwait(false);
            if (next.IsFailure)
            {
                return Result<(T1, T2, T3), E>.Failure(next.Error);
            }
            return Result<(T1, T2, T3), E>.Success((value.Item1, value.Item2, next.Value));
        }

        /// <summary>
        /// If the awaitable Result is successful, runs the asynchronous next step with the 2 values as separate arguments and returns the earlier values together with the step's value as a tuple of 3 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 2 values.</param>
        /// <param name="awaitableCombine">The asynchronous next step; it receives the 2 values as separate arguments and returns a Result with the third value.</param>
        /// <returns>A task that resolves to a Result with the 3 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static async Task<Result<(T1, T2, T3), E>> OnSuccessCombine<T1, T2, T3, E>(this Task<Result<(T1, T2), E>> awaitableSource, Func<T1, T2, Task<Result<T3, E>>> awaitableCombine)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsFailure)
            {
                return Result<(T1, T2, T3), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = await awaitableCombine(value.Item1, value.Item2).ConfigureAwait(false);
            if (next.IsFailure)
            {
                return Result<(T1, T2, T3), E>.Failure(next.Error);
            }
            return Result<(T1, T2, T3), E>.Success((value.Item1, value.Item2, next.Value));
        }

        /// <summary>
        /// If the Result is successful, runs the next step with the 3 values as separate arguments and returns the earlier values together with the step's value as a tuple of 4 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 3 values.</param>
        /// <param name="combine">The next step; it receives the 3 values as separate arguments and returns a Result with the fourth value.</param>
        /// <returns>A Result with the 4 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static Result<(T1, T2, T3, T4), E> OnSuccessCombine<T1, T2, T3, T4, E>(this Result<(T1, T2, T3), E> source, Func<T1, T2, T3, Result<T4, E>> combine)
        {
            if (source.IsFailure)
            {
                return Result<(T1, T2, T3, T4), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = combine(value.Item1, value.Item2, value.Item3);
            if (next.IsFailure)
            {
                return Result<(T1, T2, T3, T4), E>.Failure(next.Error);
            }
            return Result<(T1, T2, T3, T4), E>.Success((value.Item1, value.Item2, value.Item3, next.Value));
        }

        /// <summary>
        /// If the awaitable Result is successful, runs the next step with the 3 values as separate arguments and returns the earlier values together with the step's value as a tuple of 4 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 3 values.</param>
        /// <param name="combine">The next step; it receives the 3 values as separate arguments and returns a Result with the fourth value.</param>
        /// <returns>A task that resolves to a Result with the 4 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static async Task<Result<(T1, T2, T3, T4), E>> OnSuccessCombine<T1, T2, T3, T4, E>(this Task<Result<(T1, T2, T3), E>> awaitableSource, Func<T1, T2, T3, Result<T4, E>> combine)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsFailure)
            {
                return Result<(T1, T2, T3, T4), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = combine(value.Item1, value.Item2, value.Item3);
            if (next.IsFailure)
            {
                return Result<(T1, T2, T3, T4), E>.Failure(next.Error);
            }
            return Result<(T1, T2, T3, T4), E>.Success((value.Item1, value.Item2, value.Item3, next.Value));
        }

        /// <summary>
        /// If the Result is successful, runs the asynchronous next step with the 3 values as separate arguments and returns the earlier values together with the step's value as a tuple of 4 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 3 values.</param>
        /// <param name="awaitableCombine">The asynchronous next step; it receives the 3 values as separate arguments and returns a Result with the fourth value.</param>
        /// <returns>A task that resolves to a Result with the 4 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static async Task<Result<(T1, T2, T3, T4), E>> OnSuccessCombine<T1, T2, T3, T4, E>(this Result<(T1, T2, T3), E> source, Func<T1, T2, T3, Task<Result<T4, E>>> awaitableCombine)
        {
            if (source.IsFailure)
            {
                return Result<(T1, T2, T3, T4), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = await awaitableCombine(value.Item1, value.Item2, value.Item3).ConfigureAwait(false);
            if (next.IsFailure)
            {
                return Result<(T1, T2, T3, T4), E>.Failure(next.Error);
            }
            return Result<(T1, T2, T3, T4), E>.Success((value.Item1, value.Item2, value.Item3, next.Value));
        }

        /// <summary>
        /// If the awaitable Result is successful, runs the asynchronous next step with the 3 values as separate arguments and returns the earlier values together with the step's value as a tuple of 4 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 3 values.</param>
        /// <param name="awaitableCombine">The asynchronous next step; it receives the 3 values as separate arguments and returns a Result with the fourth value.</param>
        /// <returns>A task that resolves to a Result with the 4 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static async Task<Result<(T1, T2, T3, T4), E>> OnSuccessCombine<T1, T2, T3, T4, E>(this Task<Result<(T1, T2, T3), E>> awaitableSource, Func<T1, T2, T3, Task<Result<T4, E>>> awaitableCombine)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsFailure)
            {
                return Result<(T1, T2, T3, T4), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = await awaitableCombine(value.Item1, value.Item2, value.Item3).ConfigureAwait(false);
            if (next.IsFailure)
            {
                return Result<(T1, T2, T3, T4), E>.Failure(next.Error);
            }
            return Result<(T1, T2, T3, T4), E>.Success((value.Item1, value.Item2, value.Item3, next.Value));
        }

        /// <summary>
        /// If the Result is successful, runs the next step with the 4 values as separate arguments and returns the earlier values together with the step's value as a tuple of 5 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 4 values.</param>
        /// <param name="combine">The next step; it receives the 4 values as separate arguments and returns a Result with the fifth value.</param>
        /// <returns>A Result with the 5 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static Result<(T1, T2, T3, T4, T5), E> OnSuccessCombine<T1, T2, T3, T4, T5, E>(this Result<(T1, T2, T3, T4), E> source, Func<T1, T2, T3, T4, Result<T5, E>> combine)
        {
            if (source.IsFailure)
            {
                return Result<(T1, T2, T3, T4, T5), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = combine(value.Item1, value.Item2, value.Item3, value.Item4);
            if (next.IsFailure)
            {
                return Result<(T1, T2, T3, T4, T5), E>.Failure(next.Error);
            }
            return Result<(T1, T2, T3, T4, T5), E>.Success((value.Item1, value.Item2, value.Item3, value.Item4, next.Value));
        }

        /// <summary>
        /// If the awaitable Result is successful, runs the next step with the 4 values as separate arguments and returns the earlier values together with the step's value as a tuple of 5 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 4 values.</param>
        /// <param name="combine">The next step; it receives the 4 values as separate arguments and returns a Result with the fifth value.</param>
        /// <returns>A task that resolves to a Result with the 5 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static async Task<Result<(T1, T2, T3, T4, T5), E>> OnSuccessCombine<T1, T2, T3, T4, T5, E>(this Task<Result<(T1, T2, T3, T4), E>> awaitableSource, Func<T1, T2, T3, T4, Result<T5, E>> combine)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsFailure)
            {
                return Result<(T1, T2, T3, T4, T5), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = combine(value.Item1, value.Item2, value.Item3, value.Item4);
            if (next.IsFailure)
            {
                return Result<(T1, T2, T3, T4, T5), E>.Failure(next.Error);
            }
            return Result<(T1, T2, T3, T4, T5), E>.Success((value.Item1, value.Item2, value.Item3, value.Item4, next.Value));
        }

        /// <summary>
        /// If the Result is successful, runs the asynchronous next step with the 4 values as separate arguments and returns the earlier values together with the step's value as a tuple of 5 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 4 values.</param>
        /// <param name="awaitableCombine">The asynchronous next step; it receives the 4 values as separate arguments and returns a Result with the fifth value.</param>
        /// <returns>A task that resolves to a Result with the 5 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static async Task<Result<(T1, T2, T3, T4, T5), E>> OnSuccessCombine<T1, T2, T3, T4, T5, E>(this Result<(T1, T2, T3, T4), E> source, Func<T1, T2, T3, T4, Task<Result<T5, E>>> awaitableCombine)
        {
            if (source.IsFailure)
            {
                return Result<(T1, T2, T3, T4, T5), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = await awaitableCombine(value.Item1, value.Item2, value.Item3, value.Item4).ConfigureAwait(false);
            if (next.IsFailure)
            {
                return Result<(T1, T2, T3, T4, T5), E>.Failure(next.Error);
            }
            return Result<(T1, T2, T3, T4, T5), E>.Success((value.Item1, value.Item2, value.Item3, value.Item4, next.Value));
        }

        /// <summary>
        /// If the awaitable Result is successful, runs the asynchronous next step with the 4 values as separate arguments and returns the earlier values together with the step's value as a tuple of 5 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 4 values.</param>
        /// <param name="awaitableCombine">The asynchronous next step; it receives the 4 values as separate arguments and returns a Result with the fifth value.</param>
        /// <returns>A task that resolves to a Result with the 5 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static async Task<Result<(T1, T2, T3, T4, T5), E>> OnSuccessCombine<T1, T2, T3, T4, T5, E>(this Task<Result<(T1, T2, T3, T4), E>> awaitableSource, Func<T1, T2, T3, T4, Task<Result<T5, E>>> awaitableCombine)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsFailure)
            {
                return Result<(T1, T2, T3, T4, T5), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = await awaitableCombine(value.Item1, value.Item2, value.Item3, value.Item4).ConfigureAwait(false);
            if (next.IsFailure)
            {
                return Result<(T1, T2, T3, T4, T5), E>.Failure(next.Error);
            }
            return Result<(T1, T2, T3, T4, T5), E>.Success((value.Item1, value.Item2, value.Item3, value.Item4, next.Value));
        }

        /// <summary>
        /// If the Result is successful, runs the next step with the 5 values as separate arguments and returns the earlier values together with the step's value as a tuple of 6 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="T6">The type of the sixth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 5 values.</param>
        /// <param name="combine">The next step; it receives the 5 values as separate arguments and returns a Result with the sixth value.</param>
        /// <returns>A Result with the 6 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static Result<(T1, T2, T3, T4, T5, T6), E> OnSuccessCombine<T1, T2, T3, T4, T5, T6, E>(this Result<(T1, T2, T3, T4, T5), E> source, Func<T1, T2, T3, T4, T5, Result<T6, E>> combine)
        {
            if (source.IsFailure)
            {
                return Result<(T1, T2, T3, T4, T5, T6), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = combine(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
            if (next.IsFailure)
            {
                return Result<(T1, T2, T3, T4, T5, T6), E>.Failure(next.Error);
            }
            return Result<(T1, T2, T3, T4, T5, T6), E>.Success((value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, next.Value));
        }

        /// <summary>
        /// If the awaitable Result is successful, runs the next step with the 5 values as separate arguments and returns the earlier values together with the step's value as a tuple of 6 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="T6">The type of the sixth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 5 values.</param>
        /// <param name="combine">The next step; it receives the 5 values as separate arguments and returns a Result with the sixth value.</param>
        /// <returns>A task that resolves to a Result with the 6 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static async Task<Result<(T1, T2, T3, T4, T5, T6), E>> OnSuccessCombine<T1, T2, T3, T4, T5, T6, E>(this Task<Result<(T1, T2, T3, T4, T5), E>> awaitableSource, Func<T1, T2, T3, T4, T5, Result<T6, E>> combine)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsFailure)
            {
                return Result<(T1, T2, T3, T4, T5, T6), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = combine(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
            if (next.IsFailure)
            {
                return Result<(T1, T2, T3, T4, T5, T6), E>.Failure(next.Error);
            }
            return Result<(T1, T2, T3, T4, T5, T6), E>.Success((value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, next.Value));
        }

        /// <summary>
        /// If the Result is successful, runs the asynchronous next step with the 5 values as separate arguments and returns the earlier values together with the step's value as a tuple of 6 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="T6">The type of the sixth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 5 values.</param>
        /// <param name="awaitableCombine">The asynchronous next step; it receives the 5 values as separate arguments and returns a Result with the sixth value.</param>
        /// <returns>A task that resolves to a Result with the 6 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static async Task<Result<(T1, T2, T3, T4, T5, T6), E>> OnSuccessCombine<T1, T2, T3, T4, T5, T6, E>(this Result<(T1, T2, T3, T4, T5), E> source, Func<T1, T2, T3, T4, T5, Task<Result<T6, E>>> awaitableCombine)
        {
            if (source.IsFailure)
            {
                return Result<(T1, T2, T3, T4, T5, T6), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = await awaitableCombine(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5).ConfigureAwait(false);
            if (next.IsFailure)
            {
                return Result<(T1, T2, T3, T4, T5, T6), E>.Failure(next.Error);
            }
            return Result<(T1, T2, T3, T4, T5, T6), E>.Success((value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, next.Value));
        }

        /// <summary>
        /// If the awaitable Result is successful, runs the asynchronous next step with the 5 values as separate arguments and returns the earlier values together with the step's value as a tuple of 6 values. A failure of the source or of the step is returned unchanged; after a failure of the source the step is not run.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="T6">The type of the sixth value.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 5 values.</param>
        /// <param name="awaitableCombine">The asynchronous next step; it receives the 5 values as separate arguments and returns a Result with the sixth value.</param>
        /// <returns>A task that resolves to a Result with the 6 values as a tuple, in order, if the source and the step succeed; otherwise a failure Result with the first error.</returns>
        public static async Task<Result<(T1, T2, T3, T4, T5, T6), E>> OnSuccessCombine<T1, T2, T3, T4, T5, T6, E>(this Task<Result<(T1, T2, T3, T4, T5), E>> awaitableSource, Func<T1, T2, T3, T4, T5, Task<Result<T6, E>>> awaitableCombine)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsFailure)
            {
                return Result<(T1, T2, T3, T4, T5, T6), E>.Failure(source.Error);
            }

            var value = source.Value;
            var next = await awaitableCombine(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5).ConfigureAwait(false);
            if (next.IsFailure)
            {
                return Result<(T1, T2, T3, T4, T5, T6), E>.Failure(next.Error);
            }
            return Result<(T1, T2, T3, T4, T5, T6), E>.Success((value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, next.Value));
        }
    }
}
