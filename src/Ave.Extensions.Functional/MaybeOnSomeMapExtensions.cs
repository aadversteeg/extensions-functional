using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
	public static class MaybeOnSomeMapExtensions
	{
		public static Maybe<Tout> OnSomeMap<Tin, Tout>(this Maybe<Tin> source, Func<Tin, Tout> map)
		{
			if (source.HasValue)
			{
				return Maybe<Tout>.From(map(source.Value));
			}
			return Maybe<Tout>.None;
		}

		public static async Task<Maybe<Tout>> OnSomeMap<Tin, Tout>(this Task<Maybe<Tin>> awaitableSource, Func<Tin, Tout> map)
		{
			var source = await awaitableSource.ConfigureAwait(false);
			if (source.HasValue)
			{
				return Maybe<Tout>.From(map(source.Value));
			}
			return Maybe<Tout>.None;
		}

		public static async Task<Maybe<Tout>> OnSomeMap<Tin, Tout>(this Maybe<Tin> source, Func<Tin, Task<Tout>> map)
		{
			if (source.HasValue)
			{
				return Maybe<Tout>.From( await map(source.Value).ConfigureAwait(false));
			}
			return Maybe<Tout>.None;
		}

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
