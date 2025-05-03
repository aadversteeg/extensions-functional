using Ave.Extensions.Functional;
using System.Threading.Tasks;
using System;
using Xunit;
using Ave.Extensions.Functional.FluentAssertions;

namespace UnitTests.Ave.Functional
{
	public class MaybeOnSomeBindExtensionsTests
	{
		[Fact(DisplayName = "MOSBE-0001: If maybe has value, the value should be bound.")]
		public void MOSBE0001()
		{
			// arrange
			var maybe = Maybe<int>.From(42);
			Func<int, Maybe<string>> map = (v) => Maybe<string>.From(v.ToString());

			// act
			var boundMaybe = maybe.OnSomeBind(map);

			// assert
			boundMaybe.Should().HaveValue("42");
		}

		[Fact(DisplayName = "MOSBE-0002: If maybe has no value, nothing is bound.")]
		public void MOSBE0002()
		{
			// arrange
			var maybe = Maybe<int>.None;
			Func<int, Maybe<string>> map = (v) => Maybe<string>.From(v.ToString());

			// act
			var boundMaybe = maybe.OnSomeBind(map);

			// assert
			boundMaybe.Should().HaveNoValue();
		}

		[Fact(DisplayName = "MOSBE-0003: If awaitable maybe has value, the value should be bound.")]
		public async Task MOSBE0003()
		{
			// arrange
			var maybe = Task.FromResult(Maybe<int>.From(42));
			Func<int, Maybe<string>> map = (v) => Maybe<string>.From(v.ToString());

			// act
			var boundMaybe = await maybe.OnSomeBind(map);

			// assert
			boundMaybe.Should().HaveValue("42");
		}

		[Fact(DisplayName = "MOSBE-0004: If awaitable maybe has no value, nothing is bound.")]
		public async Task MOSBE0004()
		{
			// arrange
			var maybe = Task.FromResult(Maybe<int>.None);
			Func<int, Maybe<string>> map = (v) => Maybe<string>.From(v.ToString());

			// act
			var boundMaybe = await maybe.OnSomeBind(map);

			// assert
			boundMaybe.Should().HaveNoValue();
		}

		[Fact(DisplayName = "MOSBE-0005: If maybe has value, the value should be bound.")]
		public async Task MOSBE0005()
		{
			// arrange
			var maybe = Maybe<int>.From(42);
			Func<int, Task<Maybe<string>>> map = (v) => Task.FromResult((Maybe<string>.From(v.ToString())));

			// act
			var boundMaybe = await maybe.OnSomeBind(map);

			// assert
			boundMaybe.Should().HaveValue("42");
		}

		[Fact(DisplayName = "MOSBE-0006: If maybe has no value, nothing is bound.")]
		public async Task MOSBE0006()
		{
			// arrange
			var maybe = Maybe<int>.None;
			Func<int, Task<Maybe<string>>> map = (v) => Task.FromResult((Maybe<string>.From(v.ToString())));

			// act
			var boundMaybe = await maybe.OnSomeBind(map);

			// assert
			boundMaybe.Should().HaveNoValue();
		}

		[Fact(DisplayName = "MOSBE-0007: If awaitable maybe has value, the value should be bound.")]
		public async Task MOSBE0007()
		{
			// arrange
			var maybe = Task.FromResult(Maybe<int>.From(42));
			Func<int, Task<Maybe<string>>> map = (v) => Task.FromResult((Maybe<string>.From(v.ToString())));

			// act
			var boundMaybe = await maybe.OnSomeBind(map);

			// assert
			boundMaybe.Should().HaveSomeValue("42");
		}

		[Fact(DisplayName = "MOSBE-0008: If awaitable maybe has no value, nothing is bound.")]
		public async Task MOSBE0008()
		{
			// arrange
			var maybe = Task.FromResult(Maybe<int>.None);
			Func<int, Task<Maybe<string>>> map = (v) => Task.FromResult((Maybe<string>.From(v.ToString())));

			// act
			var boundMaybe = await maybe.OnSomeBind(map);

			// assert
			boundMaybe.Should().HaveNoValue();
		}
	}
}
