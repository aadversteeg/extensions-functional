using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
    /// <summary>
    /// Provides extension methods for the Maybe monad that apply a binding function when the Maybe has a value.
    /// </summary>
    public static class MaybeOnSomeBindExtensions
    {
        /// <summary>
        /// If the Maybe has a value, applies a binding function that returns a new Maybe of a different type.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the source Maybe.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Maybe.</typeparam>
        /// <param name="source">The source Maybe instance.</param>
        /// <param name="map">A function that takes the value from the source Maybe and returns a new Maybe of a different type.</param>
        /// <returns>The result of the binding function if the source has a value; otherwise, an empty Maybe of the output type.</returns>
        public static Maybe<Tout> OnSomeBind<Tin, Tout>(this Maybe<Tin> source, Func<Tin, Maybe<Tout>> map)
        {
            if (source.HasValue)
            {
                return map(source.Value);
            }
            return Maybe<Tout>.None;
        }

        /// <summary>
        /// If the awaitable Maybe has a value, applies a binding function that returns a new Maybe of a different type.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the source Maybe.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Maybe.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Maybe instance.</param>
        /// <param name="map">A function that takes the value from the source Maybe and returns a new Maybe of a different type.</param>
        /// <returns>A task that resolves to the result of the binding function if the source has a value; otherwise, an empty Maybe of the output type.</returns>
        public static async Task<Maybe<Tout>> OnSomeBind<Tin, Tout>(this Task<Maybe<Tin>> awaitableSource, Func<Tin, Maybe<Tout>> map)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.HasValue)
            {
                return map(source.Value);
            }
            return Maybe<Tout>.None;
        }

        /// <summary>
        /// If the Maybe has a value, applies an asynchronous binding function that returns a new Maybe of a different type.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the source Maybe.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Maybe.</typeparam>
        /// <param name="source">The source Maybe instance.</param>
        /// <param name="map">An asynchronous function that takes the value from the source Maybe and returns a new Maybe of a different type.</param>
        /// <returns>A task that resolves to the result of the binding function if the source has a value; otherwise, an empty Maybe of the output type.</returns>
        public static async Task<Maybe<Tout>> OnSomeBind<Tin, Tout>(this Maybe<Tin> source, Func<Tin, Task<Maybe<Tout>>> map)
        {
            if (source.HasValue)
            {
                return await map(source.Value).ConfigureAwait(false);
            }
            return Maybe<Tout>.None;
        }

        /// <summary>
        /// If the awaitable Maybe has a value, applies an asynchronous binding function that returns a new Maybe of a different type.
        /// </summary>
        /// <typeparam name="Tin">The type of the value in the source Maybe.</typeparam>
        /// <typeparam name="Tout">The type of the value in the resulting Maybe.</typeparam>
        /// <param name="awaitableSource">A task that resolves to a Maybe instance.</param>
        /// <param name="map">An asynchronous function that takes the value from the source Maybe and returns a new Maybe of a different type.</param>
        /// <returns>A task that resolves to the result of the binding function if the source has a value; otherwise, an empty Maybe of the output type.</returns>
        public static async Task<Maybe<Tout>> OnSomeBind<Tin, Tout>(this Task<Maybe<Tin>> awaitableSource, Func<Tin, Task<Maybe<Tout>>> map)
        {
            var source = await awaitableSource.ConfigureAwait(false);
            if (source.HasValue)
            {
                return await map(source.Value).ConfigureAwait(false);
            }
            return Maybe<Tout>.None;
        }
    }
}
