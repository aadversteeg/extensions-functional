using Ave.Extensions.Functional;
using Ave.Extensions.Functional.FluentAssertions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Ave.Functional
{
    public class ResultTraverseAllExtensionsTests
    {
        private static Result<int, string> ParseNumber(string text) =>
            int.TryParse(text, out var number)
                ? Result<int, string>.Success(number)
                : Result<int, string>.Failure($"'{text}' is not a number");

        private static Task<Result<int, string>> ParseNumberAsync(string text) =>
            Task.FromResult(ParseNumber(text));

        private static string JoinErrors(IReadOnlyList<string> errors) => string.Join(" | ", errors);

        [Fact(DisplayName = "RTAE-0001: An empty sequence succeeds with an empty list and does not call combine.")]
        public void RTAE0001()
        {
            // arrange
            var combineCalls = 0;

            // act
            var result = Array.Empty<string>().TraverseAll(ParseNumber, errors => { combineCalls++; return JoinErrors(errors); });

            // assert
            result.Should().Succeed();
            result.Value.Should().BeEmpty();
            combineCalls.Should().Be(0);
        }

        [Fact(DisplayName = "RTAE-0002: If every item succeeds, the result holds all values in sequence order.")]
        public void RTAE0002()
        {
            // arrange
            var items = new[] { "3", "1", "2" };

            // act
            var result = items.TraverseAll(ParseNumber, JoinErrors);

            // assert
            result.Should().Succeed();
            result.Value.Should().Equal(3, 1, 2);
        }

        [Fact(DisplayName = "RTAE-0003: If every item succeeds, combine is not called.")]
        public void RTAE0003()
        {
            // arrange
            var combineCalls = 0;

            // act
            var result = new[] { "1", "2" }.TraverseAll(ParseNumber, errors => { combineCalls++; return JoinErrors(errors); });

            // assert
            result.Should().Succeed();
            combineCalls.Should().Be(0);
        }

        [Fact(DisplayName = "RTAE-0004: If one item fails, combine receives that single error and the result fails with what combine returns.")]
        public void RTAE0004()
        {
            // arrange
            IReadOnlyList<string>? received = null;

            // act
            var result = new[] { "1", "x", "3" }.TraverseAll(ParseNumber, errors => { received = errors; return "combined"; });

            // assert
            result.Should().FailWith("combined");
            received.Should().Equal("'x' is not a number");
        }

        [Fact(DisplayName = "RTAE-0005: If several items fail, combine receives every error in sequence order.")]
        public void RTAE0005()
        {
            // arrange
            var items = new[] { "b", "1", "a", "2", "c" };

            // act
            var result = items.TraverseAll(ParseNumber, JoinErrors);

            // assert
            result.Should().FailWith("'b' is not a number | 'a' is not a number | 'c' is not a number");
        }

        [Fact(DisplayName = "RTAE-0006: Every item is mapped exactly once, in sequence order, also after a failure.")]
        public void RTAE0006()
        {
            // arrange
            var visited = new List<string>();
            var items = new[] { "x", "1", "y", "2" };

            // act
            items.TraverseAll(item => { visited.Add(item); return ParseNumber(item); }, JoinErrors);

            // assert
            visited.Should().Equal("x", "1", "y", "2");
        }

        [Fact(DisplayName = "RTAE-0007: The source sequence is enumerated only once.")]
        public void RTAE0007()
        {
            // arrange
            var enumerations = 0;
            IEnumerable<string> Items()
            {
                enumerations++;
                yield return "1";
                yield return "x";
            }

            // act
            Items().TraverseAll(ParseNumber, JoinErrors);

            // assert
            enumerations.Should().Be(1);
        }

        [Fact(DisplayName = "RTAE-0008: The value list cannot be changed through a cast to a mutable list.")]
        public void RTAE0008()
        {
            // act
            var result = new[] { "1" }.TraverseAll(ParseNumber, JoinErrors);

            // assert
            result.Value.Should().NotBeAssignableTo<List<int>>();
            result.Value.Should().NotBeAssignableTo<int[]>();
        }

        [Fact(DisplayName = "RTAE-0009: A null argument throws ArgumentNullException with the parameter name.")]
        public void RTAE0009()
        {
            // arrange
            var items = new[] { "1" };

            // act
            Action nullSource = () => ((IEnumerable<string>)null!).TraverseAll(ParseNumber, JoinErrors);
            Action nullMap = () => items.TraverseAll((Func<string, Result<int, string>>)null!, JoinErrors);
            Action nullCombine = () => items.TraverseAll(ParseNumber, null!);

            // assert
            nullSource.Should().Throw<ArgumentNullException>().WithParameterName("source");
            nullMap.Should().Throw<ArgumentNullException>().WithParameterName("map");
            nullCombine.Should().Throw<ArgumentNullException>().WithParameterName("combine");
        }

        [Fact(DisplayName = "RTAE-0010: An empty sequence with an asynchronous map succeeds with an empty list and does not call combine.")]
        public async Task RTAE0010()
        {
            // arrange
            var combineCalls = 0;

            // act
            var result = await Array.Empty<string>().TraverseAll(ParseNumberAsync, errors => { combineCalls++; return JoinErrors(errors); });

            // assert
            result.Should().Succeed();
            result.Value.Should().BeEmpty();
            combineCalls.Should().Be(0);
        }

        [Fact(DisplayName = "RTAE-0011: If every item succeeds with an asynchronous map, the result holds all values in sequence order.")]
        public async Task RTAE0011()
        {
            // act
            var result = await new[] { "3", "1", "2" }.TraverseAll(ParseNumberAsync, JoinErrors);

            // assert
            result.Should().Succeed();
            result.Value.Should().Equal(3, 1, 2);
        }

        [Fact(DisplayName = "RTAE-0012: If one item fails with an asynchronous map, combine receives that single error.")]
        public async Task RTAE0012()
        {
            // arrange
            IReadOnlyList<string>? received = null;

            // act
            var result = await new[] { "1", "x" }.TraverseAll(ParseNumberAsync, errors => { received = errors; return "combined"; });

            // assert
            result.Should().FailWith("combined");
            received.Should().Equal("'x' is not a number");
        }

        [Fact(DisplayName = "RTAE-0013: If several items fail with an asynchronous map, combine receives every error in sequence order.")]
        public async Task RTAE0013()
        {
            // act
            var result = await new[] { "b", "1", "a", "c" }.TraverseAll(ParseNumberAsync, JoinErrors);

            // assert
            result.Should().FailWith("'b' is not a number | 'a' is not a number | 'c' is not a number");
        }

        [Fact(DisplayName = "RTAE-0014: An asynchronous map runs one item at a time, in sequence order, and visits every item.")]
        public async Task RTAE0014()
        {
            // arrange
            var running = 0;
            var maxRunning = 0;
            var visited = new List<string>();
            async Task<Result<int, string>> Map(string item)
            {
                running++;
                maxRunning = Math.Max(maxRunning, running);
                visited.Add(item);
                await Task.Delay(5);
                running--;
                return ParseNumber(item);
            }

            // act
            await new[] { "x", "1", "y", "2" }.TraverseAll(Map, JoinErrors);

            // assert
            maxRunning.Should().Be(1);
            visited.Should().Equal("x", "1", "y", "2");
        }

        [Fact(DisplayName = "RTAE-0015: A null argument with an asynchronous map throws at the call, before anything is awaited.")]
        public void RTAE0015()
        {
            // arrange
            var items = new[] { "1" };

            // act
            Action nullSource = () => ((IEnumerable<string>)null!).TraverseAll(ParseNumberAsync, JoinErrors);
            Action nullMap = () => items.TraverseAll((Func<string, Task<Result<int, string>>>)null!, JoinErrors);
            Action nullCombine = () => items.TraverseAll(ParseNumberAsync, null!);

            // assert
            nullSource.Should().Throw<ArgumentNullException>().WithParameterName("source");
            nullMap.Should().Throw<ArgumentNullException>().WithParameterName("awaitableMap");
            nullCombine.Should().Throw<ArgumentNullException>().WithParameterName("combine");
        }
    }
}
