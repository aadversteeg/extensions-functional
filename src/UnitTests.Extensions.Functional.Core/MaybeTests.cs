using AutoFixture;
using Ave.Extensions.Functional;
using FluentAssertions;
using System;
using Xunit;

namespace UnitTests.Ave.Functional
{
	public class MaybeTests
	{
		[Fact(DisplayName = "M-0001: String representation of may without value is (No Value).")]
		public void M0001()
		{
			// arrange

			// act
			var mayBeAsString = Maybe<int>.None.ToString();

			// assert
			mayBeAsString.Should().Be("(No Value)");
		}

		[Fact(DisplayName = "M-0002: String representation of maybe with value is string representation of value.")]
		public void M0002()
		{
			// arrange
			var maybe = Maybe.From(42);

			// act
			var mayBeAsString = maybe.ToString();

			// assert
			mayBeAsString.Should().Be("42");
		}

        [Fact(DisplayName = "M-0003: Getting value from maybe with no value should fail.")]
        public void M0003()
        {
            // arrange
            var maybe = Maybe<int>.None;

            // act
            var act = () => maybe.Value;

            // assert
           act.Should().Throw<InvalidOperationException>()
                .WithMessage("Property Value of Maybe cannot be accesses because the Maybe has no value.");
        }

        [Fact(DisplayName = "M-0010: Value should return value of maybe.From.")]
        public void M0004()
        {
            // arrange
            var fixture = new Fixture();
            var value = fixture.Create<int>();
            var maybe = Maybe.From(value);

            // act
            var valueFromMaybe = maybe.Value;

            // assert
            valueFromMaybe.Should().Be(value);
        }
    }
}