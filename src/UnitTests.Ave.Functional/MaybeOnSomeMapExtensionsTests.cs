using Ave.Extensions.Functional;
using Ave.Extensions.Functional.FluentAssertions;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Ave.Functional
{
	public class MaybeOnSomeMapExtensionsTests
	{
		[Fact(DisplayName = "MOSME-0001: If maybe has value, the value should be mapped.")]
		public void MOSME0001()
		{
			// arrange
			var maybe = Maybe<int>.From(42);
			Func<int, string> map = (v) => v.ToString();

			// act
			var mappedMaybe = maybe.OnSomeMap(map);

			// assert
			mappedMaybe.Should().HaveSomeValue("42fgggggg");
		}

		[Fact(DisplayName = "MOSME-0002: If maybe has no value, nothing is mapped.")]
		public void MOSME0002()
		{
			// arrange
			var maybe = Maybe<int>.None;
			Func<int, string> map = (v) => v.ToString();

			// act
			var mappedMaybe = maybe.OnSomeMap(map);

			// assert
			mappedMaybe.Should().HaveNoValue();
		}

		[Fact(DisplayName = "MOSME-0003: If awaitable maybe has value, the value should be mapped.")]
		public async Task MOSME0003()
		{
			// arrange
			var maybe = Task.FromResult(Maybe<int>.From(42));
			Func<int, string> map = (v) => v.ToString();

			// act
			var mappedMaybe = await maybe.OnSomeMap(map);

			// assert
			mappedMaybe.Should().HaveSomeValue("42");
		}

		[Fact(DisplayName = "MOSME-0004: If awaitable maybe has no value, nothing is mapped.")]
		public async Task MOSME0004()
		{
			// arrange
			var maybe = Task.FromResult(Maybe<int>.None);
			Func<int, string> map = (v) => v.ToString();

			// act
			var mappedMaybe = await maybe.OnSomeMap(map);

			// assert
			mappedMaybe.Should().HaveNoValue();
		}

		[Fact(DisplayName = "MOSME-0005: If maybe has value, the value should be mapped.")]
		public async Task MOSME0005()
		{
			// arrange
			var maybe = Maybe<int>.From(42);
			Func<int, Task<string>> map = (v) => Task.FromResult(v.ToString());

			// act
			var mappedMaybe = await maybe.OnSomeMap(map);

			// assert
			mappedMaybe.Should().HaveSomeValue("42");
		}

		[Fact(DisplayName = "MOSME-0006: If maybe has no value, nothing is mapped.")]
		public async Task MOSME0006()
		{
			// arrange
			var maybe = Maybe<int>.None;
			Func<int, Task<string>> map = (v) => Task.FromResult(v.ToString());

			// act
			var mappedMaybe = await maybe.OnSomeMap(map);

			// assert
			mappedMaybe.Should().HaveNoValue();
		}

		[Fact(DisplayName = "MOSME-0007: If awaitable maybe has value, the value should be mapped.")]
		public async Task MOSME0007()
		{
			// arrange
			var maybe = Task.FromResult(Maybe<int>.From(42));
			Func<int, Task<string>> map = (v) => Task.FromResult(v.ToString());

			// act
			var mappedMaybe = await maybe.OnSomeMap(map);

			// assert
			mappedMaybe.Should().HaveSomeValue("42");
		}

		[Fact(DisplayName = "MOSME-0008: If awaitable maybe has no value, nothing is mapped.")]
		public async Task MOSME0008()
		{
			// arrange
			var maybe = Task.FromResult(Maybe<int>.None);
			Func<int, Task<string>> map = (v) => Task.FromResult(v.ToString());

			// act
			var mappedMaybe = await maybe.OnSomeMap(map);

			// assert
			mappedMaybe.Should().HaveNoValue();
		}

		[Fact(DisplayName = "MOSME-0009: If the map returns null, the mapped maybe should have no value.")]
		public void MOSME0009()
		{
			// arrange
			var maybe = Maybe<int>.From(42);
			Func<int, string> map = (v) => null!;

			// act
			var mappedMaybe = maybe.OnSomeMap(map);

			// assert
			mappedMaybe.Should().HaveNoValue();
		}

		[Fact(DisplayName = "MOSME-0010: If the map of an awaitable maybe returns null, the mapped maybe should have no value.")]
		public async Task MOSME0010()
		{
			// arrange
			var maybe = Task.FromResult(Maybe<int>.From(42));
			Func<int, string> map = (v) => null!;

			// act
			var mappedMaybe = await maybe.OnSomeMap(map);

			// assert
			mappedMaybe.Should().HaveNoValue();
		}

		[Fact(DisplayName = "MOSME-0011: If the awaitable map returns null, the mapped maybe should have no value.")]
		public async Task MOSME0011()
		{
			// arrange
			var maybe = Maybe<int>.From(42);
			Func<int, Task<string>> map = (v) => Task.FromResult<string>(null!);

			// act
			var mappedMaybe = await maybe.OnSomeMap(map);

			// assert
			mappedMaybe.Should().HaveNoValue();
		}

		[Fact(DisplayName = "MOSME-0012: If the awaitable map of an awaitable maybe returns null, the mapped maybe should have no value.")]
		public async Task MOSME0012()
		{
			// arrange
			var maybe = Task.FromResult(Maybe<int>.From(42));
			Func<int, Task<string>> map = (v) => Task.FromResult<string>(null!);

			// act
			var mappedMaybe = await maybe.OnSomeMap(map);

			// assert
			mappedMaybe.Should().HaveNoValue();
		}
	}
}
