using Ave.Extensions.Functional;
using Ave.Extensions.Functional.FluentAssertions;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Ave.Functional
{
	public class ResultOnSuccessBindExtensionsTests
	{
        [Fact(DisplayName = "ROSBE-0001: If result indicates success, the value should be bound.")]
        public void ROSE0001()
        {
            // arrange
            var result = Result<int, string>.Success(42);

            // act
            var bindResult = result.OnSuccessBind((s) => Result<string, string>.Success( s.ToString()));

            // assert
            bindResult.Should().SucceedWith("42");
        }

        [Fact(DisplayName = "ROSBE-0002: If result indicates failure, the value should not be bound.")]
        public void ROSBE0002()
        {
            // arrange
            var result = Result<int, string>.Failure("something failed");

            // act
            var bindResult = result.OnSuccessBind((s) => Result<string, string>.Success(s.ToString()));

            // assert
            bindResult.Should().FailWith("something failed");
        }

        [Fact(DisplayName = "ROSBE-0003: If result indicates success, but the bind fails, the result will indicate failure.")]
        public void ROSBE0003()
        {
            // arrange
            var result = Result<int, string>.Success(42);

            // act
            var bindResult = result.OnSuccessBind((s) => Result<string, string>.Failure(("bind failed")));

            // assert
            bindResult.Should().FailWith("bind failed");
        }

		[Fact(DisplayName = "ROSBE-0004: If awaitable result indicates success, the value should be bound.")]
		public async Task ROSE0004()
		{
			// arrange
			var result = Task.FromResult(Result<int, string>.Success(42));

			// act
			var bindResult = await result.OnSuccessBind((s) => Result<string, string>.Success(s.ToString()));

			// assert
			bindResult.Should().SucceedWith("42");
		}

		[Fact(DisplayName = "ROSBE-0005: If awaitable result indicates failure, the value should not be bound.")]
		public async Task ROSBE0005()
		{
			// arrange
			var result = Task.FromResult(Result<int, string>.Failure("something failed"));

			// act
			var bindResult = await result.OnSuccessBind((s) => Result<string, string>.Success(s.ToString()));

			// assert
			bindResult.Should().FailWith("something failed");
		}

		[Fact(DisplayName = "ROSBE-0006: If awaitable result indicates success, but the bind fails, the result will indicate failure.")]
		public async Task ROSBE0006()
		{
			// arrange
			var result = Task.FromResult(Result<int, string>.Success(42));

			// act
			var bindResult = await result.OnSuccessBind((s) => Result<string, string>.Failure(("bind failed")));
			
			// assert
			bindResult.Should().FailWith("bind failed");
		}

		[Fact(DisplayName = "ROSBE-0007: If result indicates failure, the value should not be bound.")]
		public async Task ROSBE0007()
		{
			// arrange
			var result = Result<int, string>.Failure("something failed");

			// act
			var bindResult = await result.OnSuccessBind((s) => Task.FromResult(Result<string, string>.Success(s.ToString())));

			// assert
			bindResult.Should().FailWith("something failed");
		}

		[Fact(DisplayName = "ROSBE-0008: If result indicates success, but the bind fails, the result will indicate failure.")]
		public async Task ROSBE0008()
		{
			// arrange
			var result = Result<int, string>.Success(42);

			// act
			var bindResult = await result.OnSuccessBind((s) => Task.FromResult(Result<string, string>.Failure(("bind failed"))));

			// assert
			bindResult.Should().FailWith("bind failed");
		}

		[Fact(DisplayName = "ROSBE-0009: If awaitable result indicates failure, the value should not be bound.")]
		public async Task ROSBE0009()
		{
			// arrange
			var result = Task.FromResult(Result<int, string>.Failure("something failed"));

			// act
			var bindResult = await result.OnSuccessBind((s) => Task.FromResult(Result<string, string>.Success(s.ToString())));

			// assert
			bindResult.Should().FailWith("something failed");
		}

		[Fact(DisplayName = "ROSBE-0010: If awaitable result indicates success, but the bind fails, the result will indicate failure.")]
		public async Task ROSBE0010()
		{
			// arrange
			var result = Task.FromResult(Result<int, string>.Success(42));

			// act
			var bindResult = await result.OnSuccessBind((s) => Task.FromResult(Result<string, string>.Failure(("bind failed"))));

			// assert
			bindResult.Should().FailWith("bind failed");
		}
	}
}
