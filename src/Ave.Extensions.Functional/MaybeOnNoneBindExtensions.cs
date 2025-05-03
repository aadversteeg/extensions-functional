using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
	/// <summary>
	/// Provides extension methods for the Maybe monad that apply a binding function when the Maybe has no value.
	/// </summary>
	public static class MaybeOnNoneBindExtensions
	{
		/// <summary>
		/// If the Maybe has no value, applies a binding function that returns a new Maybe.
		/// </summary>
		/// <typeparam name="T">The type of the value in the Maybe.</typeparam>
		/// <param name="source">The source Maybe instance.</param>
		/// <param name="map">A function that returns a new Maybe to use when the source has no value.</param>
		/// <returns>The original Maybe if it has a value; otherwise, the result of the binding function.</returns>
		public static Maybe<T> OnNoneBind<T>(this Maybe<T> source, Func<Maybe<T>> map)
		{
			if (source.HasValue)
			{
				return source.Value;
			}
			return map();
		}

		/// <summary>
		/// If the awaited Maybe has no value, applies a binding function that returns a new Maybe.
		/// </summary>
		/// <typeparam name="T">The type of the value in the Maybe.</typeparam>
		/// <param name="awaitableSource">A Task that resolves to a Maybe instance.</param>
		/// <param name="map">A function that returns a new Maybe to use when the source has no value.</param>
		/// <returns>A Task that resolves to the original Maybe if it has a value; otherwise, the result of the binding function.</returns>
		public static async Task<Maybe<T>> OnNoneBind<T>(this Task<Maybe<T>> awaitableSource, Func<Maybe<T>> map)
		{
			var source = await awaitableSource.ConfigureAwait(false);
			if (source.HasValue)
			{
				return source;
			}
			return map();
		}

		/// <summary>
		/// If the Maybe has no value, applies an asynchronous binding function that returns a new Maybe.
		/// </summary>
		/// <typeparam name="T">The type of the value in the Maybe.</typeparam>
		/// <param name="source">The source Maybe instance.</param>
		/// <param name="map">An asynchronous function that returns a new Maybe to use when the source has no value.</param>
		/// <returns>A Task that resolves to the original Maybe if it has a value; otherwise, the result of the asynchronous binding function.</returns>
		public static async Task<Maybe<T>> OnNoneBind<T>(this Maybe<T> source, Func<Task<Maybe<T>>> map)
		{
			if (source.HasValue)
			{
				return source;
			}
			return await map().ConfigureAwait(false);
		}

		/// <summary>
		/// If the awaited Maybe has no value, applies an asynchronous binding function that returns a new Maybe.
		/// </summary>
		/// <typeparam name="T">The type of the value in the Maybe.</typeparam>
		/// <param name="awaitableSource">A Task that resolves to a Maybe instance.</param>
		/// <param name="map">An asynchronous function that returns a new Maybe to use when the source has no value.</param>
		/// <returns>A Task that resolves to the original Maybe if it has a value; otherwise, the result of the asynchronous binding function.</returns>
		public static async Task<Maybe<T>> OnNoneBind<T>(this Task<Maybe<T>> awaitableSource, Func<Task<Maybe<T>>> map)
		{
			var source = await awaitableSource.ConfigureAwait(false);
			if (source.HasValue)
			{
				return source;
			}
			return await map().ConfigureAwait(false);
		}
	}
}