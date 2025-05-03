using Ave.Extensions.Functional;
using Ave.Extensions.Functional.FluentAssertions;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Ave.Functional
{
	public class ResultOnSuccessMapExtensionsTests
	{
		[Fact(DisplayName = "ROSME-0001: If result indicates success, the value should be mapped.")]
		public void ROSME0001()
		{
			// arrange
			var result = Result<int, string>.Success(42);
			Func<int, string> map = (s) => s.ToString();

			// act

			var mappedResult = result.OnSuccessMap(map);

			// assert
			mappedResult.Should().SucceedWith("42");
		}

        [Fact(DisplayName = "ROSME-0002: If result indicates failure, the value should not be mapped.")]
        public void ROSME0002()
        {
            // arrange
            var result = Result<int, string>.Failure("something failed");
			Func<int, string> map = (s) => s.ToString();

			// act
			var mappedResult = result.OnSuccessMap(map);

            // assert
            mappedResult.Should().FailWith("something failed");
        }

		[Fact(DisplayName = "ROSME-0003: If awaitable result indicates success, the value should be mapped.")]
		public async Task ROSME0003()
		{
			// arrange
			var result = Task.FromResult( Result<int, string>.Success(42));
			Func<int, string> map = (s) => s.ToString();

			// act
			var mappedResult = await result.OnSuccessMap(map);

			// assert
			mappedResult.Should().SucceedWith("42");
		}

		[Fact(DisplayName = "ROSME-0004: If awaitable result indicates failure, the value should not be mapped.")]
		public async Task ROSME0004()
		{
			// arrange
			var result = Task.FromResult(Result<int, string>.Failure("something failed"));
			Func<int, string> map = (s) => s.ToString();

			// act
			var mappedResult = await result.OnSuccessMap(map);

			// assert
			mappedResult.Should().FailWith("something failed");
		}

		[Fact(DisplayName = "ROSME-0005: If result indicates success, the value should be mapped.")]
		public async Task ROSME0005()
		{
			// arrange
			var result = Result<int, string>.Success(42);
			Func<int, Task<string>> map = (s) => Task.FromResult(s.ToString());

			// act

			var mappedResult = await result.OnSuccessMap(map);

			// assert
			mappedResult.Should().SucceedWith("42");
		}

		[Fact(DisplayName = "ROSME-0006: If result indicates failure, the value should not be mapped.")]
		public async Task ROSME0006()
		{
			// arrange
			var result = Result<int, string>.Failure("something failed");
			Func<int, Task<string>> map = (s) => Task.FromResult(s.ToString());

			// act
			var mappedResult = await result.OnSuccessMap(map);

			// assert
			mappedResult.Should().FailWith("something failed");
		}

		[Fact(DisplayName = "ROSME-0007: If awaitable result indicates success, the value should be mapped.")]
		public async Task ROSME0007()
		{
			// arrange
			var result = Task.FromResult(Result<int, string>.Success(42));
			Func<int, Task<string>> map = (s) => Task.FromResult(s.ToString());

			// act
			var mappedResult = await result.OnSuccessMap(map);

			// assert
			mappedResult.Should().SucceedWith("42");
		}

		[Fact(DisplayName = "ROSME-0008: If awaitable result indicates failure, the value should not be mapped.")]
		public async Task ROSME0008()
		{
			// arrange
			var result = Task.FromResult(Result<int, string>.Failure("something failed"));
			Func<int, Task<string>> map = (s) => Task.FromResult(s.ToString());

			// act
			var mappedResult = await result.OnSuccessMap(map);

			// assert
			mappedResult.Should().FailWith("something failed");
		}
	}
}
