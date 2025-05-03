using Ave.Extensions.Functional;
using FluentAssertions;
using System;
using Xunit;

namespace UnitTests.Ave.Functional
{
	public class ResultTests
	{
		[Fact(DisplayName = "R-0001: Success with a value should return result indicating success with value.")]
		public void R0001()
		{
			// arrange

			// act
			var result = Result<int, string>.Success(42);

			// assert
			result.IsSuccess.Should().BeTrue();
			result.IsFailure.Should().BeFalse();
			result.Value.Should().Be(42);
		}

		[Fact(DisplayName = "R-0002: Failure for a certain type of value should return result indicating failure with cause of failure.")]
		public void R0002()
		{
			// arrange

			// act
			var result = Result<int, string>.Failure("some error");

			// assert
			result.IsSuccess.Should().BeFalse();
			result.IsFailure.Should().BeTrue();
			result.Error.Should().Be("some error");
		}

		[Fact(DisplayName = "R-0003: Getting value on result indicating failure should fail.")]
		public void R0003()
		{
			// arrange

			// act
			var result = Result<int, string>.Failure("some error");
			var act = () => result.Value;

			// assert
			act.Should().Throw<InvalidOperationException>()
				.WithMessage("Property Value of Result cannot be accessed because Result is failure.");
		}

		[Fact(DisplayName = "R-0004: Getting errors on result indicating success with a value should fail.")]
		public void R0004()
		{
			// arrange

			// act
			var result = Result<int, string>.Success(42);
			var act = () => result.Error;

			// assert
			act.Should().Throw<InvalidOperationException>()
				.WithMessage("Property Error of Result cannot be accessed because Result is successful.");
		}

		[Fact(DisplayName = "R-0005: Equal success Results should be considered equal.")]
		public void R0005()
		{
			// arrange
			var result1 = Result<int, string>.Success(42);
			var result2 = Result<int, string>.Success(42);
			var result3 = Result<int, string>.Success(43);

			// assert
			result1.Should().Be(result2);
			result1.Should().NotBe(result3);
			(result1 == result2).Should().BeTrue();
			(result1 != result3).Should().BeTrue();
		}

		[Fact(DisplayName = "R-0006: Equal failure Results should be considered equal.")]
		public void R0006()
		{
			// arrange
			var result1 = Result<int, string>.Failure("error");
			var result2 = Result<int, string>.Failure("error");
			var result3 = Result<int, string>.Failure("different error");

			// assert
			result1.Should().Be(result2);
			result1.Should().NotBe(result3);
			(result1 == result2).Should().BeTrue();
			(result1 != result3).Should().BeTrue();
		}

		[Fact(DisplayName = "R-0007: Success and failure Results should not be equal.")]
		public void R0007()
		{
			// arrange
			var success = Result<int, string>.Success(42);
			var failure = Result<int, string>.Failure("error");

			// assert
			success.Should().NotBe(failure);
			(success != failure).Should().BeTrue();
		}

		[Fact(DisplayName = "R-0008: Equal Results should have same hash code.")]
		public void R0008()
		{
			// arrange
			var success1 = Result<int, string>.Success(42);
			var success2 = Result<int, string>.Success(42);
			var failure1 = Result<int, string>.Failure("error");
			var failure2 = Result<int, string>.Failure("error");

			// assert
			success1.GetHashCode().Should().Be(success2.GetHashCode());
			failure1.GetHashCode().Should().Be(failure2.GetHashCode());
			success1.GetHashCode().Should().NotBe(failure1.GetHashCode());
		}

		[Fact(DisplayName = "R-0009: Different Results should have different hash codes.")]
		public void R0009()
		{
			// arrange
			var success1 = Result<int, string>.Success(42);
			var success2 = Result<int, string>.Success(43);
			var failure1 = Result<int, string>.Failure("error1");
			var failure2 = Result<int, string>.Failure("error2");

			// assert
			success1.GetHashCode().Should().NotBe(success2.GetHashCode());
			failure1.GetHashCode().Should().NotBe(failure2.GetHashCode());
		}
	}
}
