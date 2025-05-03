using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
	public static class MaybeOnNoneBindExtensions
	{
		public static Maybe<T> OnNoneBind<T>(this Maybe<T> source, Func<Maybe<T>> map)
		{
			if (source.HasValue)
			{
				return source.Value;
			}
			return map();
		}

		public static async Task<Maybe<T>> OnNoneBind<T>(this Task<Maybe<T>> awaitableSource, Func<Maybe<T>> map)
		{
			var source = await awaitableSource.ConfigureAwait(false);
			if (source.HasValue)
			{
				return source;
			}
			return map();
		}

		public static async Task<Maybe<T>> OnNoneBind<T>(this Maybe<T> source, Func<Task<Maybe<T>>> map)
		{
			if (source.HasValue)
			{
				return source;
			}
			return await map().ConfigureAwait(false);
		}

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
