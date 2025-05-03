namespace Ave.Extensions.Functional.FluentAssertions
{
	public static class ResultExtensions
	{
		public static ResultAssertions<T,E> Should<T,E>(this Result<T,E> instance) => new ResultAssertions<T,E>(instance);
	}
}
