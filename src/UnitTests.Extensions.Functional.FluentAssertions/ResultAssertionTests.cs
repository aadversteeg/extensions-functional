using Ave.Extensions.Functional;
using Ave.Extensions.Functional.FluentAssertions;
using FluentAssertions;
using System;
using Xunit.Sdk;
using Xunit;
using AutoFixture;

namespace UnitTests.Extensions.Functional.FluentAssertions
{
    public class ResultAssertionTests
    {
        private readonly Fixture _fixture;
        public ResultAssertionTests()
        {
            _fixture = new Fixture();
        }

        [Fact(DisplayName ="RA-001: Successful result with value should succeed")]
        public void RA001()
        {
            string value = _fixture.Create<string>();
            var result = Result<string, string>.Success(value);

            var action = () => result.Should().Succeed();

            action.Should().NotThrow();
        }

        [Fact(DisplayName ="RA-002: Successful result with value should succeed with value")]
        public void RA002()
        {
            string value = _fixture.Create<string>();
            var result = Result<string, string>.Success(value);

            result.Should().SucceedWith(value);
        }

        [Fact(DisplayName = "RA-003: Failed result with error, should give exception when it should succeed")]
        public void WhenResultIsExpectedToBeSuccessItShouldThrowWhenFailure()
        {
            var error = _fixture.Create<string>();
            var result = Result<string, string>.Failure(error);

            var act = () => result.Should().Succeed();

            act.Should().Throw<XunitException>().WithMessage("Expected Result to be successful but it failed");
        }

        [Fact(DisplayName = "RA-004: Successful result with value should give exception when it should succeed with another value")]
        public void RA004()
        {
            string value = _fixture.Create<string>(); 
            string expectedValue = _fixture.Create<string>();

            var result = Result<string, Exception>.Success(value);

            var act = () => result.Should().SucceedWith(expectedValue);

            act.Should().Throw<XunitException>().WithMessage($"Excepted Result value to be \"{expectedValue}\" but found \"{value}\"");
        }

        [Fact(DisplayName = "RA-005: Failed Result with error should fail")]
        public void RA005()
        {
            var error = _fixture.Create<string>();
            var result = Result<string, string>.Failure(error);

            result.Should().Fail();
        }

        [Fact(DisplayName = "RA-006: Failed Result with error should fail with error")]
        public void RA006()
        {
            var error = _fixture.Create<string>(); ;
            var result = Result<string, string>.Failure(error);

            result.Should().FailWith(error);
        }


        [Fact(DisplayName = "RA-007: Failed Result with error should gove an exception when it should fail with another error")]
        public void RA007()
        {
            var error = _fixture.Create<string>();
            var expectedError = _fixture.Create<string>();

            var result = Result<string, string>.Failure(error);

            var act = () => result.Should().FailWith(expectedError);

            act.Should().Throw<XunitException>().WithMessage($"Excepted Result value to be \"{expectedError}\" but found \"{error}\"");
        }

        [Fact(DisplayName = "RA-008: Success full result should gove an exception when it should fail ")]
        public void RA008()
        {
            string value = _fixture.Create<string>();
            var expectedError = _fixture.Create<string>();
            var result = Result<string, string>.Success(value);

            var act = () => result.Should().Fail();

            act.Should().Throw<XunitException>().WithMessage("Expected Result to be failure but it succeeded");
        }

        [Fact(DisplayName = "RA-009: Success full result should give an exception when it should fail with an error")]
        public void RA009()
        {
            string value = _fixture.Create<string>();
            var expectedError = _fixture.Create<string>();
            var result = Result<string, string>.Success(value);

            var act = () => result.Should().FailWith(expectedError);

            act.Should().Throw<XunitException>().WithMessage("Expected Result to be failure but it succeeded");
        }

    }
}
