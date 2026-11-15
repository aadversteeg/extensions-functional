using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
    /// <summary>
    /// Provides Match overloads for a Result holding a tuple of 2 to 6 values, passing the values as separate arguments.
    /// </summary>
    public static class ResultMatchTupleExtensions
    {
        /// <summary>
        /// Applies pattern matching to a Result holding a tuple of 2 values: executes onSuccess with the values as separate arguments, or onError with the error.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="TOut">The type of the value returned by both match functions.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 2 values.</param>
        /// <param name="onSuccess">The function to execute if the Result is successful, taking the 2 values as input.</param>
        /// <param name="onError">The function to execute if the Result is an error, taking the error value as input.</param>
        /// <returns>The result of either the onSuccess or onError function, depending on the state of the Result.</returns>
        public static TOut Match<T1, T2, TOut, E>(this Result<(T1, T2), E> source, Func<T1, T2, TOut> onSuccess, Func<E, TOut> onError)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return onSuccess(value.Item1, value.Item2);
            }

            return onError(source.Error);
        }

        /// <summary>
        /// Applies pattern matching to an awaitable Result holding a tuple of 2 values: executes onSuccess with the values as separate arguments, or onError with the error.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="TOut">The type of the value returned by both match functions.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 2 values.</param>
        /// <param name="onSuccess">The function to execute if the Result is successful, taking the 2 values as input.</param>
        /// <param name="onError">The function to execute if the Result is an error, taking the error value as input.</param>
        /// <returns>A task that resolves to the result of either the onSuccess or onError function, depending on the state of the Result.</returns>
        public static async Task<TOut> Match<T1, T2, TOut, E>(this Task<Result<(T1, T2), E>> awaitableSource, Func<T1, T2, TOut> onSuccess, Func<E, TOut> onError)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return onSuccess(value.Item1, value.Item2);
            }

            return onError(source.Error);
        }

        /// <summary>
        /// Applies pattern matching to a Result holding a tuple of 3 values: executes onSuccess with the values as separate arguments, or onError with the error.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="TOut">The type of the value returned by both match functions.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 3 values.</param>
        /// <param name="onSuccess">The function to execute if the Result is successful, taking the 3 values as input.</param>
        /// <param name="onError">The function to execute if the Result is an error, taking the error value as input.</param>
        /// <returns>The result of either the onSuccess or onError function, depending on the state of the Result.</returns>
        public static TOut Match<T1, T2, T3, TOut, E>(this Result<(T1, T2, T3), E> source, Func<T1, T2, T3, TOut> onSuccess, Func<E, TOut> onError)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return onSuccess(value.Item1, value.Item2, value.Item3);
            }

            return onError(source.Error);
        }

        /// <summary>
        /// Applies pattern matching to an awaitable Result holding a tuple of 3 values: executes onSuccess with the values as separate arguments, or onError with the error.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="TOut">The type of the value returned by both match functions.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 3 values.</param>
        /// <param name="onSuccess">The function to execute if the Result is successful, taking the 3 values as input.</param>
        /// <param name="onError">The function to execute if the Result is an error, taking the error value as input.</param>
        /// <returns>A task that resolves to the result of either the onSuccess or onError function, depending on the state of the Result.</returns>
        public static async Task<TOut> Match<T1, T2, T3, TOut, E>(this Task<Result<(T1, T2, T3), E>> awaitableSource, Func<T1, T2, T3, TOut> onSuccess, Func<E, TOut> onError)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return onSuccess(value.Item1, value.Item2, value.Item3);
            }

            return onError(source.Error);
        }

        /// <summary>
        /// Applies pattern matching to a Result holding a tuple of 4 values: executes onSuccess with the values as separate arguments, or onError with the error.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="TOut">The type of the value returned by both match functions.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 4 values.</param>
        /// <param name="onSuccess">The function to execute if the Result is successful, taking the 4 values as input.</param>
        /// <param name="onError">The function to execute if the Result is an error, taking the error value as input.</param>
        /// <returns>The result of either the onSuccess or onError function, depending on the state of the Result.</returns>
        public static TOut Match<T1, T2, T3, T4, TOut, E>(this Result<(T1, T2, T3, T4), E> source, Func<T1, T2, T3, T4, TOut> onSuccess, Func<E, TOut> onError)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return onSuccess(value.Item1, value.Item2, value.Item3, value.Item4);
            }

            return onError(source.Error);
        }

        /// <summary>
        /// Applies pattern matching to an awaitable Result holding a tuple of 4 values: executes onSuccess with the values as separate arguments, or onError with the error.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="TOut">The type of the value returned by both match functions.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 4 values.</param>
        /// <param name="onSuccess">The function to execute if the Result is successful, taking the 4 values as input.</param>
        /// <param name="onError">The function to execute if the Result is an error, taking the error value as input.</param>
        /// <returns>A task that resolves to the result of either the onSuccess or onError function, depending on the state of the Result.</returns>
        public static async Task<TOut> Match<T1, T2, T3, T4, TOut, E>(this Task<Result<(T1, T2, T3, T4), E>> awaitableSource, Func<T1, T2, T3, T4, TOut> onSuccess, Func<E, TOut> onError)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return onSuccess(value.Item1, value.Item2, value.Item3, value.Item4);
            }

            return onError(source.Error);
        }

        /// <summary>
        /// Applies pattern matching to a Result holding a tuple of 5 values: executes onSuccess with the values as separate arguments, or onError with the error.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="TOut">The type of the value returned by both match functions.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 5 values.</param>
        /// <param name="onSuccess">The function to execute if the Result is successful, taking the 5 values as input.</param>
        /// <param name="onError">The function to execute if the Result is an error, taking the error value as input.</param>
        /// <returns>The result of either the onSuccess or onError function, depending on the state of the Result.</returns>
        public static TOut Match<T1, T2, T3, T4, T5, TOut, E>(this Result<(T1, T2, T3, T4, T5), E> source, Func<T1, T2, T3, T4, T5, TOut> onSuccess, Func<E, TOut> onError)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return onSuccess(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
            }

            return onError(source.Error);
        }

        /// <summary>
        /// Applies pattern matching to an awaitable Result holding a tuple of 5 values: executes onSuccess with the values as separate arguments, or onError with the error.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="TOut">The type of the value returned by both match functions.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 5 values.</param>
        /// <param name="onSuccess">The function to execute if the Result is successful, taking the 5 values as input.</param>
        /// <param name="onError">The function to execute if the Result is an error, taking the error value as input.</param>
        /// <returns>A task that resolves to the result of either the onSuccess or onError function, depending on the state of the Result.</returns>
        public static async Task<TOut> Match<T1, T2, T3, T4, T5, TOut, E>(this Task<Result<(T1, T2, T3, T4, T5), E>> awaitableSource, Func<T1, T2, T3, T4, T5, TOut> onSuccess, Func<E, TOut> onError)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return onSuccess(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
            }

            return onError(source.Error);
        }

        /// <summary>
        /// Applies pattern matching to a Result holding a tuple of 6 values: executes onSuccess with the values as separate arguments, or onError with the error.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="T6">The type of the sixth value.</typeparam>
        /// <typeparam name="TOut">The type of the value returned by both match functions.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The source Result holding a tuple of 6 values.</param>
        /// <param name="onSuccess">The function to execute if the Result is successful, taking the 6 values as input.</param>
        /// <param name="onError">The function to execute if the Result is an error, taking the error value as input.</param>
        /// <returns>The result of either the onSuccess or onError function, depending on the state of the Result.</returns>
        public static TOut Match<T1, T2, T3, T4, T5, T6, TOut, E>(this Result<(T1, T2, T3, T4, T5, T6), E> source, Func<T1, T2, T3, T4, T5, T6, TOut> onSuccess, Func<E, TOut> onError)
        {
            if (source.IsSuccess)
            {
                var value = source.Value;
                return onSuccess(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
            }

            return onError(source.Error);
        }

        /// <summary>
        /// Applies pattern matching to an awaitable Result holding a tuple of 6 values: executes onSuccess with the values as separate arguments, or onError with the error.
        /// </summary>
        /// <typeparam name="T1">The type of the first value.</typeparam>
        /// <typeparam name="T2">The type of the second value.</typeparam>
        /// <typeparam name="T3">The type of the third value.</typeparam>
        /// <typeparam name="T4">The type of the fourth value.</typeparam>
        /// <typeparam name="T5">The type of the fifth value.</typeparam>
        /// <typeparam name="T6">The type of the sixth value.</typeparam>
        /// <typeparam name="TOut">The type of the value returned by both match functions.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Result holding a tuple of 6 values.</param>
        /// <param name="onSuccess">The function to execute if the Result is successful, taking the 6 values as input.</param>
        /// <param name="onError">The function to execute if the Result is an error, taking the error value as input.</param>
        /// <returns>A task that resolves to the result of either the onSuccess or onError function, depending on the state of the Result.</returns>
        public static async Task<TOut> Match<T1, T2, T3, T4, T5, T6, TOut, E>(this Task<Result<(T1, T2, T3, T4, T5, T6), E>> awaitableSource, Func<T1, T2, T3, T4, T5, T6, TOut> onSuccess, Func<E, TOut> onError)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.IsSuccess)
            {
                var value = source.Value;
                return onSuccess(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
            }

            return onError(source.Error);
        }
    }
}
