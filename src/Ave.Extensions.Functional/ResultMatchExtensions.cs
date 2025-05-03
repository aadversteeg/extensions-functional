using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
	public static class ResultMatchExtensions
	{
		public static TOut Match<TIn, TOut, E>(this Result<TIn, E> source, Func<TIn, TOut> onSuccess, Func<E, TOut> onError)
		{
			if(source.IsSuccess) {
				return onSuccess(source.Value);
			}

			return onError(source.Error);
		}

		public static async Task<TOut> Match<TIn, TOut, E>(this Task<Result<TIn, E>>  awaitableSource, Func<TIn, TOut> onSuccess, Func<E, TOut> onError)
		{
			var source = await awaitableSource.ConfigureAwait(false);
			if (source.IsSuccess)
			{
				return onSuccess(source.Value);
			}

			return onError(source.Error);
		}
	}
}
