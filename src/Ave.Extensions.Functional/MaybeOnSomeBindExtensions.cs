using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
	public static class MaybeOnSomeBindExtensions
	{
		public static Maybe<Tout> OnSomeBind<Tin, Tout>(this Maybe<Tin> source, Func<Tin, Maybe<Tout>> map)
		{
			if (source.HasValue)
			{
				return map(source.Value);
			}
			return Maybe<Tout>.None;
		}

		public static async Task<Maybe<Tout>> OnSomeBind<Tin, Tout>(this Task<Maybe<Tin>> awaitableSource, Func<Tin, Maybe<Tout>> map)
		{
			var source = await awaitableSource.ConfigureAwait(false);
			if (source.HasValue)
			{
				return map(source.Value);
			}
			return Maybe<Tout>.None;
		}

		public static async Task<Maybe<Tout>> OnSomeBind<Tin, Tout>(this Maybe<Tin> source, Func<Tin, Task<Maybe<Tout>>> map)
		{
			if (source.HasValue)
			{
				return await map(source.Value).ConfigureAwait(false);
			}
			return Maybe<Tout>.None;
		}

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
