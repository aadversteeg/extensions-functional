using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
	public static class ResultOnSuccessBindExtensions
	{
        public static Result<Tout, E> OnSuccessBind<Tin, Tout, E>(this Result<Tin, E> source, Func<Tin, Result<Tout,E>> bind)
        {
            if (source.IsSuccess)
            {
                return bind(source.Value);
            }
            return Result<Tout, E>.Failure(source.Error);
        }

		public static async	Task<Result<Tout, E>> OnSuccessBind<Tin, Tout, E>(this Task<Result<Tin, E>> awaitableSource, Func<Tin, Result<Tout, E>> bind)
		{
			var source = await awaitableSource.ConfigureAwait(false);
			if (source.IsSuccess)
			{
				return bind(source.Value);
			}
			return Result<Tout, E>.Failure(source.Error);
		}

		public static async Task<Result<Tout, E>> OnSuccessBind<Tin, Tout, E>(this Result<Tin, E> source, Func<Tin, Task<Result<Tout, E>>> awaitableBind)
		{
			if (source.IsSuccess)
			{
				return await awaitableBind(source.Value).ConfigureAwait(false);
			}
			return Result<Tout, E>.Failure(source.Error);
		}

		public static async Task<Result<Tout, E>> OnSuccessBind<Tin, Tout, E>(this Task<Result<Tin, E>> awaitableSource, Func<Tin, Task<Result<Tout, E>>> awaitableBind)
		{
			var source = await awaitableSource.ConfigureAwait(false);
			if (source.IsSuccess)
			{
				return await awaitableBind(source.Value).ConfigureAwait(false);
			}
			return Result<Tout, E>.Failure(source.Error);
		}
	}
}
