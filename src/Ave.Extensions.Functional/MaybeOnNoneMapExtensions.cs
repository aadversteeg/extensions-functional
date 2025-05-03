using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
    /// <summary>
    /// Provides extension methods for the Maybe monad that apply a mapping function when the Maybe has no value.
    /// </summary>
    public static class MaybeOnNoneMapExtensions
    {
        /// <summary>
        /// If the Maybe has no value, applies a mapping function to create a new value.
        /// </summary>
        /// <typeparam name="T">The type of the value in the Maybe.</typeparam>
        /// <param name="source">The source Maybe instance.</param>
        /// <param name="map">A function that returns a new value to wrap in a Maybe when the source has no value.</param>
        /// <returns>The original Maybe if it has a value; otherwise, a new Maybe containing the result of the mapping function.</returns>
        public static Maybe<T> OnNoneMap<T>(this Maybe<T> source, Func<T> map)
        {
            if (source.HasValue)
            { 
                return source;
            }
            return Maybe<T>.From(map());
        }

        /// <summary>
        /// If the awaitable Maybe has no value, applies a mapping function to create a new value.
        /// </summary>
        /// <typeparam name="T">The type of the value in the Maybe.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Maybe instance.</param>
        /// <param name="map">A function that returns a new value to wrap in a Maybe when the source has no value.</param>
        /// <returns>A task that resolves to the original Maybe if it has a value; otherwise, a new Maybe containing the result of the mapping function.</returns>
        public static async Task<Maybe<T>> OnNoneMap<T>(this Task<Maybe<T>> awaitableSource, Func<T> map)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.HasValue)
            {
                return source;
            }
            return Maybe<T>.From(map());
        }

        /// <summary>
        /// If the Maybe has no value, applies an asynchronous mapping function to create a new value.
        /// </summary>
        /// <typeparam name="T">The type of the value in the Maybe.</typeparam>
        /// <param name="source">The source Maybe instance.</param>
        /// <param name="map">An asynchronous function that returns a new value to wrap in a Maybe when the source has no value.</param>
        /// <returns>A task that resolves to the original Maybe if it has a value; otherwise, a new Maybe containing the result of the mapping function.</returns>
        public static async Task<Maybe<T>> OnNoneMap<T>(this Maybe<T> source, Func<Task<T>> map)
        {
            if (source.HasValue)
            {
                return source.Value;
            }
            return Maybe<T>.From(await map().ConfigureAwait(false)); 
        }

        /// <summary>
        /// If the awaitable Maybe has no value, applies an asynchronous mapping function to create a new value.
        /// </summary>
        /// <typeparam name="T">The type of the value in the Maybe.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Maybe instance.</param>
        /// <param name="map">An asynchronous function that returns a new value to wrap in a Maybe when the source has no value.</param>
        /// <returns>A task that resolves to the original Maybe if it has a value; otherwise, a new Maybe containing the result of the mapping function.</returns>
        public static async Task<Maybe<T>> OnNoneMap<T>(this Task<Maybe<T>> awaitableSource, Func<Task<T>> map)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.HasValue)
            {
                return source;
            }
            return Maybe<T>.From(await map().ConfigureAwait(false));
        }
    }
}
