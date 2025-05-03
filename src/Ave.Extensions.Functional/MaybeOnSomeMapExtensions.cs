using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
    /// <summary>
    /// Provides extension methods for the Maybe monad that apply a mapping function when the Maybe has a value.
    /// </summary>
    public static class MaybeOnSomeMapExtensions
    {
        /// <summary>
        /// If the Maybe has a value, applies a mapping function to transform the value to a different type.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the source Maybe.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Maybe.</typeparam>
        /// <param name="source">The source Maybe instance.</param>
        /// <param name="map">A function that transforms the value from the source Maybe to a different type.</param>
        /// <returns>A new Maybe containing the transformed value if the source has a value; otherwise, an empty Maybe of the output type.</returns>
        public static Maybe<Tout> OnSomeMap<Tin, Tout>(this Maybe<Tin> source, Func<Tin, Tout> map)
        {
            if (source.HasValue)
            {
                return Maybe<Tout>.From(map(source.Value));
            }
            return Maybe<Tout>.None;
        }

        /// <summary>
        /// If the awaitable Maybe has a value, applies a mapping function to transform the value to a different type.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the source Maybe.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Maybe.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Maybe instance.</param>
        /// <param name="map">A function that transforms the value from the source Maybe to a different type.</param>
        /// <returns>A task that resolves to a new Maybe containing the transformed value if the source has a value; otherwise, an empty Maybe of the output type.</returns>
        public static async Task<Maybe<Tout>> OnSomeMap<Tin, Tout>(this Task<Maybe<Tin>> awaitableSource, Func<Tin, Tout> map)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.HasValue)
            {
                return Maybe<Tout>.From(map(source.Value));
            }
            return Maybe<Tout>.None;
        }

        /// <summary>
        /// If the Maybe has a value, applies an asynchronous mapping function to transform the value to a different type.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the source Maybe.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Maybe.</typeparam>
        /// <param name="source">The source Maybe instance.</param>
        /// <param name="map">An asynchronous function that transforms the value from the source Maybe to a different type.</param>
        /// <returns>A task that resolves to a new Maybe containing the transformed value if the source has a value; otherwise, an empty Maybe of the output type.</returns>
        public static async Task<Maybe<Tout>> OnSomeMap<Tin, Tout>(this Maybe<Tin> source, Func<Tin, Task<Tout>> map)
        {
            if (source.HasValue)
            {
                return Maybe<Tout>.From(await map(source.Value).ConfigureAwait(false));
            }
            return Maybe<Tout>.None;
        }

        /// <summary>
        /// If the awaitable Maybe has a value, applies an asynchronous mapping function to transform the value to a different type.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the source Maybe.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Maybe.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Maybe instance.</param>
        /// <param name="map">An asynchronous function that transforms the value from the source Maybe to a different type.</param>
        /// <returns>A task that resolves to a new Maybe containing the transformed value if the source has a value; otherwise, an empty Maybe of the output type.</returns>
        public static async Task<Maybe<Tout>> OnSomeMap<Tin, Tout>(this Task<Maybe<Tin>> awaitableSource, Func<Tin, Task<Tout>> map)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.HasValue)
            {
                return Maybe<Tout>.From(await map(source.Value).ConfigureAwait(false));
            }
            return Maybe<Tout>.None;
        }
    }
}
