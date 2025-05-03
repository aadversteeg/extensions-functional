using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
	public static class ResultOnSuccessMapExtensions
	{
		public static Result<Tout,E> OnSuccessMap<Tin,Tout, E>(this Result<Tin,E> source, Func<Tin,Tout> map)
		{
			if(source.IsSuccess)
			{
				return Result<Tout, E>.Success(map(source.Value));
			}
			return Result<Tout, E>.Failure(source.Error);
		}

		public static async Task<Result<Tout, E>> OnSuccessMap<Tin, Tout, E>(this Task<Result<Tin, E>> awaitableSource, Func<Tin, Tout> map)
		{
			var source = await awaitableSource.ConfigureAwait(false);

			if (source.IsSuccess)
			{
				return Result<Tout, E>.Success(map(source.Value));
			}
			return Result<Tout, E>.Failure(source.Error);
		}

		public static async Task<Result<Tout, E>> OnSuccessMap<Tin, Tout, E>(this Result<Tin, E> source, Func<Tin, Task<Tout>> map)
		{
			if (source.IsSuccess)
			{
				return Result<Tout, E>.Success(await map(source.Value).ConfigureAwait(false));
			}
			return Result<Tout, E>.Failure(source.Error);
		}

		public static async Task<Result<Tout, E>> OnSuccessMap<Tin, Tout, E>(this Task<Result<Tin, E>> awaitableSource, Func<Tin, Task<Tout>> map)
		{
			var source = await awaitableSource.ConfigureAwait(false);

			if (source.IsSuccess)
			{
				return Result<Tout, E>.Success(await map(source.Value).ConfigureAwait(false));
			}
			return Result<Tout, E>.Failure(source.Error);
		}
	}
}
