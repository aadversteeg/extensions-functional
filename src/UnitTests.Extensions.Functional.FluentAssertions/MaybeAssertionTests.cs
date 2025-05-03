using AutoFixture;
using Ave.Extensions.Functional;
using Ave.Extensions.Functional.FluentAssertions;
using FluentAssertions;
using System;
using Xunit;
using Xunit.Sdk;

namespace UnitTests.Extensions.Functional.FluentAssertions
{
    public class MaybeAssertionTests
    {
        private readonly Fixture _fixture;

        public MaybeAssertionTests() 
        {
            _fixture = new Fixture();
        }

        [Fact(DisplayName ="MA-001: Maybe from value should have some value")]
        public void MA001()
        {
			var value = _fixture.Create<string>();
			var maybe = Maybe.From(value);
            maybe.Should().HaveSomeValue();
        }

        [Fact(DisplayName = "MA-002: Maybe from value should have value")]
        public void MA002()
        {
            var value = _fixture.Create<string>();
            var maybe = Maybe.From(value);

            maybe.Should().HaveValue(value);
        }

        [Fact(DisplayName = "MA-003: When maybe does not have expected value, exception should be thrown")]
        public void MA003()
        {
            var value = _fixture.Create<string>();
            var maybe = Maybe.From(value);

            var expectedValue = _fixture.Create<string>() + value;

            Action act = () => maybe.Should().HaveValue(expectedValue, "it is test");

            act.Should().Throw<XunitException>().WithMessage($"*value \"{expectedValue}\" because it is test, but with value \"{value}\" it*");
        }

        [Fact(DisplayName = "MA-004: When maybe is expected to have a value, but is None, an exception should be thrown")]
        public void MA004()
        {
            var maybe = Maybe<string>.None;
            var expectedValue = _fixture.Create<string>();

            Action act = () => maybe.Should().HaveValue(expectedValue, "it is not None");

            act.Should().Throw<XunitException>().WithMessage($"*value \"{expectedValue}\" because it is not None*");
        }

        [Fact(DisplayName = "MA-005: A maybe from None should have no value")]
        public void WhenMaybeIsExpectedToHaveNoValueAndItHasNoneShouldNotThrow()
        {
            Maybe<string> maybe = Maybe<string>.None;

            maybe.Should().HaveNoValue();
        }

        [Fact(DisplayName = "MA-006: When maybe is expected to have no value, but does have a value, an exception should be thrown")]
        public void MA006()
        {
            var value = _fixture.Create<string>();
            var maybe = Maybe.From(value);

            Action act = () => maybe.Should().HaveNoValue("it is None");

            act.Should().Throw<XunitException>().WithMessage($"*Maybe to have no value because it is None, but with value \"{value}\" it*");
        }
    }
}