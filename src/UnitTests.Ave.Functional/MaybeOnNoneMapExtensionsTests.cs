using Ave.Extensions.Functional;
using Ave.Extensions.Functional.FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Ave.Functional
{
	public class MaybeOnNoneMapExtensionsTests
	{
		[Fact(DisplayName = "MONME-0001: If the map returns null, the mapped maybe should have no value.")]
		public void MONME0001()
		{
			// arrange
			var maybe = Maybe<string>.None;
			Func<string> map = () => null!;

			// act
			var mappedMaybe = maybe.OnNoneMap(map);

			// assert
			mappedMaybe.Should().HaveNoValue();
		}

		[Fact(DisplayName = "MONME-0002: If the map of an awaitable maybe returns null, the mapped maybe should have no value.")]
		public async Task MONME0002()
		{
			// arrange
			var maybe = Task.FromResult(Maybe<string>.None);
			Func<string> map = () => null!;

			// act
			var mappedMaybe = await maybe.OnNoneMap(map);

			// assert
			mappedMaybe.Should().HaveNoValue();
		}

		[Fact(DisplayName = "MONME-0003: If the awaitable map returns null, the mapped maybe should have no value.")]
		public async Task MONME0003()
		{
			// arrange
			var maybe = Maybe<string>.None;
			Func<Task<string>> map = () => Task.FromResult<string>(null!);

			// act
			var mappedMaybe = await maybe.OnNoneMap(map);

			// assert
			mappedMaybe.Should().HaveNoValue();
		}

		[Fact(DisplayName = "MONME-0004: If the awaitable map of an awaitable maybe returns null, the mapped maybe should have no value.")]
		public async Task MONME0004()
		{
			// arrange
			var maybe = Task.FromResult(Maybe<string>.None);
			Func<Task<string>> map = () => Task.FromResult<string>(null!);

			// act
			var mappedMaybe = await maybe.OnNoneMap(map);

			// assert
			mappedMaybe.Should().HaveNoValue();
		}

		[Fact(DisplayName = "MONME-0005: If maybe has no value, the map should provide the value.")]
		public void MONME0005()
		{
			// arrange
			var maybe = Maybe<string>.None;
			Func<string> map = () => "42";

			// act
			var mappedMaybe = maybe.OnNoneMap(map);

			// assert
			mappedMaybe.Should().HaveValue("42");
		}

		[Fact(DisplayName = "MONME-0006: If awaitable maybe has no value, the map should provide the value.")]
		public async Task MONME0006()
		{
			// arrange
			var maybe = Task.FromResult(Maybe<string>.None);
			Func<string> map = () => "42";

			// act
			var mappedMaybe = await maybe.OnNoneMap(map);

			// assert
			mappedMaybe.Should().HaveValue("42");
		}

		[Fact(DisplayName = "MONME-0007: If maybe has no value, the awaitable map should provide the value.")]
		public async Task MONME0007()
		{
			// arrange
			var maybe = Maybe<string>.None;
			Func<Task<string>> map = () => Task.FromResult("42");

			// act
			var mappedMaybe = await maybe.OnNoneMap(map);

			// assert
			mappedMaybe.Should().HaveValue("42");
		}

		[Fact(DisplayName = "MONME-0008: If maybe has a value, the map should not be applied.")]
		public void MONME0008()
		{
			// arrange
			var maybe = Maybe<string>.From("42");
			Func<string> map = () => throw new InvalidOperationException("map should not be applied");

			// act
			var mappedMaybe = maybe.OnNoneMap(map);

			// assert
			mappedMaybe.Should().HaveValue("42");
		}
	}
}
