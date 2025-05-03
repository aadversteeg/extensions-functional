using Ave.Extensions.Functional;
using FluentAssertions;
using System;
using Xunit;

namespace UnitTests.Ave.Functional
{
    public class VoidResultTests
    {
        [Fact(DisplayName = "VR-0001: Success should return result indicating success.")]
        public void VR0001()
        {
            // arrange

            // act
            var result = VoidResult<string>.Success();

            // assert
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
        }

        [Fact(DisplayName = "VR-0002: Failure should return result indicating failure with cause of failure.")]
        public void VR0002()
        {
            // arrange

            // act
            var result = VoidResult<string>.Failure("some error");

            // assert
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("some error");
        }

        [Fact(DisplayName = "VR-0003: Getting error on successful result should fail.")]
        public void VR0003()
        {
            // arrange
            var result = VoidResult<string>.Success();

            // act
            var act = () => result.Error;

            // assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Property Error of Result cannot be accessed because Result is successful.");
        }

        [Fact(DisplayName = "VR-0004: VoidResult should be implicitly convertible to Result<Unit,E>.")]
        public void VR0004()
        {
            // arrange
            var voidResult = VoidResult<string>.Success();

            // act
            Result<Unit, string> result = voidResult;

            // assert
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
        }

        [Fact(DisplayName = "VR-0005: Result<Unit,E> should be implicitly convertible to VoidResult.")]
        public void VR0005()
        {
            // arrange
            var unitResult = Result<Unit, string>.Success(Unit.Value);

            // act
            VoidResult<string> voidResult = unitResult;

            // assert
            voidResult.IsSuccess.Should().BeTrue();
            voidResult.IsFailure.Should().BeFalse();
        }

        [Fact(DisplayName = "VR-0006: Equal VoidResults should be considered equal.")]
        public void VR0006()
        {
            // arrange
            var result1 = VoidResult<string>.Success();
            var result2 = VoidResult<string>.Success();
            var result3 = VoidResult<string>.Failure("error");
            var result4 = VoidResult<string>.Failure("error");
            var result5 = VoidResult<string>.Failure("different error");

            // assert
            result1.Should().Be(result2);
            result3.Should().Be(result4);
            result3.Should().NotBe(result5);
            result1.Should().NotBe(result3);
        }

        [Fact(DisplayName = "VR-0007: VoidResult should be equal to equivalent Result<Unit,E>.")]
        public void VR0007()
        {
            // arrange
            var voidResult = VoidResult<string>.Success();
            var unitResult = Result<Unit, string>.Success(Unit.Value);
            var voidFailure = VoidResult<string>.Failure("error");
            var unitFailure = Result<Unit, string>.Failure("error");

            // assert
            voidResult.Should().Be(unitResult);
            voidFailure.Should().Be(unitFailure);
            (voidResult == unitResult).Should().BeTrue();
            (voidFailure == unitFailure).Should().BeTrue();
        }

        [Fact(DisplayName = "VR-0008: Equal VoidResults should have same hash code.")]
        public void VR0008()
        {
            // arrange
            var result1 = VoidResult<string>.Success();
            var result2 = VoidResult<string>.Success();
            var result3 = VoidResult<string>.Failure("error");
            var result4 = VoidResult<string>.Failure("error");

            // assert
            result1.GetHashCode().Should().Be(result2.GetHashCode());
            result3.GetHashCode().Should().Be(result4.GetHashCode());
            result1.GetHashCode().Should().NotBe(result3.GetHashCode());
        }

        [Fact(DisplayName = "V-0009: VoidResult should have same hash code as equivalent Result<Unit,E>.")]
        public void V0009()
        {
            // arrange
            var voidResult = VoidResult<string>.Success();
            var unitResult = Result<Unit, string>.Success(Unit.Value);
            var voidFailure = VoidResult<string>.Failure("error");
            var unitFailure = Result<Unit, string>.Failure("error");

            // assert
            voidResult.GetHashCode().Should().Be(unitResult.GetHashCode());
            voidFailure.GetHashCode().Should().Be(unitFailure.GetHashCode());
        }
    }
} 