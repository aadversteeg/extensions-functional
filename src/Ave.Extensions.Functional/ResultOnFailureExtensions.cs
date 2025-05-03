using System;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
    public static class ResultOnFailureExtensions
    {   
        public static Result<T, Eout> OnFailure<T, Ein, Eout>(this Result<T,Ein> source, Func<Ein,Eout> mapError)
        {
            if(source.IsFailure)
            {
                return Result<T,Eout>.Failure(mapError(source.Error));
            }
            return Result<T, Eout>.Success(source.Value);
        }

		public static async Task<Result<T, Eout>> OnFailure<T, Ein, Eout>(this Task<Result<T, Ein>> awaitableSource, Func<Ein, Eout> mapError)
		{
            var source = await awaitableSource.ConfigureAwait(false);
			if (source.IsFailure)
			{
				return Result<T, Eout>.Failure(mapError(source.Error));
			}
			return Result<T, Eout>.Success(source.Value);
		}

		public static async Task<Result<T, Eout>> OnFailure<T, Ein, Eout>(this Result<T, Ein> source, Func<Ein, Task<Eout>> mapError)
		{
			if (source.IsFailure)
			{
				return Result<T, Eout>.Failure(await mapError(source.Error).ConfigureAwait(false));
			}
			return Result<T, Eout>.Success(source.Value);
		}

		public static async Task<Result<T, Eout>> OnFailure<T, Ein, Eout>(this Task<Result<T, Ein>> awaitableSource, Func<Ein, Task<Eout>> mapError)
		{
			var source = await awaitableSource.ConfigureAwait(false);
			if (source.IsFailure)
			{
				return Result<T, Eout>.Failure(await mapError(source.Error).ConfigureAwait(false));
			}
			return Result<T, Eout>.Success(source.Value);
		}
	}
}
