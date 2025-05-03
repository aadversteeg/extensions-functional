using System.Threading.Tasks;
using System;

namespace Ave.Extensions.Functional
{
	/// <summary>
	/// Provides extension methods for pattern matching on Maybe&lt;T&gt; instances.
	/// </summary>
	public static class MaybeMatchExtensions
	{
		/// <summary>
		/// Matches a Maybe instance to one of two functions depending on whether it has a value.
		/// </summary>
		/// <typeparam name="TIn">The type of the value in the Maybe.</typeparam>
		/// <typeparam name="TOut">The return type of the functions.</typeparam>
		/// <param name="source">The Maybe instance to match against.</param>
		/// <param name="onSome">The function to call when the Maybe has a value.</param>
		/// <param name="onNone">The function to call when the Maybe has no value.</param>
		/// <returns>The result of calling either onSome with the value or onNone.</returns>
		public static TOut Match<TIn, TOut>(this Maybe<TIn> source, Func<TIn, TOut> onSome, Func<TOut> onNone)
		{
			if (source.HasValue)
			{
				return  onSome(source.Value);
			}

			return onNone();
		}

		/// <summary>
		/// Asynchronously matches a Maybe instance to one of two functions depending on whether it has a value.
		/// </summary>
		/// <typeparam name="TIn">The type of the value in the Maybe.</typeparam>
		/// <typeparam name="TOut">The return type of the functions.</typeparam>
		/// <param name="awaitableSource">The Task that resolves to a Maybe instance.</param>
		/// <param name="onSome">The function to call when the Maybe has a value.</param>
		/// <param name="onNone">The function to call when the Maybe has no value.</param>
		/// <returns>A Task that resolves to the result of calling either onSome with the value or onNone.</returns>
		public static async Task<TOut> Match<TIn, TOut>(this Task<Maybe<TIn>> awaitableSource, Func<TIn, TOut> onSome, Func<TOut> onNone)
		{
			var source = await awaitableSource.ConfigureAwait(false);
			if (source.HasValue)
			{
				return onSome(source.Value);
			}

			return onNone();
		}
	}
}