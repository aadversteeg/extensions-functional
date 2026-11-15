using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
    /// <summary>
    /// Provides OnSuccessBind overloads for a Result holding a tuple of 2 to 6 values, passing the values as separate arguments.
    /// </summary>
    public static class ResultOnSuccessBindTupleExtensions
    {
        /// <summary>
        /// If the Result is successful, applies a binding function to its 2 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 2 values.</param>
        /// <param name="bind">A function that takes the 2 values and returns a new Result.</param>
        /// <returns>The result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static Result<Tout, E> OnSuccessBind<T1, T2, Tout, E>(this Result<(T1, T2), E> source, Func<T1, T2, Result<Tout, E>> bind)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return bind(value.Item1, value.Item2);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, applies a binding function to its 2 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 2 values.</param>
        /// <param name="bind">A function that takes the 2 values and returns a new Result.</param>
        /// <returns>A task that resolves to the result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static async Task<Result<Tout, E>> OnSuccessBind<T1, T2, Tout, E>(this Task<Result<(T1, T2), E>> awaitableSource, Func<T1, T2, Result<Tout, E>> bind)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return bind(value.Item1, value.Item2);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, applies an asynchronous binding function to its 2 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 2 values.</param>
        /// <param name="awaitableBind">An asynchronous function that takes the 2 values and returns a new Result.</param>
        /// <returns>A task that resolves to the result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static async Task<Result<Tout, E>> OnSuccessBind<T1, T2, Tout, E>(this Result<(T1, T2), E> source, Func<T1, T2, Task<Result<Tout, E>>> awaitableBind)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return await awaitableBind(value.Item1, value.Item2).ConfigureAwait(false);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, applies an asynchronous binding function to its 2 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 2 values.</param>
        /// <param name="awaitableBind">An asynchronous function that takes the 2 values and returns a new Result.</param>
        /// <returns>A task that resolves to the result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static async Task<Result<Tout, E>> OnSuccessBind<T1, T2, Tout, E>(this Task<Result<(T1, T2), E>> awaitableSource, Func<T1, T2, Task<Result<Tout, E>>> awaitableBind)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return await awaitableBind(value.Item1, value.Item2).ConfigureAwait(false);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, applies a binding function to its 3 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 3 values.</param>
        /// <param name="bind">A function that takes the 3 values and returns a new Result.</param>
        /// <returns>The result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static Result<Tout, E> OnSuccessBind<T1, T2, T3, Tout, E>(this Result<(T1, T2, T3), E> source, Func<T1, T2, T3, Result<Tout, E>> bind)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return bind(value.Item1, value.Item2, value.Item3);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, applies a binding function to its 3 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 3 values.</param>
        /// <param name="bind">A function that takes the 3 values and returns a new Result.</param>
        /// <returns>A task that resolves to the result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static async Task<Result<Tout, E>> OnSuccessBind<T1, T2, T3, Tout, E>(this Task<Result<(T1, T2, T3), E>> awaitableSource, Func<T1, T2, T3, Result<Tout, E>> bind)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return bind(value.Item1, value.Item2, value.Item3);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, applies an asynchronous binding function to its 3 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 3 values.</param>
        /// <param name="awaitableBind">An asynchronous function that takes the 3 values and returns a new Result.</param>
        /// <returns>A task that resolves to the result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static async Task<Result<Tout, E>> OnSuccessBind<T1, T2, T3, Tout, E>(this Result<(T1, T2, T3), E> source, Func<T1, T2, T3, Task<Result<Tout, E>>> awaitableBind)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return await awaitableBind(value.Item1, value.Item2, value.Item3).ConfigureAwait(false);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, applies an asynchronous binding function to its 3 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 3 values.</param>
        /// <param name="awaitableBind">An asynchronous function that takes the 3 values and returns a new Result.</param>
        /// <returns>A task that resolves to the result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static async Task<Result<Tout, E>> OnSuccessBind<T1, T2, T3, Tout, E>(this Task<Result<(T1, T2, T3), E>> awaitableSource, Func<T1, T2, T3, Task<Result<Tout, E>>> awaitableBind)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return await awaitableBind(value.Item1, value.Item2, value.Item3).ConfigureAwait(false);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, applies a binding function to its 4 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 4 values.</param>
        /// <param name="bind">A function that takes the 4 values and returns a new Result.</param>
        /// <returns>The result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static Result<Tout, E> OnSuccessBind<T1, T2, T3, T4, Tout, E>(this Result<(T1, T2, T3, T4), E> source, Func<T1, T2, T3, T4, Result<Tout, E>> bind)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return bind(value.Item1, value.Item2, value.Item3, value.Item4);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, applies a binding function to its 4 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 4 values.</param>
        /// <param name="bind">A function that takes the 4 values and returns a new Result.</param>
        /// <returns>A task that resolves to the result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static async Task<Result<Tout, E>> OnSuccessBind<T1, T2, T3, T4, Tout, E>(this Task<Result<(T1, T2, T3, T4), E>> awaitableSource, Func<T1, T2, T3, T4, Result<Tout, E>> bind)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return bind(value.Item1, value.Item2, value.Item3, value.Item4);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, applies an asynchronous binding function to its 4 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 4 values.</param>
        /// <param name="awaitableBind">An asynchronous function that takes the 4 values and returns a new Result.</param>
        /// <returns>A task that resolves to the result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static async Task<Result<Tout, E>> OnSuccessBind<T1, T2, T3, T4, Tout, E>(this Result<(T1, T2, T3, T4), E> source, Func<T1, T2, T3, T4, Task<Result<Tout, E>>> awaitableBind)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return await awaitableBind(value.Item1, value.Item2, value.Item3, value.Item4).ConfigureAwait(false);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, applies an asynchronous binding function to its 4 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 4 values.</param>
        /// <param name="awaitableBind">An asynchronous function that takes the 4 values and returns a new Result.</param>
        /// <returns>A task that resolves to the result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static async Task<Result<Tout, E>> OnSuccessBind<T1, T2, T3, T4, Tout, E>(this Task<Result<(T1, T2, T3, T4), E>> awaitableSource, Func<T1, T2, T3, T4, Task<Result<Tout, E>>> awaitableBind)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return await awaitableBind(value.Item1, value.Item2, value.Item3, value.Item4).ConfigureAwait(false);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, applies a binding function to its 5 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 5 values.</param>
        /// <param name="bind">A function that takes the 5 values and returns a new Result.</param>
        /// <returns>The result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static Result<Tout, E> OnSuccessBind<T1, T2, T3, T4, T5, Tout, E>(this Result<(T1, T2, T3, T4, T5), E> source, Func<T1, T2, T3, T4, T5, Result<Tout, E>> bind)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return bind(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, applies a binding function to its 5 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 5 values.</param>
        /// <param name="bind">A function that takes the 5 values and returns a new Result.</param>
        /// <returns>A task that resolves to the result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static async Task<Result<Tout, E>> OnSuccessBind<T1, T2, T3, T4, T5, Tout, E>(this Task<Result<(T1, T2, T3, T4, T5), E>> awaitableSource, Func<T1, T2, T3, T4, T5, Result<Tout, E>> bind)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return bind(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, applies an asynchronous binding function to its 5 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 5 values.</param>
        /// <param name="awaitableBind">An asynchronous function that takes the 5 values and returns a new Result.</param>
        /// <returns>A task that resolves to the result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static async Task<Result<Tout, E>> OnSuccessBind<T1, T2, T3, T4, T5, Tout, E>(this Result<(T1, T2, T3, T4, T5), E> source, Func<T1, T2, T3, T4, T5, Task<Result<Tout, E>>> awaitableBind)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return await awaitableBind(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5).ConfigureAwait(false);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, applies an asynchronous binding function to its 5 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 5 values.</param>
        /// <param name="awaitableBind">An asynchronous function that takes the 5 values and returns a new Result.</param>
        /// <returns>A task that resolves to the result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static async Task<Result<Tout, E>> OnSuccessBind<T1, T2, T3, T4, T5, Tout, E>(this Task<Result<(T1, T2, T3, T4, T5), E>> awaitableSource, Func<T1, T2, T3, T4, T5, Task<Result<Tout, E>>> awaitableBind)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return await awaitableBind(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5).ConfigureAwait(false);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, applies a binding function to its 6 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="T6">The type of the sixth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 6 values.</param>
        /// <param name="bind">A function that takes the 6 values and returns a new Result.</param>
        /// <returns>The result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static Result<Tout, E> OnSuccessBind<T1, T2, T3, T4, T5, T6, Tout, E>(this Result<(T1, T2, T3, T4, T5, T6), E> source, Func<T1, T2, T3, T4, T5, T6, Result<Tout, E>> bind)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return bind(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, applies a binding function to its 6 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="T6">The type of the sixth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 6 values.</param>
        /// <param name="bind">A function that takes the 6 values and returns a new Result.</param>
        /// <returns>A task that resolves to the result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static async Task<Result<Tout, E>> OnSuccessBind<T1, T2, T3, T4, T5, T6, Tout, E>(this Task<Result<(T1, T2, T3, T4, T5, T6), E>> awaitableSource, Func<T1, T2, T3, T4, T5, T6, Result<Tout, E>> bind)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return bind(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, applies an asynchronous binding function to its 6 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="T6">The type of the sixth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 6 values.</param>
        /// <param name="awaitableBind">An asynchronous function that takes the 6 values and returns a new Result.</param>
        /// <returns>A task that resolves to the result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static async Task<Result<Tout, E>> OnSuccessBind<T1, T2, T3, T4, T5, T6, Tout, E>(this Result<(T1, T2, T3, T4, T5, T6), E> source, Func<T1, T2, T3, T4, T5, T6, Task<Result<Tout, E>>> awaitableBind)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return await awaitableBind(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6).ConfigureAwait(false);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, applies an asynchronous binding function to its 6 values, passed as separate arguments, that returns a new Result.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="T6">The type of the sixth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 6 values.</param>
        /// <param name="awaitableBind">An asynchronous function that takes the 6 values and returns a new Result.</param>
        /// <returns>A task that resolves to the result of the binding function if the source is successful; otherwise, a failure Result with the original error.</returns>
        public static async Task<Result<Tout, E>> OnSuccessBind<T1, T2, T3, T4, T5, T6, Tout, E>(this Task<Result<(T1, T2, T3, T4, T5, T6), E>> awaitableSource, Func<T1, T2, T3, T4, T5, T6, Task<Result<Tout, E>>> awaitableBind)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return await awaitableBind(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6).ConfigureAwait(false);
            }
            return Result<Tout, E>.Failure(source.Error);
        }
    }
}
