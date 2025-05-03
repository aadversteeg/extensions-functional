using Ave.Extensions.Functional;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Ave.Functional
{
	public class ResultMatchExtensionsTests
	{
		[Fact(DisplayName = "RME-0001: If result indicates success, the value should be mapped using onSuccess.")]
		public void RME0001()
		{
			// arrange
			var result = Result<int, string>.Success(42);

			Func<int, string> onSuccess = (s) => s.ToString();
			Func<string, string> onFailure = (s) => $"<{s}>";

			// act
			var matchResult = result.Match(onSuccess, onFailure);

			// assert
			matchResult.Should().Be("42");
		}

		[Fact(DisplayName = "RME-0002: If result indicates failure, the error should be mapped using onError.")]
		public void RME0002()
		{
			// arrange
			var result = Result<int, string>.Failure("something failed");

			Func<int, string> onSuccess = (s) => s.ToString();
			Func<string, string> onFailure = (s) => $"<{s}>";

			// act
			var matchResult = result.Match(onSuccess, onFailure);

			// assert
			matchResult.Should().Be("<something failed>");
		}

		[Fact(DisplayName = "RME-0003: If result indicates success, the value should be mapped using onSuccess.")]
		public async Task RME0003()
		{
			// arrange
			var result = Task.FromResult(Result<int, string>.Success(42));

			Func<int, string> onSuccess = (s) => s.ToString();
			Func<string, string> onFailure = (s) => $"<{s}>";

			// act
			var matchResult = await result.Match(onSuccess, onFailure);

			// assert
			matchResult.Should().Be("42");
		}

		[Fact(DisplayName = "RME-0004: If result indicates failure, the error should be mapped using onError.")]
		public async Task RME0004()
		{
			// arrange
			var result = Task.FromResult(Result<int, string>.Failure("something failed"));

			Func<int, string> onSuccess = (s) => s.ToString();
			Func<string, string> onFailure = (s) => $"<{s}>";

			// act
			var matchResult = await result.Match(onSuccess, onFailure);

			// assert
			matchResult.Should().Be("<something failed>");

		}
	}
}

