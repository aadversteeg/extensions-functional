using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
    /// <summary>
    /// Provides OnSuccessMap overloads for a Result holding a tuple of 2 to 6 values, passing the values as separate arguments.
    /// </summary>
    public static class ResultOnSuccessMapTupleExtensions
    {
        /// <summary>
        /// If the Result is successful, transforms its 2 values, passed as separate arguments, using the provided mapping function.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 2 values.</param>
        /// <param name="map">A function that transforms the 2 values to a different type.</param>
        /// <returns>A new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static Result<Tout, E> OnSuccessMap<T1, T2, Tout, E>(this Result<(T1, T2), E> source, Func<T1, T2, Tout> map)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(map(value.Item1, value.Item2));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, transforms its 2 values, passed as separate arguments, using the provided mapping function.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 2 values.</param>
        /// <param name="map">A function that transforms the 2 values to a different type.</param>
        /// <returns>A task that resolves to a new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static async Task<Result<Tout, E>> OnSuccessMap<T1, T2, Tout, E>(this Task<Result<(T1, T2), E>> awaitableSource, Func<T1, T2, Tout> map)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(map(value.Item1, value.Item2));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, asynchronously transforms its 2 values, passed as separate arguments, using the provided mapping function.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 2 values.</param>
        /// <param name="map">An asynchronous function that transforms the 2 values to a different type.</param>
        /// <returns>A task that resolves to a new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static async Task<Result<Tout, E>> OnSuccessMap<T1, T2, Tout, E>(this Result<(T1, T2), E> source, Func<T1, T2, Task<Tout>> map)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(await map(value.Item1, value.Item2).ConfigureAwait(false));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, asynchronously transforms its 2 values, passed as separate arguments, using the provided mapping function.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 2 values.</param>
        /// <param name="map">An asynchronous function that transforms the 2 values to a different type.</param>
        /// <returns>A task that resolves to a new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static async Task<Result<Tout, E>> OnSuccessMap<T1, T2, Tout, E>(this Task<Result<(T1, T2), E>> awaitableSource, Func<T1, T2, Task<Tout>> map)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(await map(value.Item1, value.Item2).ConfigureAwait(false));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, transforms its 3 values, passed as separate arguments, using the provided mapping function.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 3 values.</param>
        /// <param name="map">A function that transforms the 3 values to a different type.</param>
        /// <returns>A new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static Result<Tout, E> OnSuccessMap<T1, T2, T3, Tout, E>(this Result<(T1, T2, T3), E> source, Func<T1, T2, T3, Tout> map)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(map(value.Item1, value.Item2, value.Item3));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, transforms its 3 values, passed as separate arguments, using the provided mapping function.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 3 values.</param>
        /// <param name="map">A function that transforms the 3 values to a different type.</param>
        /// <returns>A task that resolves to a new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static async Task<Result<Tout, E>> OnSuccessMap<T1, T2, T3, Tout, E>(this Task<Result<(T1, T2, T3), E>> awaitableSource, Func<T1, T2, T3, Tout> map)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(map(value.Item1, value.Item2, value.Item3));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, asynchronously transforms its 3 values, passed as separate arguments, using the provided mapping function.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 3 values.</param>
        /// <param name="map">An asynchronous function that transforms the 3 values to a different type.</param>
        /// <returns>A task that resolves to a new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static async Task<Result<Tout, E>> OnSuccessMap<T1, T2, T3, Tout, E>(this Result<(T1, T2, T3), E> source, Func<T1, T2, T3, Task<Tout>> map)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(await map(value.Item1, value.Item2, value.Item3).ConfigureAwait(false));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, asynchronously transforms its 3 values, passed as separate arguments, using the provided mapping function.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 3 values.</param>
        /// <param name="map">An asynchronous function that transforms the 3 values to a different type.</param>
        /// <returns>A task that resolves to a new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static async Task<Result<Tout, E>> OnSuccessMap<T1, T2, T3, Tout, E>(this Task<Result<(T1, T2, T3), E>> awaitableSource, Func<T1, T2, T3, Task<Tout>> map)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(await map(value.Item1, value.Item2, value.Item3).ConfigureAwait(false));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, transforms its 4 values, passed as separate arguments, using the provided mapping function.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 4 values.</param>
        /// <param name="map">A function that transforms the 4 values to a different type.</param>
        /// <returns>A new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static Result<Tout, E> OnSuccessMap<T1, T2, T3, T4, Tout, E>(this Result<(T1, T2, T3, T4), E> source, Func<T1, T2, T3, T4, Tout> map)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(map(value.Item1, value.Item2, value.Item3, value.Item4));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, transforms its 4 values, passed as separate arguments, using the provided mapping function.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 4 values.</param>
        /// <param name="map">A function that transforms the 4 values to a different type.</param>
        /// <returns>A task that resolves to a new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static async Task<Result<Tout, E>> OnSuccessMap<T1, T2, T3, T4, Tout, E>(this Task<Result<(T1, T2, T3, T4), E>> awaitableSource, Func<T1, T2, T3, T4, Tout> map)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(map(value.Item1, value.Item2, value.Item3, value.Item4));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, asynchronously transforms its 4 values, passed as separate arguments, using the provided mapping function.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 4 values.</param>
        /// <param name="map">An asynchronous function that transforms the 4 values to a different type.</param>
        /// <returns>A task that resolves to a new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static async Task<Result<Tout, E>> OnSuccessMap<T1, T2, T3, T4, Tout, E>(this Result<(T1, T2, T3, T4), E> source, Func<T1, T2, T3, T4, Task<Tout>> map)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(await map(value.Item1, value.Item2, value.Item3, value.Item4).ConfigureAwait(false));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, asynchronously transforms its 4 values, passed as separate arguments, using the provided mapping function.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 4 values.</param>
        /// <param name="map">An asynchronous function that transforms the 4 values to a different type.</param>
        /// <returns>A task that resolves to a new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static async Task<Result<Tout, E>> OnSuccessMap<T1, T2, T3, T4, Tout, E>(this Task<Result<(T1, T2, T3, T4), E>> awaitableSource, Func<T1, T2, T3, T4, Task<Tout>> map)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(await map(value.Item1, value.Item2, value.Item3, value.Item4).ConfigureAwait(false));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, transforms its 5 values, passed as separate arguments, using the provided mapping function.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 5 values.</param>
        /// <param name="map">A function that transforms the 5 values to a different type.</param>
        /// <returns>A new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static Result<Tout, E> OnSuccessMap<T1, T2, T3, T4, T5, Tout, E>(this Result<(T1, T2, T3, T4, T5), E> source, Func<T1, T2, T3, T4, T5, Tout> map)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(map(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, transforms its 5 values, passed as separate arguments, using the provided mapping function.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 5 values.</param>
        /// <param name="map">A function that transforms the 5 values to a different type.</param>
        /// <returns>A task that resolves to a new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static async Task<Result<Tout, E>> OnSuccessMap<T1, T2, T3, T4, T5, Tout, E>(this Task<Result<(T1, T2, T3, T4, T5), E>> awaitableSource, Func<T1, T2, T3, T4, T5, Tout> map)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(map(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, asynchronously transforms its 5 values, passed as separate arguments, using the provided mapping function.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 5 values.</param>
        /// <param name="map">An asynchronous function that transforms the 5 values to a different type.</param>
        /// <returns>A task that resolves to a new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static async Task<Result<Tout, E>> OnSuccessMap<T1, T2, T3, T4, T5, Tout, E>(this Result<(T1, T2, T3, T4, T5), E> source, Func<T1, T2, T3, T4, T5, Task<Tout>> map)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(await map(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5).ConfigureAwait(false));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, asynchronously transforms its 5 values, passed as separate arguments, using the provided mapping function.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 5 values.</param>
        /// <param name="map">An asynchronous function that transforms the 5 values to a different type.</param>
        /// <returns>A task that resolves to a new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static async Task<Result<Tout, E>> OnSuccessMap<T1, T2, T3, T4, T5, Tout, E>(this Task<Result<(T1, T2, T3, T4, T5), E>> awaitableSource, Func<T1, T2, T3, T4, T5, Task<Tout>> map)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(await map(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5).ConfigureAwait(false));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, transforms its 6 values, passed as separate arguments, using the provided mapping function.
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
        /// <param name="map">A function that transforms the 6 values to a different type.</param>
        /// <returns>A new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static Result<Tout, E> OnSuccessMap<T1, T2, T3, T4, T5, T6, Tout, E>(this Result<(T1, T2, T3, T4, T5, T6), E> source, Func<T1, T2, T3, T4, T5, T6, Tout> map)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(map(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, transforms its 6 values, passed as separate arguments, using the provided mapping function.
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
        /// <param name="map">A function that transforms the 6 values to a different type.</param>
        /// <returns>A task that resolves to a new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static async Task<Result<Tout, E>> OnSuccessMap<T1, T2, T3, T4, T5, T6, Tout, E>(this Task<Result<(T1, T2, T3, T4, T5, T6), E>> awaitableSource, Func<T1, T2, T3, T4, T5, T6, Tout> map)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(map(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the Result is successful, asynchronously transforms its 6 values, passed as separate arguments, using the provided mapping function.
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
        /// <param name="map">An asynchronous function that transforms the 6 values to a different type.</param>
        /// <returns>A task that resolves to a new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static async Task<Result<Tout, E>> OnSuccessMap<T1, T2, T3, T4, T5, T6, Tout, E>(this Result<(T1, T2, T3, T4, T5, T6), E> source, Func<T1, T2, T3, T4, T5, T6, Task<Tout>> map)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(await map(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6).ConfigureAwait(false));
            }
            return Result<Tout, E>.Failure(source.Error);
        }

        /// <summary>
        /// If the awaitable Result is successful, asynchronously transforms its 6 values, passed as separate arguments, using the provided mapping function.
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
        /// <param name="map">An asynchronous function that transforms the 6 values to a different type.</param>
        /// <returns>A task that resolves to a new Result with the transformed value if successful, or with the original error if a failure.</returns>
        public static async Task<Result<Tout, E>> OnSuccessMap<T1, T2, T3, T4, T5, T6, Tout, E>(this Task<Result<(T1, T2, T3, T4, T5, T6), E>> awaitableSource, Func<T1, T2, T3, T4, T5, T6, Task<Tout>> map)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return Result<Tout, E>.Success(await map(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6).ConfigureAwait(false));
            }
            return Result<Tout, E>.Failure(source.Error);
        }
    }
}
