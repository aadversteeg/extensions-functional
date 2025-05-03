using System.Threading.Tasks;
using System;

namespace Ave.Extensions.Functional
{
	public static class MaybeMatchExtensions
	{
		public static TOut Match<TIn, TOut>(this Maybe<TIn> source, Func<TIn, TOut> onSome, Func<TOut> onNone)
		{
			if (source.HasValue)
			{
				return  onSome(source.Value);
			}

			return onNone();
		}

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
