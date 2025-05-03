using Ave.Extensions.Functional;
using Ave.Extensions.Functional.FluentAssertions;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Ave.Functional
{
    public class ResultOnFailureExtensionsTests
    {
        [Fact(DisplayName = "ROFE-0001: If result indicates failure, the error should be mapped.")]
        public void ROFE0001()
        {
            // arrange
            var result = Result<int, int>.Failure(42);
			Func<int, string> map = (s) => s.ToString();

			// act
			var mappedResult = result.OnFailure(map);

            // assert
            mappedResult.Should().FailWith("42");
        }

        [Fact(DisplayName = "ROFE-0002: If result indicates success, the error should not be mapped.")]
        public void ROFE0002()
        {
            // arrange
            var result = Result<int, int>.Success(42);
			Func<int, string> map = (s) => s.ToString();

			// act
			var mappedResult = result.OnFailure(map);

            // assert
            mappedResult.Should().SucceedWith(42);
        }

		[Fact(DisplayName = "ROFE-0003: If awaitable result indicates failure, the error should be mapped.")]
		public async Task ROFE0003()
		{
			// arrange
			var result = Task.FromResult(Result<int, int>.Failure(42));
			Func<int, string> map = (s) => s.ToString();

			// act
			var mappedResult = await result.OnFailure(map);

			// assert
			mappedResult.Should().FailWith("42");
		}

		[Fact(DisplayName = "ROFE-0004: If awaitable result indicates success, the error should not be mapped.")]
		public async Task ROFE0004()
		{
			// arrange
			var result = Task.FromResult(Result<int, int>.Success(42));
			Func<int, string> map = (s) => s.ToString();

			// act
			var mappedResult = await result.OnFailure(map);

			// assert
			mappedResult.Should().SucceedWith(42);
		}

		[Fact(DisplayName = "ROFE-0005: If result indicates failure, the error should be mapped.")]
		public async Task ROFE0005()
		{
			// arrange
			var result = Result<int, int>.Failure(42);
			Func<int, Task<string>> map = (s) => Task.FromResult(s.ToString());

			// act
			var mappedResult = await result.OnFailure(map);

			// assert
			mappedResult.Should().FailWith("42");
		}

		[Fact(DisplayName = "ROFE-0006: If result indicates success, the error should not be mapped.")]
		public async Task ROFE0006()
		{
			// arrange
			var result = Result<int, int>.Success(42);
			Func<int, Task<string>> map = (s) => Task.FromResult(s.ToString());

			// act
			var mappedResult = await result.OnFailure(map);

			// assert
			mappedResult.Should().SucceedWith(42);
		}

		[Fact(DisplayName = "ROFE-0007: If awaitable result indicates failure, the error should be mapped.")]
		public async Task ROFE0007()
		{
			// arrange
			var result = Task.FromResult(Result<int, int>.Failure(42));
			Func<int, Task<string>> map = (s) => Task.FromResult(s.ToString());

			// act
			var mappedResult = await result.OnFailure(map);

			// assert
			mappedResult.Should().FailWith("42");
		}

		[Fact(DisplayName = "ROFE-0008: If awaitable result indicates success, the error should not be mapped.")]
		public async Task ROFE0008()
		{
			// arrange
			var result = Task.FromResult(Result<int, int>.Success(42));
			Func<int, Task<string>> map = (s) => Task.FromResult(s.ToString());

			// act
			var mappedResult = await result.OnFailure(map);

			// assert
			mappedResult.Should().SucceedWith(42);
		}
	}
}
