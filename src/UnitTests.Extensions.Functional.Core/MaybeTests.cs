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

        [Fact(DisplayName = "M-0011: From with a null reference should have no value.")]
        public void M0011()
        {
            // arrange

            // act
            var maybe = Maybe<string>.From(null);

            // assert
            maybe.HasNoValue.Should().BeTrue();
            maybe.HasValue.Should().BeFalse();
        }

        [Fact(DisplayName = "M-0012: From with a null reference should be indistinguishable from None.")]
        public void M0012()
        {
            // arrange

            // act
            var maybe = Maybe<string>.From(null);

            // assert
            maybe.ToString().Should().Be(Maybe<string>.None.ToString());
            maybe.ToString().Should().Be("(No Value)");
        }

        [Fact(DisplayName = "M-0013: Getting value from a maybe created from a null reference should fail.")]
        public void M0013()
        {
            // arrange
            var maybe = Maybe<string>.From(null);

            // act
            var act = () => maybe.Value;

            // assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Property Value of Maybe cannot be accesses because the Maybe has no value.");
        }

        [Fact(DisplayName = "M-0014: Maybe.From with a null reference should have no value.")]
        public void M0014()
        {
            // arrange

            // act
            var maybe = Maybe.From<string>(null);

            // assert
            maybe.HasNoValue.Should().BeTrue();
        }

        [Fact(DisplayName = "M-0015: Implicit conversion of a null reference should have no value.")]
        public void M0015()
        {
            // arrange
            string? value = null;

            // act
            Maybe<string> maybe = value;

            // assert
            maybe.HasNoValue.Should().BeTrue();
        }

        [Fact(DisplayName = "M-0016: Implicit conversion of the null literal should have no value.")]
        public void M0016()
        {
            // arrange

            // act
            Maybe<string> maybe = null;

            // assert
            maybe.HasNoValue.Should().BeTrue();
        }

        [Fact(DisplayName = "M-0017: From with a reference value should have that value.")]
        public void M0017()
        {
            // arrange
            var fixture = new Fixture();
            var value = fixture.Create<string>();

            // act
            var maybe = Maybe<string>.From(value);

            // assert
            maybe.HasValue.Should().BeTrue();
            maybe.Value.Should().Be(value);
        }

        [Fact(DisplayName = "M-0018: Implicit conversion of a reference value should have that value.")]
        public void M0018()
        {
            // arrange
            var fixture = new Fixture();
            var value = fixture.Create<string>();

            // act
            Maybe<string> maybe = value;

            // assert
            maybe.HasValue.Should().BeTrue();
            maybe.Value.Should().Be(value);
        }

        [Fact(DisplayName = "M-0019: From with a nullable value type without a value should have no value.")]
        public void M0019()
        {
            // arrange

            // act
            var maybe = Maybe<int?>.From(null);

            // assert
            maybe.HasNoValue.Should().BeTrue();
        }

        [Fact(DisplayName = "M-0020: From with a nullable value type with a value should have that value.")]
        public void M0020()
        {
            // arrange
            int? value = 42;

            // act
            var maybe = Maybe<int?>.From(value);

            // assert
            maybe.HasValue.Should().BeTrue();
            maybe.Value.Should().Be(42);
        }

        [Fact(DisplayName = "M-0021: Implicit conversion of a nullable value type without a value should have no value.")]
        public void M0021()
        {
            // arrange
            int? value = null;

            // act
            Maybe<int?> maybe = value;

            // assert
            maybe.HasNoValue.Should().BeTrue();
        }

        [Fact(DisplayName = "M-0022: From with the default of a value type should have that value.")]
        public void M0022()
        {
            // arrange

            // act
            var maybe = Maybe<int>.From(0);

            // assert
            maybe.HasValue.Should().BeTrue();
            maybe.Value.Should().Be(0);
        }

        [Fact(DisplayName = "M-0023: From with false should have that value.")]
        public void M0023()
        {
            // arrange

            // act
            var maybe = Maybe<bool>.From(false);

            // assert
            maybe.HasValue.Should().BeTrue();
            maybe.Value.Should().BeFalse();
        }

        [Fact(DisplayName = "M-0024: From with the empty string should have that value.")]
        public void M0024()
        {
            // arrange

            // act
            var maybe = Maybe<string>.From(string.Empty);

            // assert
            maybe.HasValue.Should().BeTrue();
            maybe.Value.Should().Be(string.Empty);
        }

        [Fact(DisplayName = "M-0025: From with the default of a struct should have that value.")]
        public void M0025()
        {
            // arrange

            // act
            var maybe = Maybe<Guid>.From(default);

            // assert
            maybe.HasValue.Should().BeTrue();
            maybe.Value.Should().Be(Guid.Empty);
        }

        [Fact(DisplayName = "M-0026: The default of Maybe should have no value.")]
        public void M0026()
        {
            // arrange

            // act
            var maybe = default(Maybe<string>);

            // assert
            maybe.HasNoValue.Should().BeTrue();
        }
    }
}