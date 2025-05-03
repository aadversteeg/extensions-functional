using Ave.Extensions.Functional;
using Ave.Extensions.Functional.FluentAssertions;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Ave.Functional
{
    public class ResultOnFailureDoExtensionsTests
    {
        [Fact(DisplayName = "ROFDE-0001: If result indicates failure, the action should be done.")]
        public void ROFDE0001()
        {
            // arrange
            var result = Result<int, string>.Failure("Something failed");
            string actResult = string.Empty;

            // act
            Action<string> act = (e) => actResult = e;
            var mappedResult = result.OnFailureDo(act);

            // assert
            mappedResult.Should().FailWith("Something failed");
            actResult.Should().Be("Something failed");
        }

        [Fact(DisplayName = "ROFDE-0002: If result indicates success, the action should not be done.")]
        public void ROFDE0002()
        {
            // arrange
            var result = Result<int, string>.Success(42);
            string actResult = string.Empty;

            // act
            Action<string> act = (e) => actResult = e;
            var mappedResult = result.OnFailureDo(act);

            // assert
            mappedResult.Should().SucceedWith(42);
            actResult.Should().BeEmpty();
        }

		[Fact(DisplayName = "ROFDE-0003: If awaitable result indicates failure, the action should be done.")]
		public async Task ROFDE0003()
		{
			// arrange
			var result = Task.FromResult(Result<int, string>.Failure("Something failed"));
			string actResult = string.Empty;

			// act
			Action<string> act = (e) => actResult = e;
			var mappedResult = await result.OnFailureDo(act);

			// assert
			mappedResult.Should().FailWith("Something failed");
			actResult.Should().Be("Something failed");
		}

		[Fact(DisplayName = "ROFDE-0004: If awaitable result indicates success, the action should not be done.")]
		public async Task ROFDE0004()
		{
			// arrange
			var result = Task.FromResult(Result<int, string>.Success(42));
			string actResult = string.Empty;

			// act
			Action<string> act = (e) => actResult = e;
			var mappedResult = await result.OnFailureDo(act);

			// assert
			mappedResult.Should().SucceedWith(42);
			actResult.Should().BeEmpty();
		}

		[Fact(DisplayName = "ROFDE-0005: If result indicates failure, the action should be done.")]
		public async Task ROFDE0005()
		{
			// arrange
			var result = Result<int, string>.Failure("Something failed");
			string actResult = string.Empty;

			// act
			Func<string, Task> act = (e) =>
			{
				actResult = e;
				return Task.CompletedTask;
			};
			var mappedResult = await result.OnFailureDo(act);

			// assert
			mappedResult.Should().FailWith("Something failed");
			actResult.Should().Be("Something failed");
		}

		[Fact(DisplayName = "ROFDE-0006: If result indicates success, the action should not be done.")]
		public async Task ROFDE0006()
		{
			// arrange
			var result = Result<int, string>.Success(42);
			string actResult = string.Empty;

			// act
			Func<string, Task> act = (e) =>
			{
				actResult = e;
				return Task.CompletedTask;
			};
			var mappedResult = await result.OnFailureDo(act);

			// assert
			mappedResult.Should().SucceedWith(42);
			actResult.Should().BeEmpty();
		}

		[Fact(DisplayName = "ROFDE-0007: If awaitable result indicates failure, the action should be done.")]
		public async Task ROFDE0007()
		{
			// arrange
			var result = Task.FromResult(Result<int, string>.Failure("Something failed"));
			string actResult = string.Empty;

			// act
			Func<string, Task> act = (e) =>
			{
				actResult = e;
				return Task.CompletedTask;
			};
			var mappedResult = await result.OnFailureDo(act);

			// assert
			mappedResult.Should().FailWith("Something failed");
			actResult.Should().Be("Something failed");
		}

		[Fact(DisplayName = "ROFDE-0008: If awaitable result indicates success, the action should not be done.")]
		public async Task ROFDE0008()
		{
			// arrange
			var result = Task.FromResult(Result<int, string>.Success(42));
			string actResult = string.Empty;

			// act
			Func<string, Task> act = (e) =>
			{
				actResult = e;
				return Task.CompletedTask;
			};
			var mappedResult = await result.OnFailureDo(act);

			// assert
			mappedResult.Should().SucceedWith(42);
			actResult.Should().BeEmpty();
		}
	}
}
