using Ave.Extensions.Functional;
using Ave.Extensions.Functional.FluentAssertions;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Ave.Functional
{
    public class ResultOnSuccessDoExtensionsTests
    {
        [Fact(DisplayName = "ROSDE-0001: If result indicates success, the action should be done.")]
        public void ROSDE0001()
        {
            // arrange
            var result = Result<int, string>.Success(42);
            int actResult = 0;

            // act
            Action<int> act = (v) => actResult = v;
            var mappedResult = result.OnSuccessDo(act);

            // assert
            mappedResult.Should().SucceedWith(42);
            actResult.Should().Be(42);
        }

        [Fact(DisplayName = "ROSDE-0002: If result indicates failure, the action should not be done.")]
        public void ROSDE0002()
        {
            // arrange
            var result = Result<int, string>.Failure("Something failed");
            int actResult = 0;

            // act
            Action<int> act = (v) => actResult = v;
            var mappedResult = result.OnSuccessDo(act);

            // assert
            mappedResult.Should().FailWith("Something failed");
            actResult.Should().Be(0);
        }

		[Fact(DisplayName = "ROSDE-0003: If awaitable result indicates success, the action should be done.")]
		public async Task ROSDE0003()
		{
			// arrange
			var result = Task.FromResult(Result<int, string>.Success(42));
			int actResult = 0;

			// act
			Action<int> act = (v) => actResult = v;
			var mappedResult = await result.OnSuccessDo(act);

			// assert
			mappedResult.Should().SucceedWith(42);
			actResult.Should().Be(42);
		}

		[Fact(DisplayName = "ROSDE-0004: If awaitable result indicates failure, the action should not be done.")]
		public async Task ROSDE0004()
		{
			// arrange
			var result = Task.FromResult(Result<int, string>.Failure("Something failed"));
			int actResult = 0;

			// act
			Action<int> act = (v) => actResult = v;
			var mappedResult = await result.OnSuccessDo(act);

			// assert
			mappedResult.Should().FailWith("Something failed");
			actResult.Should().Be(0);
		}

		[Fact(DisplayName = "ROSDE-0005: If result indicates success, the action should be done.")]
		public async Task ROSDE0005()
		{
			// arrange
			var result = Result<int, string>.Success(42);
			int actResult = 0;

			// act
			Func<int, Task> act = (v) =>
			{
				actResult = v;
				return Task.CompletedTask;
			};
			var mappedResult = await result.OnSuccessDo(act);

			// assert
			mappedResult.Should().SucceedWith(42);
			actResult.Should().Be(42);
		}

		[Fact(DisplayName = "ROSDE-0006: If result indicates failure, the action should not be done.")]
		public async Task ROSDE0006()
		{
			// arrange
			var result = Result<int, string>.Failure("Something failed");
			int actResult = 0;

			// act
			Func<int, Task> act = (v) =>
			{
				actResult = v;
				return Task.CompletedTask;
			};
			var mappedResult = await result.OnSuccessDo(act);

			// assert
			mappedResult.Should().FailWith("Something failed");
			actResult.Should().Be(0);
		}

		[Fact(DisplayName = "ROSDE-0007: If awaitable result indicates success, the action should be done.")]
		public async Task ROSDE0007()
		{
			// arrange
			var result = Task.FromResult(Result<int, string>.Success(42));
			int actResult = 0;

			// act
			Func<int, Task> act = (v) =>
			{
				actResult = v;
				return Task.CompletedTask;
			};
			var mappedResult = await result.OnSuccessDo(act);

			// assert
			mappedResult.Should().SucceedWith(42);
			actResult.Should().Be(42);
		}

		[Fact(DisplayName = "ROSDE-0008: If awaitable result indicates failure, the action should not be done.")]
		public async Task ROSDE0008()
		{
			// arrange
			var result = Task.FromResult(Result<int, string>.Failure("Something failed"));
			int actResult = 0;

			// act
			Func<int, Task> act = (v) =>
			{
				actResult = v;
				return Task.CompletedTask;
			};
			var mappedResult = await result.OnSuccessDo(act);

			// assert
			mappedResult.Should().FailWith("Something failed");
			actResult.Should().Be(0);
		}
	}
}
