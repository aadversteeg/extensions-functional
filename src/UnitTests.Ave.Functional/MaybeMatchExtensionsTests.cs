using Ave.Extensions.Functional;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Ave.Functional
{
	public class MaybeMatchExtensionsTests
	{
		[Fact(DisplayName = "MME-0001: If maybe has value, the value should be mapped using onSome.")]
		public void MME0001()
		{
			// arrange
			var result = Maybe<int>.From(42);

			Func<int, string> onSome = (s) => s.ToString();
			Func<string> onNone = () => "It was none";

			// act
			var matchResult = result.Match(onSome, onNone);

			// assert
			matchResult.Should().Be("42");
		}

		[Fact(DisplayName = "MME-0002: If maybe has no value, onNone should be used to get a value.")]
		public void MME0002()
		{
			// arrange
			var result = Maybe<int>.None;

			Func<int, string> onSome = (s) => s.ToString();
			Func<string> onNone = () => "It was none";

			// act
			var matchResult = result.Match(onSome, onNone);

			// assert
			matchResult.Should().Be("It was none");
		}

		[Fact(DisplayName = "MME-0003: If awaitable maybe has value, the value should be mapped using onSome.")]
		public async Task RME0003()
		{
			// arrange
			var awaitableMaybe = Task.FromResult(Maybe<int>.From(42));

			Func<int, string> onSome = (s) => s.ToString();
			Func<string> onNone = () => "It was none";

			// act
			var matchResult = await awaitableMaybe.Match(onSome, onNone);

			// assert
			matchResult.Should().Be("42");
		}

		[Fact(DisplayName = "MME-0004: If awaitable maybe has no value, onNone should be used to get a value.")]
		public async Task MME0004()
		{
			// arrange
			var awaitableMaybe = Task.FromResult(Maybe<int>.None);

			Func<int, string> onSome = (s) => s.ToString();
			Func<string> onNone = () => "It was none";

			// act
			var matchResult = await awaitableMaybe.Match(onSome, onNone);

			// assert
			matchResult.Should().Be("It was none");
		}
	}
}
