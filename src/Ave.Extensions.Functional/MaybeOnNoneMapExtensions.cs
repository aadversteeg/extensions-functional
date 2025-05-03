using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
	public static class MaybeOnNoneMapExtensions
	{
		public static Maybe<T> OnNoneMap<T>(this Maybe<T> source, Func<T> map)
		{
			if (source.HasValue)
			{ 
				return source;
			}
			return Maybe<T>.From(map());
		}

		public static async Task<Maybe<T>> OnNoneMap<T>(this Task<Maybe<T>> awaitableSource, Func<T> map)
		{
			var source = await awaitableSource.ConfigureAwait(false);
			if (source.HasValue)
			{
				return source;
			}
			return Maybe<T>.From(map());
	    }

		public static async Task<Maybe<T>> OnNoneMap<T>(this Maybe<T> source, Func<Task<T>> map)
		{
			if (source.HasValue)
			{
				return source.Value;
			}
			return Maybe<T>.From(await map().ConfigureAwait(false)); 
		}

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
