using Ave.Extensions.Functional;
using Ave.Extensions.Functional.FluentAssertions;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Ave.Functional
{
    /// <summary>
    /// One success and one failure test per tuple overload (OnSuccessCombine and the spreading OnSuccessMap,
    /// OnSuccessBind, OnSuccessDo and Match). Each test calls its overload with a lambda of separate parameters,
    /// so compiling this file also proves that every overload is selected without ambiguity.
    /// </summary>
    public class ResultTupleOverloadsTests
    {
        [Fact(DisplayName = "RTO-0001: OnSuccessCombine on a successful Result of 1 value(s) with a function should pass them in order and add the new value.")]
        public void RTO0001()
        {
            // arrange
            string? received = null;

            // act
            var result = Result<int, string>.Success(1).OnSuccessCombine((a1) => { received = string.Join(",", new object[] { a1 }); return Result<int, string>.Success(2); });

            // assert
            received.Should().Be("1");
            result.Should().SucceedWith((1, 2));
        }

        [Fact(DisplayName = "RTO-0002: OnSuccessCombine on a failed Result of 1 value(s) with a function should return the error and not run the step.")]
        public void RTO0002()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<int, string>.Failure("source failed").OnSuccessCombine((a1) => { invoked = true; return Result<int, string>.Success(2); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0003: OnSuccessCombine on a successful Result of 1 value(s) with a failing function should return the step's error.")]
        public void RTO0003()
        {
            // act
            var result = Result<int, string>.Success(1).OnSuccessCombine((a1) => {  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0004: OnSuccessCombine on a successful awaitable Result of 1 value(s) with a function should pass them in order and add the new value.")]
        public async Task RTO0004()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<int, string>.Success(1)).OnSuccessCombine((a1) => { received = string.Join(",", new object[] { a1 }); return Result<int, string>.Success(2); });

            // assert
            received.Should().Be("1");
            result.Should().SucceedWith((1, 2));
        }

        [Fact(DisplayName = "RTO-0005: OnSuccessCombine on a failed awaitable Result of 1 value(s) with a function should return the error and not run the step.")]
        public async Task RTO0005()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<int, string>.Failure("source failed")).OnSuccessCombine((a1) => { invoked = true; return Result<int, string>.Success(2); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0006: OnSuccessCombine on a successful awaitable Result of 1 value(s) with a failing function should return the step's error.")]
        public async Task RTO0006()
        {
            // act
            var result = await Task.FromResult(Result<int, string>.Success(1)).OnSuccessCombine((a1) => {  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0007: OnSuccessCombine on a successful Result of 1 value(s) with an asynchronous function should pass them in order and add the new value.")]
        public async Task RTO0007()
        {
            // arrange
            string? received = null;

            // act
            var result = await Result<int, string>.Success(1).OnSuccessCombine(async (a1) => { await Task.Yield(); received = string.Join(",", new object[] { a1 }); return Result<int, string>.Success(2); });

            // assert
            received.Should().Be("1");
            result.Should().SucceedWith((1, 2));
        }

        [Fact(DisplayName = "RTO-0008: OnSuccessCombine on a failed Result of 1 value(s) with an asynchronous function should return the error and not run the step.")]
        public async Task RTO0008()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<int, string>.Failure("source failed").OnSuccessCombine(async (a1) => { await Task.Yield(); invoked = true; return Result<int, string>.Success(2); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0009: OnSuccessCombine on a successful Result of 1 value(s) with a failing asynchronous function should return the step's error.")]
        public async Task RTO0009()
        {
            // act
            var result = await Result<int, string>.Success(1).OnSuccessCombine(async (a1) => { await Task.Yield();  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0010: OnSuccessCombine on a successful awaitable Result of 1 value(s) with an asynchronous function should pass them in order and add the new value.")]
        public async Task RTO0010()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<int, string>.Success(1)).OnSuccessCombine(async (a1) => { await Task.Yield(); received = string.Join(",", new object[] { a1 }); return Result<int, string>.Success(2); });

            // assert
            received.Should().Be("1");
            result.Should().SucceedWith((1, 2));
        }

        [Fact(DisplayName = "RTO-0011: OnSuccessCombine on a failed awaitable Result of 1 value(s) with an asynchronous function should return the error and not run the step.")]
        public async Task RTO0011()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<int, string>.Failure("source failed")).OnSuccessCombine(async (a1) => { await Task.Yield(); invoked = true; return Result<int, string>.Success(2); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0012: OnSuccessCombine on a successful awaitable Result of 1 value(s) with a failing asynchronous function should return the step's error.")]
        public async Task RTO0012()
        {
            // act
            var result = await Task.FromResult(Result<int, string>.Success(1)).OnSuccessCombine(async (a1) => { await Task.Yield();  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0013: OnSuccessCombine on a successful Result of 2 value(s) with a function should pass them in order and add the new value.")]
        public void RTO0013()
        {
            // arrange
            string? received = null;

            // act
            var result = Result<(int, int), string>.Success((1, 2)).OnSuccessCombine((a1, a2) => { received = string.Join(",", new object[] { a1, a2 }); return Result<int, string>.Success(3); });

            // assert
            received.Should().Be("1,2");
            result.Should().SucceedWith((1, 2, 3));
        }

        [Fact(DisplayName = "RTO-0014: OnSuccessCombine on a failed Result of 2 value(s) with a function should return the error and not run the step.")]
        public void RTO0014()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<(int, int), string>.Failure("source failed").OnSuccessCombine((a1, a2) => { invoked = true; return Result<int, string>.Success(3); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0015: OnSuccessCombine on a successful Result of 2 value(s) with a failing function should return the step's error.")]
        public void RTO0015()
        {
            // act
            var result = Result<(int, int), string>.Success((1, 2)).OnSuccessCombine((a1, a2) => {  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0016: OnSuccessCombine on a successful awaitable Result of 2 value(s) with a function should pass them in order and add the new value.")]
        public async Task RTO0016()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<(int, int), string>.Success((1, 2))).OnSuccessCombine((a1, a2) => { received = string.Join(",", new object[] { a1, a2 }); return Result<int, string>.Success(3); });

            // assert
            received.Should().Be("1,2");
            result.Should().SucceedWith((1, 2, 3));
        }

        [Fact(DisplayName = "RTO-0017: OnSuccessCombine on a failed awaitable Result of 2 value(s) with a function should return the error and not run the step.")]
        public async Task RTO0017()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int), string>.Failure("source failed")).OnSuccessCombine((a1, a2) => { invoked = true; return Result<int, string>.Success(3); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0018: OnSuccessCombine on a successful awaitable Result of 2 value(s) with a failing function should return the step's error.")]
        public async Task RTO0018()
        {
            // act
            var result = await Task.FromResult(Result<(int, int), string>.Success((1, 2))).OnSuccessCombine((a1, a2) => {  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0019: OnSuccessCombine on a successful Result of 2 value(s) with an asynchronous function should pass them in order and add the new value.")]
        public async Task RTO0019()
        {
            // arrange
            string? received = null;

            // act
            var result = await Result<(int, int), string>.Success((1, 2)).OnSuccessCombine(async (a1, a2) => { await Task.Yield(); received = string.Join(",", new object[] { a1, a2 }); return Result<int, string>.Success(3); });

            // assert
            received.Should().Be("1,2");
            result.Should().SucceedWith((1, 2, 3));
        }

        [Fact(DisplayName = "RTO-0020: OnSuccessCombine on a failed Result of 2 value(s) with an asynchronous function should return the error and not run the step.")]
        public async Task RTO0020()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<(int, int), string>.Failure("source failed").OnSuccessCombine(async (a1, a2) => { await Task.Yield(); invoked = true; return Result<int, string>.Success(3); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0021: OnSuccessCombine on a successful Result of 2 value(s) with a failing asynchronous function should return the step's error.")]
        public async Task RTO0021()
        {
            // act
            var result = await Result<(int, int), string>.Success((1, 2)).OnSuccessCombine(async (a1, a2) => { await Task.Yield();  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0022: OnSuccessCombine on a successful awaitable Result of 2 value(s) with an asynchronous function should pass them in order and add the new value.")]
        public async Task RTO0022()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<(int, int), string>.Success((1, 2))).OnSuccessCombine(async (a1, a2) => { await Task.Yield(); received = string.Join(",", new object[] { a1, a2 }); return Result<int, string>.Success(3); });

            // assert
            received.Should().Be("1,2");
            result.Should().SucceedWith((1, 2, 3));
        }

        [Fact(DisplayName = "RTO-0023: OnSuccessCombine on a failed awaitable Result of 2 value(s) with an asynchronous function should return the error and not run the step.")]
        public async Task RTO0023()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int), string>.Failure("source failed")).OnSuccessCombine(async (a1, a2) => { await Task.Yield(); invoked = true; return Result<int, string>.Success(3); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0024: OnSuccessCombine on a successful awaitable Result of 2 value(s) with a failing asynchronous function should return the step's error.")]
        public async Task RTO0024()
        {
            // act
            var result = await Task.FromResult(Result<(int, int), string>.Success((1, 2))).OnSuccessCombine(async (a1, a2) => { await Task.Yield();  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0025: OnSuccessCombine on a successful Result of 3 value(s) with a function should pass them in order and add the new value.")]
        public void RTO0025()
        {
            // arrange
            string? received = null;

            // act
            var result = Result<(int, int, int), string>.Success((1, 2, 3)).OnSuccessCombine((a1, a2, a3) => { received = string.Join(",", new object[] { a1, a2, a3 }); return Result<int, string>.Success(4); });

            // assert
            received.Should().Be("1,2,3");
            result.Should().SucceedWith((1, 2, 3, 4));
        }

        [Fact(DisplayName = "RTO-0026: OnSuccessCombine on a failed Result of 3 value(s) with a function should return the error and not run the step.")]
        public void RTO0026()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<(int, int, int), string>.Failure("source failed").OnSuccessCombine((a1, a2, a3) => { invoked = true; return Result<int, string>.Success(4); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0027: OnSuccessCombine on a successful Result of 3 value(s) with a failing function should return the step's error.")]
        public void RTO0027()
        {
            // act
            var result = Result<(int, int, int), string>.Success((1, 2, 3)).OnSuccessCombine((a1, a2, a3) => {  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0028: OnSuccessCombine on a successful awaitable Result of 3 value(s) with a function should pass them in order and add the new value.")]
        public async Task RTO0028()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Success((1, 2, 3))).OnSuccessCombine((a1, a2, a3) => { received = string.Join(",", new object[] { a1, a2, a3 }); return Result<int, string>.Success(4); });

            // assert
            received.Should().Be("1,2,3");
            result.Should().SucceedWith((1, 2, 3, 4));
        }

        [Fact(DisplayName = "RTO-0029: OnSuccessCombine on a failed awaitable Result of 3 value(s) with a function should return the error and not run the step.")]
        public async Task RTO0029()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Failure("source failed")).OnSuccessCombine((a1, a2, a3) => { invoked = true; return Result<int, string>.Success(4); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0030: OnSuccessCombine on a successful awaitable Result of 3 value(s) with a failing function should return the step's error.")]
        public async Task RTO0030()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Success((1, 2, 3))).OnSuccessCombine((a1, a2, a3) => {  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0031: OnSuccessCombine on a successful Result of 3 value(s) with an asynchronous function should pass them in order and add the new value.")]
        public async Task RTO0031()
        {
            // arrange
            string? received = null;

            // act
            var result = await Result<(int, int, int), string>.Success((1, 2, 3)).OnSuccessCombine(async (a1, a2, a3) => { await Task.Yield(); received = string.Join(",", new object[] { a1, a2, a3 }); return Result<int, string>.Success(4); });

            // assert
            received.Should().Be("1,2,3");
            result.Should().SucceedWith((1, 2, 3, 4));
        }

        [Fact(DisplayName = "RTO-0032: OnSuccessCombine on a failed Result of 3 value(s) with an asynchronous function should return the error and not run the step.")]
        public async Task RTO0032()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<(int, int, int), string>.Failure("source failed").OnSuccessCombine(async (a1, a2, a3) => { await Task.Yield(); invoked = true; return Result<int, string>.Success(4); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0033: OnSuccessCombine on a successful Result of 3 value(s) with a failing asynchronous function should return the step's error.")]
        public async Task RTO0033()
        {
            // act
            var result = await Result<(int, int, int), string>.Success((1, 2, 3)).OnSuccessCombine(async (a1, a2, a3) => { await Task.Yield();  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0034: OnSuccessCombine on a successful awaitable Result of 3 value(s) with an asynchronous function should pass them in order and add the new value.")]
        public async Task RTO0034()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Success((1, 2, 3))).OnSuccessCombine(async (a1, a2, a3) => { await Task.Yield(); received = string.Join(",", new object[] { a1, a2, a3 }); return Result<int, string>.Success(4); });

            // assert
            received.Should().Be("1,2,3");
            result.Should().SucceedWith((1, 2, 3, 4));
        }

        [Fact(DisplayName = "RTO-0035: OnSuccessCombine on a failed awaitable Result of 3 value(s) with an asynchronous function should return the error and not run the step.")]
        public async Task RTO0035()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Failure("source failed")).OnSuccessCombine(async (a1, a2, a3) => { await Task.Yield(); invoked = true; return Result<int, string>.Success(4); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0036: OnSuccessCombine on a successful awaitable Result of 3 value(s) with a failing asynchronous function should return the step's error.")]
        public async Task RTO0036()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Success((1, 2, 3))).OnSuccessCombine(async (a1, a2, a3) => { await Task.Yield();  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0037: OnSuccessCombine on a successful Result of 4 value(s) with a function should pass them in order and add the new value.")]
        public void RTO0037()
        {
            // arrange
            string? received = null;

            // act
            var result = Result<(int, int, int, int), string>.Success((1, 2, 3, 4)).OnSuccessCombine((a1, a2, a3, a4) => { received = string.Join(",", new object[] { a1, a2, a3, a4 }); return Result<int, string>.Success(5); });

            // assert
            received.Should().Be("1,2,3,4");
            result.Should().SucceedWith((1, 2, 3, 4, 5));
        }

        [Fact(DisplayName = "RTO-0038: OnSuccessCombine on a failed Result of 4 value(s) with a function should return the error and not run the step.")]
        public void RTO0038()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<(int, int, int, int), string>.Failure("source failed").OnSuccessCombine((a1, a2, a3, a4) => { invoked = true; return Result<int, string>.Success(5); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0039: OnSuccessCombine on a successful Result of 4 value(s) with a failing function should return the step's error.")]
        public void RTO0039()
        {
            // act
            var result = Result<(int, int, int, int), string>.Success((1, 2, 3, 4)).OnSuccessCombine((a1, a2, a3, a4) => {  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0040: OnSuccessCombine on a successful awaitable Result of 4 value(s) with a function should pass them in order and add the new value.")]
        public async Task RTO0040()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Success((1, 2, 3, 4))).OnSuccessCombine((a1, a2, a3, a4) => { received = string.Join(",", new object[] { a1, a2, a3, a4 }); return Result<int, string>.Success(5); });

            // assert
            received.Should().Be("1,2,3,4");
            result.Should().SucceedWith((1, 2, 3, 4, 5));
        }

        [Fact(DisplayName = "RTO-0041: OnSuccessCombine on a failed awaitable Result of 4 value(s) with a function should return the error and not run the step.")]
        public async Task RTO0041()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Failure("source failed")).OnSuccessCombine((a1, a2, a3, a4) => { invoked = true; return Result<int, string>.Success(5); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0042: OnSuccessCombine on a successful awaitable Result of 4 value(s) with a failing function should return the step's error.")]
        public async Task RTO0042()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Success((1, 2, 3, 4))).OnSuccessCombine((a1, a2, a3, a4) => {  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0043: OnSuccessCombine on a successful Result of 4 value(s) with an asynchronous function should pass them in order and add the new value.")]
        public async Task RTO0043()
        {
            // arrange
            string? received = null;

            // act
            var result = await Result<(int, int, int, int), string>.Success((1, 2, 3, 4)).OnSuccessCombine(async (a1, a2, a3, a4) => { await Task.Yield(); received = string.Join(",", new object[] { a1, a2, a3, a4 }); return Result<int, string>.Success(5); });

            // assert
            received.Should().Be("1,2,3,4");
            result.Should().SucceedWith((1, 2, 3, 4, 5));
        }

        [Fact(DisplayName = "RTO-0044: OnSuccessCombine on a failed Result of 4 value(s) with an asynchronous function should return the error and not run the step.")]
        public async Task RTO0044()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<(int, int, int, int), string>.Failure("source failed").OnSuccessCombine(async (a1, a2, a3, a4) => { await Task.Yield(); invoked = true; return Result<int, string>.Success(5); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0045: OnSuccessCombine on a successful Result of 4 value(s) with a failing asynchronous function should return the step's error.")]
        public async Task RTO0045()
        {
            // act
            var result = await Result<(int, int, int, int), string>.Success((1, 2, 3, 4)).OnSuccessCombine(async (a1, a2, a3, a4) => { await Task.Yield();  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0046: OnSuccessCombine on a successful awaitable Result of 4 value(s) with an asynchronous function should pass them in order and add the new value.")]
        public async Task RTO0046()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Success((1, 2, 3, 4))).OnSuccessCombine(async (a1, a2, a3, a4) => { await Task.Yield(); received = string.Join(",", new object[] { a1, a2, a3, a4 }); return Result<int, string>.Success(5); });

            // assert
            received.Should().Be("1,2,3,4");
            result.Should().SucceedWith((1, 2, 3, 4, 5));
        }

        [Fact(DisplayName = "RTO-0047: OnSuccessCombine on a failed awaitable Result of 4 value(s) with an asynchronous function should return the error and not run the step.")]
        public async Task RTO0047()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Failure("source failed")).OnSuccessCombine(async (a1, a2, a3, a4) => { await Task.Yield(); invoked = true; return Result<int, string>.Success(5); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0048: OnSuccessCombine on a successful awaitable Result of 4 value(s) with a failing asynchronous function should return the step's error.")]
        public async Task RTO0048()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Success((1, 2, 3, 4))).OnSuccessCombine(async (a1, a2, a3, a4) => { await Task.Yield();  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0049: OnSuccessCombine on a successful Result of 5 value(s) with a function should pass them in order and add the new value.")]
        public void RTO0049()
        {
            // arrange
            string? received = null;

            // act
            var result = Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5)).OnSuccessCombine((a1, a2, a3, a4, a5) => { received = string.Join(",", new object[] { a1, a2, a3, a4, a5 }); return Result<int, string>.Success(6); });

            // assert
            received.Should().Be("1,2,3,4,5");
            result.Should().SucceedWith((1, 2, 3, 4, 5, 6));
        }

        [Fact(DisplayName = "RTO-0050: OnSuccessCombine on a failed Result of 5 value(s) with a function should return the error and not run the step.")]
        public void RTO0050()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<(int, int, int, int, int), string>.Failure("source failed").OnSuccessCombine((a1, a2, a3, a4, a5) => { invoked = true; return Result<int, string>.Success(6); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0051: OnSuccessCombine on a successful Result of 5 value(s) with a failing function should return the step's error.")]
        public void RTO0051()
        {
            // act
            var result = Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5)).OnSuccessCombine((a1, a2, a3, a4, a5) => {  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0052: OnSuccessCombine on a successful awaitable Result of 5 value(s) with a function should pass them in order and add the new value.")]
        public async Task RTO0052()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5))).OnSuccessCombine((a1, a2, a3, a4, a5) => { received = string.Join(",", new object[] { a1, a2, a3, a4, a5 }); return Result<int, string>.Success(6); });

            // assert
            received.Should().Be("1,2,3,4,5");
            result.Should().SucceedWith((1, 2, 3, 4, 5, 6));
        }

        [Fact(DisplayName = "RTO-0053: OnSuccessCombine on a failed awaitable Result of 5 value(s) with a function should return the error and not run the step.")]
        public async Task RTO0053()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Failure("source failed")).OnSuccessCombine((a1, a2, a3, a4, a5) => { invoked = true; return Result<int, string>.Success(6); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0054: OnSuccessCombine on a successful awaitable Result of 5 value(s) with a failing function should return the step's error.")]
        public async Task RTO0054()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5))).OnSuccessCombine((a1, a2, a3, a4, a5) => {  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0055: OnSuccessCombine on a successful Result of 5 value(s) with an asynchronous function should pass them in order and add the new value.")]
        public async Task RTO0055()
        {
            // arrange
            string? received = null;

            // act
            var result = await Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5)).OnSuccessCombine(async (a1, a2, a3, a4, a5) => { await Task.Yield(); received = string.Join(",", new object[] { a1, a2, a3, a4, a5 }); return Result<int, string>.Success(6); });

            // assert
            received.Should().Be("1,2,3,4,5");
            result.Should().SucceedWith((1, 2, 3, 4, 5, 6));
        }

        [Fact(DisplayName = "RTO-0056: OnSuccessCombine on a failed Result of 5 value(s) with an asynchronous function should return the error and not run the step.")]
        public async Task RTO0056()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<(int, int, int, int, int), string>.Failure("source failed").OnSuccessCombine(async (a1, a2, a3, a4, a5) => { await Task.Yield(); invoked = true; return Result<int, string>.Success(6); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0057: OnSuccessCombine on a successful Result of 5 value(s) with a failing asynchronous function should return the step's error.")]
        public async Task RTO0057()
        {
            // act
            var result = await Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5)).OnSuccessCombine(async (a1, a2, a3, a4, a5) => { await Task.Yield();  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0058: OnSuccessCombine on a successful awaitable Result of 5 value(s) with an asynchronous function should pass them in order and add the new value.")]
        public async Task RTO0058()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5))).OnSuccessCombine(async (a1, a2, a3, a4, a5) => { await Task.Yield(); received = string.Join(",", new object[] { a1, a2, a3, a4, a5 }); return Result<int, string>.Success(6); });

            // assert
            received.Should().Be("1,2,3,4,5");
            result.Should().SucceedWith((1, 2, 3, 4, 5, 6));
        }

        [Fact(DisplayName = "RTO-0059: OnSuccessCombine on a failed awaitable Result of 5 value(s) with an asynchronous function should return the error and not run the step.")]
        public async Task RTO0059()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Failure("source failed")).OnSuccessCombine(async (a1, a2, a3, a4, a5) => { await Task.Yield(); invoked = true; return Result<int, string>.Success(6); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0060: OnSuccessCombine on a successful awaitable Result of 5 value(s) with a failing asynchronous function should return the step's error.")]
        public async Task RTO0060()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5))).OnSuccessCombine(async (a1, a2, a3, a4, a5) => { await Task.Yield();  return Result<int, string>.Failure("step failed"); });

            // assert
            result.Should().FailWith("step failed");
        }

        [Fact(DisplayName = "RTO-0061: OnSuccessMap on a successful Result of 2 values with a function should pass the values in order.")]
        public void RTO0061()
        {
            // act
            var result = Result<(int, int), string>.Success((1, 2)).OnSuccessMap((a1, a2) => {  return string.Join(",", new object[] { a1, a2 }); });

            // assert
            result.Should().SucceedWith("1,2");
        }

        [Fact(DisplayName = "RTO-0062: OnSuccessMap on a failed Result of 2 values with a function should return the error and not map.")]
        public void RTO0062()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<(int, int), string>.Failure("source failed").OnSuccessMap((a1, a2) => { invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0063: OnSuccessBind on a successful Result of 2 values with a function should pass the values in order and return the bound Result.")]
        public void RTO0063()
        {
            // act
            var result = Result<(int, int), string>.Success((1, 2)).OnSuccessBind((a1, a2) => {  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2 })); });

            // assert
            result.Should().SucceedWith("1,2");
        }

        [Fact(DisplayName = "RTO-0064: OnSuccessBind on a failed Result of 2 values with a function should return the error and not bind.")]
        public void RTO0064()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<(int, int), string>.Failure("source failed").OnSuccessBind((a1, a2) => { invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0065: OnSuccessBind on a successful Result of 2 values with a failing function should return the bound error.")]
        public void RTO0065()
        {
            // act
            var result = Result<(int, int), string>.Success((1, 2)).OnSuccessBind((a1, a2) => {  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0066: OnSuccessDo on a successful Result of 2 values with an action should pass the values in order and return the source.")]
        public void RTO0066()
        {
            // arrange
            string? received = null;

            // act
            var result = Result<(int, int), string>.Success((1, 2)).OnSuccessDo((a1, a2) => { received = string.Join(",", new object[] { a1, a2 }); });

            // assert
            received.Should().Be("1,2");
            result.Should().SucceedWith((1, 2));
        }

        [Fact(DisplayName = "RTO-0067: OnSuccessDo on a failed Result of 2 values with an action should not run the action and return the source.")]
        public void RTO0067()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<(int, int), string>.Failure("source failed").OnSuccessDo((a1, a2) => { invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0068: OnSuccessMap on a successful awaitable Result of 2 values with a function should pass the values in order.")]
        public async Task RTO0068()
        {
            // act
            var result = await Task.FromResult(Result<(int, int), string>.Success((1, 2))).OnSuccessMap((a1, a2) => {  return string.Join(",", new object[] { a1, a2 }); });

            // assert
            result.Should().SucceedWith("1,2");
        }

        [Fact(DisplayName = "RTO-0069: OnSuccessMap on a failed awaitable Result of 2 values with a function should return the error and not map.")]
        public async Task RTO0069()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int), string>.Failure("source failed")).OnSuccessMap((a1, a2) => { invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0070: OnSuccessBind on a successful awaitable Result of 2 values with a function should pass the values in order and return the bound Result.")]
        public async Task RTO0070()
        {
            // act
            var result = await Task.FromResult(Result<(int, int), string>.Success((1, 2))).OnSuccessBind((a1, a2) => {  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2 })); });

            // assert
            result.Should().SucceedWith("1,2");
        }

        [Fact(DisplayName = "RTO-0071: OnSuccessBind on a failed awaitable Result of 2 values with a function should return the error and not bind.")]
        public async Task RTO0071()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int), string>.Failure("source failed")).OnSuccessBind((a1, a2) => { invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0072: OnSuccessBind on a successful awaitable Result of 2 values with a failing function should return the bound error.")]
        public async Task RTO0072()
        {
            // act
            var result = await Task.FromResult(Result<(int, int), string>.Success((1, 2))).OnSuccessBind((a1, a2) => {  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0073: OnSuccessDo on a successful awaitable Result of 2 values with an action should pass the values in order and return the source.")]
        public async Task RTO0073()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<(int, int), string>.Success((1, 2))).OnSuccessDo((a1, a2) => { received = string.Join(",", new object[] { a1, a2 }); });

            // assert
            received.Should().Be("1,2");
            result.Should().SucceedWith((1, 2));
        }

        [Fact(DisplayName = "RTO-0074: OnSuccessDo on a failed awaitable Result of 2 values with an action should not run the action and return the source.")]
        public async Task RTO0074()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int), string>.Failure("source failed")).OnSuccessDo((a1, a2) => { invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0075: OnSuccessMap on a successful Result of 2 values with an asynchronous function should pass the values in order.")]
        public async Task RTO0075()
        {
            // act
            var result = await Result<(int, int), string>.Success((1, 2)).OnSuccessMap(async (a1, a2) => { await Task.Yield();  return string.Join(",", new object[] { a1, a2 }); });

            // assert
            result.Should().SucceedWith("1,2");
        }

        [Fact(DisplayName = "RTO-0076: OnSuccessMap on a failed Result of 2 values with an asynchronous function should return the error and not map.")]
        public async Task RTO0076()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<(int, int), string>.Failure("source failed").OnSuccessMap(async (a1, a2) => { await Task.Yield(); invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0077: OnSuccessBind on a successful Result of 2 values with an asynchronous function should pass the values in order and return the bound Result.")]
        public async Task RTO0077()
        {
            // act
            var result = await Result<(int, int), string>.Success((1, 2)).OnSuccessBind(async (a1, a2) => { await Task.Yield();  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2 })); });

            // assert
            result.Should().SucceedWith("1,2");
        }

        [Fact(DisplayName = "RTO-0078: OnSuccessBind on a failed Result of 2 values with an asynchronous function should return the error and not bind.")]
        public async Task RTO0078()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<(int, int), string>.Failure("source failed").OnSuccessBind(async (a1, a2) => { await Task.Yield(); invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0079: OnSuccessBind on a successful Result of 2 values with a failing asynchronous function should return the bound error.")]
        public async Task RTO0079()
        {
            // act
            var result = await Result<(int, int), string>.Success((1, 2)).OnSuccessBind(async (a1, a2) => { await Task.Yield();  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0080: OnSuccessDo on a successful Result of 2 values with an asynchronous action should pass the values in order and return the source.")]
        public async Task RTO0080()
        {
            // arrange
            string? received = null;

            // act
            var result = await Result<(int, int), string>.Success((1, 2)).OnSuccessDo(async (a1, a2) => { await Task.Delay(20); received = string.Join(",", new object[] { a1, a2 }); });

            // assert
            received.Should().Be("1,2");
            result.Should().SucceedWith((1, 2));
        }

        [Fact(DisplayName = "RTO-0081: OnSuccessDo on a failed Result of 2 values with an asynchronous action should not run the action and return the source.")]
        public async Task RTO0081()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<(int, int), string>.Failure("source failed").OnSuccessDo(async (a1, a2) => { await Task.Yield(); invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0082: OnSuccessMap on a successful awaitable Result of 2 values with an asynchronous function should pass the values in order.")]
        public async Task RTO0082()
        {
            // act
            var result = await Task.FromResult(Result<(int, int), string>.Success((1, 2))).OnSuccessMap(async (a1, a2) => { await Task.Yield();  return string.Join(",", new object[] { a1, a2 }); });

            // assert
            result.Should().SucceedWith("1,2");
        }

        [Fact(DisplayName = "RTO-0083: OnSuccessMap on a failed awaitable Result of 2 values with an asynchronous function should return the error and not map.")]
        public async Task RTO0083()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int), string>.Failure("source failed")).OnSuccessMap(async (a1, a2) => { await Task.Yield(); invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0084: OnSuccessBind on a successful awaitable Result of 2 values with an asynchronous function should pass the values in order and return the bound Result.")]
        public async Task RTO0084()
        {
            // act
            var result = await Task.FromResult(Result<(int, int), string>.Success((1, 2))).OnSuccessBind(async (a1, a2) => { await Task.Yield();  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2 })); });

            // assert
            result.Should().SucceedWith("1,2");
        }

        [Fact(DisplayName = "RTO-0085: OnSuccessBind on a failed awaitable Result of 2 values with an asynchronous function should return the error and not bind.")]
        public async Task RTO0085()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int), string>.Failure("source failed")).OnSuccessBind(async (a1, a2) => { await Task.Yield(); invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0086: OnSuccessBind on a successful awaitable Result of 2 values with a failing asynchronous function should return the bound error.")]
        public async Task RTO0086()
        {
            // act
            var result = await Task.FromResult(Result<(int, int), string>.Success((1, 2))).OnSuccessBind(async (a1, a2) => { await Task.Yield();  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0087: OnSuccessDo on a successful awaitable Result of 2 values with an asynchronous action should pass the values in order and return the source.")]
        public async Task RTO0087()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<(int, int), string>.Success((1, 2))).OnSuccessDo(async (a1, a2) => { await Task.Delay(20); received = string.Join(",", new object[] { a1, a2 }); });

            // assert
            received.Should().Be("1,2");
            result.Should().SucceedWith((1, 2));
        }

        [Fact(DisplayName = "RTO-0088: OnSuccessDo on a failed awaitable Result of 2 values with an asynchronous action should not run the action and return the source.")]
        public async Task RTO0088()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int), string>.Failure("source failed")).OnSuccessDo(async (a1, a2) => { await Task.Yield(); invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0089: Match on a successful Result of 2 values should run onSuccess with the values in order.")]
        public void RTO0089()
        {
            // act
            var text = Result<(int, int), string>.Success((1, 2)).Match((a1, a2) => string.Join(",", new object[] { a1, a2 }), error => "error: " + error);

            // assert
            text.Should().Be("1,2");
        }

        [Fact(DisplayName = "RTO-0090: Match on a failed Result of 2 values should run onError.")]
        public void RTO0090()
        {
            // act
            var text = Result<(int, int), string>.Failure("source failed").Match((a1, a2) => string.Join(",", new object[] { a1, a2 }), error => "error: " + error);

            // assert
            text.Should().Be("error: source failed");
        }

        [Fact(DisplayName = "RTO-0091: Match on a successful awaitable Result of 2 values should run onSuccess with the values in order.")]
        public async Task RTO0091()
        {
            // act
            var text = await Task.FromResult(Result<(int, int), string>.Success((1, 2))).Match((a1, a2) => string.Join(",", new object[] { a1, a2 }), error => "error: " + error);

            // assert
            text.Should().Be("1,2");
        }

        [Fact(DisplayName = "RTO-0092: Match on a failed awaitable Result of 2 values should run onError.")]
        public async Task RTO0092()
        {
            // act
            var text = await Task.FromResult(Result<(int, int), string>.Failure("source failed")).Match((a1, a2) => string.Join(",", new object[] { a1, a2 }), error => "error: " + error);

            // assert
            text.Should().Be("error: source failed");
        }

        [Fact(DisplayName = "RTO-0093: OnSuccessMap on a successful Result of 3 values with a function should pass the values in order.")]
        public void RTO0093()
        {
            // act
            var result = Result<(int, int, int), string>.Success((1, 2, 3)).OnSuccessMap((a1, a2, a3) => {  return string.Join(",", new object[] { a1, a2, a3 }); });

            // assert
            result.Should().SucceedWith("1,2,3");
        }

        [Fact(DisplayName = "RTO-0094: OnSuccessMap on a failed Result of 3 values with a function should return the error and not map.")]
        public void RTO0094()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<(int, int, int), string>.Failure("source failed").OnSuccessMap((a1, a2, a3) => { invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0095: OnSuccessBind on a successful Result of 3 values with a function should pass the values in order and return the bound Result.")]
        public void RTO0095()
        {
            // act
            var result = Result<(int, int, int), string>.Success((1, 2, 3)).OnSuccessBind((a1, a2, a3) => {  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2, a3 })); });

            // assert
            result.Should().SucceedWith("1,2,3");
        }

        [Fact(DisplayName = "RTO-0096: OnSuccessBind on a failed Result of 3 values with a function should return the error and not bind.")]
        public void RTO0096()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<(int, int, int), string>.Failure("source failed").OnSuccessBind((a1, a2, a3) => { invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0097: OnSuccessBind on a successful Result of 3 values with a failing function should return the bound error.")]
        public void RTO0097()
        {
            // act
            var result = Result<(int, int, int), string>.Success((1, 2, 3)).OnSuccessBind((a1, a2, a3) => {  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0098: OnSuccessDo on a successful Result of 3 values with an action should pass the values in order and return the source.")]
        public void RTO0098()
        {
            // arrange
            string? received = null;

            // act
            var result = Result<(int, int, int), string>.Success((1, 2, 3)).OnSuccessDo((a1, a2, a3) => { received = string.Join(",", new object[] { a1, a2, a3 }); });

            // assert
            received.Should().Be("1,2,3");
            result.Should().SucceedWith((1, 2, 3));
        }

        [Fact(DisplayName = "RTO-0099: OnSuccessDo on a failed Result of 3 values with an action should not run the action and return the source.")]
        public void RTO0099()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<(int, int, int), string>.Failure("source failed").OnSuccessDo((a1, a2, a3) => { invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0100: OnSuccessMap on a successful awaitable Result of 3 values with a function should pass the values in order.")]
        public async Task RTO0100()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Success((1, 2, 3))).OnSuccessMap((a1, a2, a3) => {  return string.Join(",", new object[] { a1, a2, a3 }); });

            // assert
            result.Should().SucceedWith("1,2,3");
        }

        [Fact(DisplayName = "RTO-0101: OnSuccessMap on a failed awaitable Result of 3 values with a function should return the error and not map.")]
        public async Task RTO0101()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Failure("source failed")).OnSuccessMap((a1, a2, a3) => { invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0102: OnSuccessBind on a successful awaitable Result of 3 values with a function should pass the values in order and return the bound Result.")]
        public async Task RTO0102()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Success((1, 2, 3))).OnSuccessBind((a1, a2, a3) => {  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2, a3 })); });

            // assert
            result.Should().SucceedWith("1,2,3");
        }

        [Fact(DisplayName = "RTO-0103: OnSuccessBind on a failed awaitable Result of 3 values with a function should return the error and not bind.")]
        public async Task RTO0103()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Failure("source failed")).OnSuccessBind((a1, a2, a3) => { invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0104: OnSuccessBind on a successful awaitable Result of 3 values with a failing function should return the bound error.")]
        public async Task RTO0104()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Success((1, 2, 3))).OnSuccessBind((a1, a2, a3) => {  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0105: OnSuccessDo on a successful awaitable Result of 3 values with an action should pass the values in order and return the source.")]
        public async Task RTO0105()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Success((1, 2, 3))).OnSuccessDo((a1, a2, a3) => { received = string.Join(",", new object[] { a1, a2, a3 }); });

            // assert
            received.Should().Be("1,2,3");
            result.Should().SucceedWith((1, 2, 3));
        }

        [Fact(DisplayName = "RTO-0106: OnSuccessDo on a failed awaitable Result of 3 values with an action should not run the action and return the source.")]
        public async Task RTO0106()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Failure("source failed")).OnSuccessDo((a1, a2, a3) => { invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0107: OnSuccessMap on a successful Result of 3 values with an asynchronous function should pass the values in order.")]
        public async Task RTO0107()
        {
            // act
            var result = await Result<(int, int, int), string>.Success((1, 2, 3)).OnSuccessMap(async (a1, a2, a3) => { await Task.Yield();  return string.Join(",", new object[] { a1, a2, a3 }); });

            // assert
            result.Should().SucceedWith("1,2,3");
        }

        [Fact(DisplayName = "RTO-0108: OnSuccessMap on a failed Result of 3 values with an asynchronous function should return the error and not map.")]
        public async Task RTO0108()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<(int, int, int), string>.Failure("source failed").OnSuccessMap(async (a1, a2, a3) => { await Task.Yield(); invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0109: OnSuccessBind on a successful Result of 3 values with an asynchronous function should pass the values in order and return the bound Result.")]
        public async Task RTO0109()
        {
            // act
            var result = await Result<(int, int, int), string>.Success((1, 2, 3)).OnSuccessBind(async (a1, a2, a3) => { await Task.Yield();  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2, a3 })); });

            // assert
            result.Should().SucceedWith("1,2,3");
        }

        [Fact(DisplayName = "RTO-0110: OnSuccessBind on a failed Result of 3 values with an asynchronous function should return the error and not bind.")]
        public async Task RTO0110()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<(int, int, int), string>.Failure("source failed").OnSuccessBind(async (a1, a2, a3) => { await Task.Yield(); invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0111: OnSuccessBind on a successful Result of 3 values with a failing asynchronous function should return the bound error.")]
        public async Task RTO0111()
        {
            // act
            var result = await Result<(int, int, int), string>.Success((1, 2, 3)).OnSuccessBind(async (a1, a2, a3) => { await Task.Yield();  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0112: OnSuccessDo on a successful Result of 3 values with an asynchronous action should pass the values in order and return the source.")]
        public async Task RTO0112()
        {
            // arrange
            string? received = null;

            // act
            var result = await Result<(int, int, int), string>.Success((1, 2, 3)).OnSuccessDo(async (a1, a2, a3) => { await Task.Delay(20); received = string.Join(",", new object[] { a1, a2, a3 }); });

            // assert
            received.Should().Be("1,2,3");
            result.Should().SucceedWith((1, 2, 3));
        }

        [Fact(DisplayName = "RTO-0113: OnSuccessDo on a failed Result of 3 values with an asynchronous action should not run the action and return the source.")]
        public async Task RTO0113()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<(int, int, int), string>.Failure("source failed").OnSuccessDo(async (a1, a2, a3) => { await Task.Yield(); invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0114: OnSuccessMap on a successful awaitable Result of 3 values with an asynchronous function should pass the values in order.")]
        public async Task RTO0114()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Success((1, 2, 3))).OnSuccessMap(async (a1, a2, a3) => { await Task.Yield();  return string.Join(",", new object[] { a1, a2, a3 }); });

            // assert
            result.Should().SucceedWith("1,2,3");
        }

        [Fact(DisplayName = "RTO-0115: OnSuccessMap on a failed awaitable Result of 3 values with an asynchronous function should return the error and not map.")]
        public async Task RTO0115()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Failure("source failed")).OnSuccessMap(async (a1, a2, a3) => { await Task.Yield(); invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0116: OnSuccessBind on a successful awaitable Result of 3 values with an asynchronous function should pass the values in order and return the bound Result.")]
        public async Task RTO0116()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Success((1, 2, 3))).OnSuccessBind(async (a1, a2, a3) => { await Task.Yield();  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2, a3 })); });

            // assert
            result.Should().SucceedWith("1,2,3");
        }

        [Fact(DisplayName = "RTO-0117: OnSuccessBind on a failed awaitable Result of 3 values with an asynchronous function should return the error and not bind.")]
        public async Task RTO0117()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Failure("source failed")).OnSuccessBind(async (a1, a2, a3) => { await Task.Yield(); invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0118: OnSuccessBind on a successful awaitable Result of 3 values with a failing asynchronous function should return the bound error.")]
        public async Task RTO0118()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Success((1, 2, 3))).OnSuccessBind(async (a1, a2, a3) => { await Task.Yield();  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0119: OnSuccessDo on a successful awaitable Result of 3 values with an asynchronous action should pass the values in order and return the source.")]
        public async Task RTO0119()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Success((1, 2, 3))).OnSuccessDo(async (a1, a2, a3) => { await Task.Delay(20); received = string.Join(",", new object[] { a1, a2, a3 }); });

            // assert
            received.Should().Be("1,2,3");
            result.Should().SucceedWith((1, 2, 3));
        }

        [Fact(DisplayName = "RTO-0120: OnSuccessDo on a failed awaitable Result of 3 values with an asynchronous action should not run the action and return the source.")]
        public async Task RTO0120()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int), string>.Failure("source failed")).OnSuccessDo(async (a1, a2, a3) => { await Task.Yield(); invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0121: Match on a successful Result of 3 values should run onSuccess with the values in order.")]
        public void RTO0121()
        {
            // act
            var text = Result<(int, int, int), string>.Success((1, 2, 3)).Match((a1, a2, a3) => string.Join(",", new object[] { a1, a2, a3 }), error => "error: " + error);

            // assert
            text.Should().Be("1,2,3");
        }

        [Fact(DisplayName = "RTO-0122: Match on a failed Result of 3 values should run onError.")]
        public void RTO0122()
        {
            // act
            var text = Result<(int, int, int), string>.Failure("source failed").Match((a1, a2, a3) => string.Join(",", new object[] { a1, a2, a3 }), error => "error: " + error);

            // assert
            text.Should().Be("error: source failed");
        }

        [Fact(DisplayName = "RTO-0123: Match on a successful awaitable Result of 3 values should run onSuccess with the values in order.")]
        public async Task RTO0123()
        {
            // act
            var text = await Task.FromResult(Result<(int, int, int), string>.Success((1, 2, 3))).Match((a1, a2, a3) => string.Join(",", new object[] { a1, a2, a3 }), error => "error: " + error);

            // assert
            text.Should().Be("1,2,3");
        }

        [Fact(DisplayName = "RTO-0124: Match on a failed awaitable Result of 3 values should run onError.")]
        public async Task RTO0124()
        {
            // act
            var text = await Task.FromResult(Result<(int, int, int), string>.Failure("source failed")).Match((a1, a2, a3) => string.Join(",", new object[] { a1, a2, a3 }), error => "error: " + error);

            // assert
            text.Should().Be("error: source failed");
        }

        [Fact(DisplayName = "RTO-0125: OnSuccessMap on a successful Result of 4 values with a function should pass the values in order.")]
        public void RTO0125()
        {
            // act
            var result = Result<(int, int, int, int), string>.Success((1, 2, 3, 4)).OnSuccessMap((a1, a2, a3, a4) => {  return string.Join(",", new object[] { a1, a2, a3, a4 }); });

            // assert
            result.Should().SucceedWith("1,2,3,4");
        }

        [Fact(DisplayName = "RTO-0126: OnSuccessMap on a failed Result of 4 values with a function should return the error and not map.")]
        public void RTO0126()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<(int, int, int, int), string>.Failure("source failed").OnSuccessMap((a1, a2, a3, a4) => { invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0127: OnSuccessBind on a successful Result of 4 values with a function should pass the values in order and return the bound Result.")]
        public void RTO0127()
        {
            // act
            var result = Result<(int, int, int, int), string>.Success((1, 2, 3, 4)).OnSuccessBind((a1, a2, a3, a4) => {  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2, a3, a4 })); });

            // assert
            result.Should().SucceedWith("1,2,3,4");
        }

        [Fact(DisplayName = "RTO-0128: OnSuccessBind on a failed Result of 4 values with a function should return the error and not bind.")]
        public void RTO0128()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<(int, int, int, int), string>.Failure("source failed").OnSuccessBind((a1, a2, a3, a4) => { invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0129: OnSuccessBind on a successful Result of 4 values with a failing function should return the bound error.")]
        public void RTO0129()
        {
            // act
            var result = Result<(int, int, int, int), string>.Success((1, 2, 3, 4)).OnSuccessBind((a1, a2, a3, a4) => {  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0130: OnSuccessDo on a successful Result of 4 values with an action should pass the values in order and return the source.")]
        public void RTO0130()
        {
            // arrange
            string? received = null;

            // act
            var result = Result<(int, int, int, int), string>.Success((1, 2, 3, 4)).OnSuccessDo((a1, a2, a3, a4) => { received = string.Join(",", new object[] { a1, a2, a3, a4 }); });

            // assert
            received.Should().Be("1,2,3,4");
            result.Should().SucceedWith((1, 2, 3, 4));
        }

        [Fact(DisplayName = "RTO-0131: OnSuccessDo on a failed Result of 4 values with an action should not run the action and return the source.")]
        public void RTO0131()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<(int, int, int, int), string>.Failure("source failed").OnSuccessDo((a1, a2, a3, a4) => { invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0132: OnSuccessMap on a successful awaitable Result of 4 values with a function should pass the values in order.")]
        public async Task RTO0132()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Success((1, 2, 3, 4))).OnSuccessMap((a1, a2, a3, a4) => {  return string.Join(",", new object[] { a1, a2, a3, a4 }); });

            // assert
            result.Should().SucceedWith("1,2,3,4");
        }

        [Fact(DisplayName = "RTO-0133: OnSuccessMap on a failed awaitable Result of 4 values with a function should return the error and not map.")]
        public async Task RTO0133()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Failure("source failed")).OnSuccessMap((a1, a2, a3, a4) => { invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0134: OnSuccessBind on a successful awaitable Result of 4 values with a function should pass the values in order and return the bound Result.")]
        public async Task RTO0134()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Success((1, 2, 3, 4))).OnSuccessBind((a1, a2, a3, a4) => {  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2, a3, a4 })); });

            // assert
            result.Should().SucceedWith("1,2,3,4");
        }

        [Fact(DisplayName = "RTO-0135: OnSuccessBind on a failed awaitable Result of 4 values with a function should return the error and not bind.")]
        public async Task RTO0135()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Failure("source failed")).OnSuccessBind((a1, a2, a3, a4) => { invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0136: OnSuccessBind on a successful awaitable Result of 4 values with a failing function should return the bound error.")]
        public async Task RTO0136()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Success((1, 2, 3, 4))).OnSuccessBind((a1, a2, a3, a4) => {  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0137: OnSuccessDo on a successful awaitable Result of 4 values with an action should pass the values in order and return the source.")]
        public async Task RTO0137()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Success((1, 2, 3, 4))).OnSuccessDo((a1, a2, a3, a4) => { received = string.Join(",", new object[] { a1, a2, a3, a4 }); });

            // assert
            received.Should().Be("1,2,3,4");
            result.Should().SucceedWith((1, 2, 3, 4));
        }

        [Fact(DisplayName = "RTO-0138: OnSuccessDo on a failed awaitable Result of 4 values with an action should not run the action and return the source.")]
        public async Task RTO0138()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Failure("source failed")).OnSuccessDo((a1, a2, a3, a4) => { invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0139: OnSuccessMap on a successful Result of 4 values with an asynchronous function should pass the values in order.")]
        public async Task RTO0139()
        {
            // act
            var result = await Result<(int, int, int, int), string>.Success((1, 2, 3, 4)).OnSuccessMap(async (a1, a2, a3, a4) => { await Task.Yield();  return string.Join(",", new object[] { a1, a2, a3, a4 }); });

            // assert
            result.Should().SucceedWith("1,2,3,4");
        }

        [Fact(DisplayName = "RTO-0140: OnSuccessMap on a failed Result of 4 values with an asynchronous function should return the error and not map.")]
        public async Task RTO0140()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<(int, int, int, int), string>.Failure("source failed").OnSuccessMap(async (a1, a2, a3, a4) => { await Task.Yield(); invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0141: OnSuccessBind on a successful Result of 4 values with an asynchronous function should pass the values in order and return the bound Result.")]
        public async Task RTO0141()
        {
            // act
            var result = await Result<(int, int, int, int), string>.Success((1, 2, 3, 4)).OnSuccessBind(async (a1, a2, a3, a4) => { await Task.Yield();  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2, a3, a4 })); });

            // assert
            result.Should().SucceedWith("1,2,3,4");
        }

        [Fact(DisplayName = "RTO-0142: OnSuccessBind on a failed Result of 4 values with an asynchronous function should return the error and not bind.")]
        public async Task RTO0142()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<(int, int, int, int), string>.Failure("source failed").OnSuccessBind(async (a1, a2, a3, a4) => { await Task.Yield(); invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0143: OnSuccessBind on a successful Result of 4 values with a failing asynchronous function should return the bound error.")]
        public async Task RTO0143()
        {
            // act
            var result = await Result<(int, int, int, int), string>.Success((1, 2, 3, 4)).OnSuccessBind(async (a1, a2, a3, a4) => { await Task.Yield();  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0144: OnSuccessDo on a successful Result of 4 values with an asynchronous action should pass the values in order and return the source.")]
        public async Task RTO0144()
        {
            // arrange
            string? received = null;

            // act
            var result = await Result<(int, int, int, int), string>.Success((1, 2, 3, 4)).OnSuccessDo(async (a1, a2, a3, a4) => { await Task.Delay(20); received = string.Join(",", new object[] { a1, a2, a3, a4 }); });

            // assert
            received.Should().Be("1,2,3,4");
            result.Should().SucceedWith((1, 2, 3, 4));
        }

        [Fact(DisplayName = "RTO-0145: OnSuccessDo on a failed Result of 4 values with an asynchronous action should not run the action and return the source.")]
        public async Task RTO0145()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<(int, int, int, int), string>.Failure("source failed").OnSuccessDo(async (a1, a2, a3, a4) => { await Task.Yield(); invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0146: OnSuccessMap on a successful awaitable Result of 4 values with an asynchronous function should pass the values in order.")]
        public async Task RTO0146()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Success((1, 2, 3, 4))).OnSuccessMap(async (a1, a2, a3, a4) => { await Task.Yield();  return string.Join(",", new object[] { a1, a2, a3, a4 }); });

            // assert
            result.Should().SucceedWith("1,2,3,4");
        }

        [Fact(DisplayName = "RTO-0147: OnSuccessMap on a failed awaitable Result of 4 values with an asynchronous function should return the error and not map.")]
        public async Task RTO0147()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Failure("source failed")).OnSuccessMap(async (a1, a2, a3, a4) => { await Task.Yield(); invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0148: OnSuccessBind on a successful awaitable Result of 4 values with an asynchronous function should pass the values in order and return the bound Result.")]
        public async Task RTO0148()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Success((1, 2, 3, 4))).OnSuccessBind(async (a1, a2, a3, a4) => { await Task.Yield();  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2, a3, a4 })); });

            // assert
            result.Should().SucceedWith("1,2,3,4");
        }

        [Fact(DisplayName = "RTO-0149: OnSuccessBind on a failed awaitable Result of 4 values with an asynchronous function should return the error and not bind.")]
        public async Task RTO0149()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Failure("source failed")).OnSuccessBind(async (a1, a2, a3, a4) => { await Task.Yield(); invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0150: OnSuccessBind on a successful awaitable Result of 4 values with a failing asynchronous function should return the bound error.")]
        public async Task RTO0150()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Success((1, 2, 3, 4))).OnSuccessBind(async (a1, a2, a3, a4) => { await Task.Yield();  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0151: OnSuccessDo on a successful awaitable Result of 4 values with an asynchronous action should pass the values in order and return the source.")]
        public async Task RTO0151()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Success((1, 2, 3, 4))).OnSuccessDo(async (a1, a2, a3, a4) => { await Task.Delay(20); received = string.Join(",", new object[] { a1, a2, a3, a4 }); });

            // assert
            received.Should().Be("1,2,3,4");
            result.Should().SucceedWith((1, 2, 3, 4));
        }

        [Fact(DisplayName = "RTO-0152: OnSuccessDo on a failed awaitable Result of 4 values with an asynchronous action should not run the action and return the source.")]
        public async Task RTO0152()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int), string>.Failure("source failed")).OnSuccessDo(async (a1, a2, a3, a4) => { await Task.Yield(); invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0153: Match on a successful Result of 4 values should run onSuccess with the values in order.")]
        public void RTO0153()
        {
            // act
            var text = Result<(int, int, int, int), string>.Success((1, 2, 3, 4)).Match((a1, a2, a3, a4) => string.Join(",", new object[] { a1, a2, a3, a4 }), error => "error: " + error);

            // assert
            text.Should().Be("1,2,3,4");
        }

        [Fact(DisplayName = "RTO-0154: Match on a failed Result of 4 values should run onError.")]
        public void RTO0154()
        {
            // act
            var text = Result<(int, int, int, int), string>.Failure("source failed").Match((a1, a2, a3, a4) => string.Join(",", new object[] { a1, a2, a3, a4 }), error => "error: " + error);

            // assert
            text.Should().Be("error: source failed");
        }

        [Fact(DisplayName = "RTO-0155: Match on a successful awaitable Result of 4 values should run onSuccess with the values in order.")]
        public async Task RTO0155()
        {
            // act
            var text = await Task.FromResult(Result<(int, int, int, int), string>.Success((1, 2, 3, 4))).Match((a1, a2, a3, a4) => string.Join(",", new object[] { a1, a2, a3, a4 }), error => "error: " + error);

            // assert
            text.Should().Be("1,2,3,4");
        }

        [Fact(DisplayName = "RTO-0156: Match on a failed awaitable Result of 4 values should run onError.")]
        public async Task RTO0156()
        {
            // act
            var text = await Task.FromResult(Result<(int, int, int, int), string>.Failure("source failed")).Match((a1, a2, a3, a4) => string.Join(",", new object[] { a1, a2, a3, a4 }), error => "error: " + error);

            // assert
            text.Should().Be("error: source failed");
        }

        [Fact(DisplayName = "RTO-0157: OnSuccessMap on a successful Result of 5 values with a function should pass the values in order.")]
        public void RTO0157()
        {
            // act
            var result = Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5)).OnSuccessMap((a1, a2, a3, a4, a5) => {  return string.Join(",", new object[] { a1, a2, a3, a4, a5 }); });

            // assert
            result.Should().SucceedWith("1,2,3,4,5");
        }

        [Fact(DisplayName = "RTO-0158: OnSuccessMap on a failed Result of 5 values with a function should return the error and not map.")]
        public void RTO0158()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<(int, int, int, int, int), string>.Failure("source failed").OnSuccessMap((a1, a2, a3, a4, a5) => { invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0159: OnSuccessBind on a successful Result of 5 values with a function should pass the values in order and return the bound Result.")]
        public void RTO0159()
        {
            // act
            var result = Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5)).OnSuccessBind((a1, a2, a3, a4, a5) => {  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2, a3, a4, a5 })); });

            // assert
            result.Should().SucceedWith("1,2,3,4,5");
        }

        [Fact(DisplayName = "RTO-0160: OnSuccessBind on a failed Result of 5 values with a function should return the error and not bind.")]
        public void RTO0160()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<(int, int, int, int, int), string>.Failure("source failed").OnSuccessBind((a1, a2, a3, a4, a5) => { invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0161: OnSuccessBind on a successful Result of 5 values with a failing function should return the bound error.")]
        public void RTO0161()
        {
            // act
            var result = Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5)).OnSuccessBind((a1, a2, a3, a4, a5) => {  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0162: OnSuccessDo on a successful Result of 5 values with an action should pass the values in order and return the source.")]
        public void RTO0162()
        {
            // arrange
            string? received = null;

            // act
            var result = Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5)).OnSuccessDo((a1, a2, a3, a4, a5) => { received = string.Join(",", new object[] { a1, a2, a3, a4, a5 }); });

            // assert
            received.Should().Be("1,2,3,4,5");
            result.Should().SucceedWith((1, 2, 3, 4, 5));
        }

        [Fact(DisplayName = "RTO-0163: OnSuccessDo on a failed Result of 5 values with an action should not run the action and return the source.")]
        public void RTO0163()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<(int, int, int, int, int), string>.Failure("source failed").OnSuccessDo((a1, a2, a3, a4, a5) => { invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0164: OnSuccessMap on a successful awaitable Result of 5 values with a function should pass the values in order.")]
        public async Task RTO0164()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5))).OnSuccessMap((a1, a2, a3, a4, a5) => {  return string.Join(",", new object[] { a1, a2, a3, a4, a5 }); });

            // assert
            result.Should().SucceedWith("1,2,3,4,5");
        }

        [Fact(DisplayName = "RTO-0165: OnSuccessMap on a failed awaitable Result of 5 values with a function should return the error and not map.")]
        public async Task RTO0165()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Failure("source failed")).OnSuccessMap((a1, a2, a3, a4, a5) => { invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0166: OnSuccessBind on a successful awaitable Result of 5 values with a function should pass the values in order and return the bound Result.")]
        public async Task RTO0166()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5))).OnSuccessBind((a1, a2, a3, a4, a5) => {  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2, a3, a4, a5 })); });

            // assert
            result.Should().SucceedWith("1,2,3,4,5");
        }

        [Fact(DisplayName = "RTO-0167: OnSuccessBind on a failed awaitable Result of 5 values with a function should return the error and not bind.")]
        public async Task RTO0167()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Failure("source failed")).OnSuccessBind((a1, a2, a3, a4, a5) => { invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0168: OnSuccessBind on a successful awaitable Result of 5 values with a failing function should return the bound error.")]
        public async Task RTO0168()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5))).OnSuccessBind((a1, a2, a3, a4, a5) => {  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0169: OnSuccessDo on a successful awaitable Result of 5 values with an action should pass the values in order and return the source.")]
        public async Task RTO0169()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5))).OnSuccessDo((a1, a2, a3, a4, a5) => { received = string.Join(",", new object[] { a1, a2, a3, a4, a5 }); });

            // assert
            received.Should().Be("1,2,3,4,5");
            result.Should().SucceedWith((1, 2, 3, 4, 5));
        }

        [Fact(DisplayName = "RTO-0170: OnSuccessDo on a failed awaitable Result of 5 values with an action should not run the action and return the source.")]
        public async Task RTO0170()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Failure("source failed")).OnSuccessDo((a1, a2, a3, a4, a5) => { invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0171: OnSuccessMap on a successful Result of 5 values with an asynchronous function should pass the values in order.")]
        public async Task RTO0171()
        {
            // act
            var result = await Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5)).OnSuccessMap(async (a1, a2, a3, a4, a5) => { await Task.Yield();  return string.Join(",", new object[] { a1, a2, a3, a4, a5 }); });

            // assert
            result.Should().SucceedWith("1,2,3,4,5");
        }

        [Fact(DisplayName = "RTO-0172: OnSuccessMap on a failed Result of 5 values with an asynchronous function should return the error and not map.")]
        public async Task RTO0172()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<(int, int, int, int, int), string>.Failure("source failed").OnSuccessMap(async (a1, a2, a3, a4, a5) => { await Task.Yield(); invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0173: OnSuccessBind on a successful Result of 5 values with an asynchronous function should pass the values in order and return the bound Result.")]
        public async Task RTO0173()
        {
            // act
            var result = await Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5)).OnSuccessBind(async (a1, a2, a3, a4, a5) => { await Task.Yield();  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2, a3, a4, a5 })); });

            // assert
            result.Should().SucceedWith("1,2,3,4,5");
        }

        [Fact(DisplayName = "RTO-0174: OnSuccessBind on a failed Result of 5 values with an asynchronous function should return the error and not bind.")]
        public async Task RTO0174()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<(int, int, int, int, int), string>.Failure("source failed").OnSuccessBind(async (a1, a2, a3, a4, a5) => { await Task.Yield(); invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0175: OnSuccessBind on a successful Result of 5 values with a failing asynchronous function should return the bound error.")]
        public async Task RTO0175()
        {
            // act
            var result = await Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5)).OnSuccessBind(async (a1, a2, a3, a4, a5) => { await Task.Yield();  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0176: OnSuccessDo on a successful Result of 5 values with an asynchronous action should pass the values in order and return the source.")]
        public async Task RTO0176()
        {
            // arrange
            string? received = null;

            // act
            var result = await Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5)).OnSuccessDo(async (a1, a2, a3, a4, a5) => { await Task.Delay(20); received = string.Join(",", new object[] { a1, a2, a3, a4, a5 }); });

            // assert
            received.Should().Be("1,2,3,4,5");
            result.Should().SucceedWith((1, 2, 3, 4, 5));
        }

        [Fact(DisplayName = "RTO-0177: OnSuccessDo on a failed Result of 5 values with an asynchronous action should not run the action and return the source.")]
        public async Task RTO0177()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<(int, int, int, int, int), string>.Failure("source failed").OnSuccessDo(async (a1, a2, a3, a4, a5) => { await Task.Yield(); invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0178: OnSuccessMap on a successful awaitable Result of 5 values with an asynchronous function should pass the values in order.")]
        public async Task RTO0178()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5))).OnSuccessMap(async (a1, a2, a3, a4, a5) => { await Task.Yield();  return string.Join(",", new object[] { a1, a2, a3, a4, a5 }); });

            // assert
            result.Should().SucceedWith("1,2,3,4,5");
        }

        [Fact(DisplayName = "RTO-0179: OnSuccessMap on a failed awaitable Result of 5 values with an asynchronous function should return the error and not map.")]
        public async Task RTO0179()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Failure("source failed")).OnSuccessMap(async (a1, a2, a3, a4, a5) => { await Task.Yield(); invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0180: OnSuccessBind on a successful awaitable Result of 5 values with an asynchronous function should pass the values in order and return the bound Result.")]
        public async Task RTO0180()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5))).OnSuccessBind(async (a1, a2, a3, a4, a5) => { await Task.Yield();  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2, a3, a4, a5 })); });

            // assert
            result.Should().SucceedWith("1,2,3,4,5");
        }

        [Fact(DisplayName = "RTO-0181: OnSuccessBind on a failed awaitable Result of 5 values with an asynchronous function should return the error and not bind.")]
        public async Task RTO0181()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Failure("source failed")).OnSuccessBind(async (a1, a2, a3, a4, a5) => { await Task.Yield(); invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0182: OnSuccessBind on a successful awaitable Result of 5 values with a failing asynchronous function should return the bound error.")]
        public async Task RTO0182()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5))).OnSuccessBind(async (a1, a2, a3, a4, a5) => { await Task.Yield();  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0183: OnSuccessDo on a successful awaitable Result of 5 values with an asynchronous action should pass the values in order and return the source.")]
        public async Task RTO0183()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5))).OnSuccessDo(async (a1, a2, a3, a4, a5) => { await Task.Delay(20); received = string.Join(",", new object[] { a1, a2, a3, a4, a5 }); });

            // assert
            received.Should().Be("1,2,3,4,5");
            result.Should().SucceedWith((1, 2, 3, 4, 5));
        }

        [Fact(DisplayName = "RTO-0184: OnSuccessDo on a failed awaitable Result of 5 values with an asynchronous action should not run the action and return the source.")]
        public async Task RTO0184()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int), string>.Failure("source failed")).OnSuccessDo(async (a1, a2, a3, a4, a5) => { await Task.Yield(); invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0185: Match on a successful Result of 5 values should run onSuccess with the values in order.")]
        public void RTO0185()
        {
            // act
            var text = Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5)).Match((a1, a2, a3, a4, a5) => string.Join(",", new object[] { a1, a2, a3, a4, a5 }), error => "error: " + error);

            // assert
            text.Should().Be("1,2,3,4,5");
        }

        [Fact(DisplayName = "RTO-0186: Match on a failed Result of 5 values should run onError.")]
        public void RTO0186()
        {
            // act
            var text = Result<(int, int, int, int, int), string>.Failure("source failed").Match((a1, a2, a3, a4, a5) => string.Join(",", new object[] { a1, a2, a3, a4, a5 }), error => "error: " + error);

            // assert
            text.Should().Be("error: source failed");
        }

        [Fact(DisplayName = "RTO-0187: Match on a successful awaitable Result of 5 values should run onSuccess with the values in order.")]
        public async Task RTO0187()
        {
            // act
            var text = await Task.FromResult(Result<(int, int, int, int, int), string>.Success((1, 2, 3, 4, 5))).Match((a1, a2, a3, a4, a5) => string.Join(",", new object[] { a1, a2, a3, a4, a5 }), error => "error: " + error);

            // assert
            text.Should().Be("1,2,3,4,5");
        }

        [Fact(DisplayName = "RTO-0188: Match on a failed awaitable Result of 5 values should run onError.")]
        public async Task RTO0188()
        {
            // act
            var text = await Task.FromResult(Result<(int, int, int, int, int), string>.Failure("source failed")).Match((a1, a2, a3, a4, a5) => string.Join(",", new object[] { a1, a2, a3, a4, a5 }), error => "error: " + error);

            // assert
            text.Should().Be("error: source failed");
        }

        [Fact(DisplayName = "RTO-0189: OnSuccessMap on a successful Result of 6 values with a function should pass the values in order.")]
        public void RTO0189()
        {
            // act
            var result = Result<(int, int, int, int, int, int), string>.Success((1, 2, 3, 4, 5, 6)).OnSuccessMap((a1, a2, a3, a4, a5, a6) => {  return string.Join(",", new object[] { a1, a2, a3, a4, a5, a6 }); });

            // assert
            result.Should().SucceedWith("1,2,3,4,5,6");
        }

        [Fact(DisplayName = "RTO-0190: OnSuccessMap on a failed Result of 6 values with a function should return the error and not map.")]
        public void RTO0190()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<(int, int, int, int, int, int), string>.Failure("source failed").OnSuccessMap((a1, a2, a3, a4, a5, a6) => { invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0191: OnSuccessBind on a successful Result of 6 values with a function should pass the values in order and return the bound Result.")]
        public void RTO0191()
        {
            // act
            var result = Result<(int, int, int, int, int, int), string>.Success((1, 2, 3, 4, 5, 6)).OnSuccessBind((a1, a2, a3, a4, a5, a6) => {  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2, a3, a4, a5, a6 })); });

            // assert
            result.Should().SucceedWith("1,2,3,4,5,6");
        }

        [Fact(DisplayName = "RTO-0192: OnSuccessBind on a failed Result of 6 values with a function should return the error and not bind.")]
        public void RTO0192()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<(int, int, int, int, int, int), string>.Failure("source failed").OnSuccessBind((a1, a2, a3, a4, a5, a6) => { invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0193: OnSuccessBind on a successful Result of 6 values with a failing function should return the bound error.")]
        public void RTO0193()
        {
            // act
            var result = Result<(int, int, int, int, int, int), string>.Success((1, 2, 3, 4, 5, 6)).OnSuccessBind((a1, a2, a3, a4, a5, a6) => {  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0194: OnSuccessDo on a successful Result of 6 values with an action should pass the values in order and return the source.")]
        public void RTO0194()
        {
            // arrange
            string? received = null;

            // act
            var result = Result<(int, int, int, int, int, int), string>.Success((1, 2, 3, 4, 5, 6)).OnSuccessDo((a1, a2, a3, a4, a5, a6) => { received = string.Join(",", new object[] { a1, a2, a3, a4, a5, a6 }); });

            // assert
            received.Should().Be("1,2,3,4,5,6");
            result.Should().SucceedWith((1, 2, 3, 4, 5, 6));
        }

        [Fact(DisplayName = "RTO-0195: OnSuccessDo on a failed Result of 6 values with an action should not run the action and return the source.")]
        public void RTO0195()
        {
            // arrange
            var invoked = false;

            // act
            var result = Result<(int, int, int, int, int, int), string>.Failure("source failed").OnSuccessDo((a1, a2, a3, a4, a5, a6) => { invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0196: OnSuccessMap on a successful awaitable Result of 6 values with a function should pass the values in order.")]
        public async Task RTO0196()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int, int), string>.Success((1, 2, 3, 4, 5, 6))).OnSuccessMap((a1, a2, a3, a4, a5, a6) => {  return string.Join(",", new object[] { a1, a2, a3, a4, a5, a6 }); });

            // assert
            result.Should().SucceedWith("1,2,3,4,5,6");
        }

        [Fact(DisplayName = "RTO-0197: OnSuccessMap on a failed awaitable Result of 6 values with a function should return the error and not map.")]
        public async Task RTO0197()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int, int), string>.Failure("source failed")).OnSuccessMap((a1, a2, a3, a4, a5, a6) => { invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0198: OnSuccessBind on a successful awaitable Result of 6 values with a function should pass the values in order and return the bound Result.")]
        public async Task RTO0198()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int, int), string>.Success((1, 2, 3, 4, 5, 6))).OnSuccessBind((a1, a2, a3, a4, a5, a6) => {  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2, a3, a4, a5, a6 })); });

            // assert
            result.Should().SucceedWith("1,2,3,4,5,6");
        }

        [Fact(DisplayName = "RTO-0199: OnSuccessBind on a failed awaitable Result of 6 values with a function should return the error and not bind.")]
        public async Task RTO0199()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int, int), string>.Failure("source failed")).OnSuccessBind((a1, a2, a3, a4, a5, a6) => { invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0200: OnSuccessBind on a successful awaitable Result of 6 values with a failing function should return the bound error.")]
        public async Task RTO0200()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int, int), string>.Success((1, 2, 3, 4, 5, 6))).OnSuccessBind((a1, a2, a3, a4, a5, a6) => {  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0201: OnSuccessDo on a successful awaitable Result of 6 values with an action should pass the values in order and return the source.")]
        public async Task RTO0201()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int, int), string>.Success((1, 2, 3, 4, 5, 6))).OnSuccessDo((a1, a2, a3, a4, a5, a6) => { received = string.Join(",", new object[] { a1, a2, a3, a4, a5, a6 }); });

            // assert
            received.Should().Be("1,2,3,4,5,6");
            result.Should().SucceedWith((1, 2, 3, 4, 5, 6));
        }

        [Fact(DisplayName = "RTO-0202: OnSuccessDo on a failed awaitable Result of 6 values with an action should not run the action and return the source.")]
        public async Task RTO0202()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int, int), string>.Failure("source failed")).OnSuccessDo((a1, a2, a3, a4, a5, a6) => { invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0203: OnSuccessMap on a successful Result of 6 values with an asynchronous function should pass the values in order.")]
        public async Task RTO0203()
        {
            // act
            var result = await Result<(int, int, int, int, int, int), string>.Success((1, 2, 3, 4, 5, 6)).OnSuccessMap(async (a1, a2, a3, a4, a5, a6) => { await Task.Yield();  return string.Join(",", new object[] { a1, a2, a3, a4, a5, a6 }); });

            // assert
            result.Should().SucceedWith("1,2,3,4,5,6");
        }

        [Fact(DisplayName = "RTO-0204: OnSuccessMap on a failed Result of 6 values with an asynchronous function should return the error and not map.")]
        public async Task RTO0204()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<(int, int, int, int, int, int), string>.Failure("source failed").OnSuccessMap(async (a1, a2, a3, a4, a5, a6) => { await Task.Yield(); invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0205: OnSuccessBind on a successful Result of 6 values with an asynchronous function should pass the values in order and return the bound Result.")]
        public async Task RTO0205()
        {
            // act
            var result = await Result<(int, int, int, int, int, int), string>.Success((1, 2, 3, 4, 5, 6)).OnSuccessBind(async (a1, a2, a3, a4, a5, a6) => { await Task.Yield();  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2, a3, a4, a5, a6 })); });

            // assert
            result.Should().SucceedWith("1,2,3,4,5,6");
        }

        [Fact(DisplayName = "RTO-0206: OnSuccessBind on a failed Result of 6 values with an asynchronous function should return the error and not bind.")]
        public async Task RTO0206()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<(int, int, int, int, int, int), string>.Failure("source failed").OnSuccessBind(async (a1, a2, a3, a4, a5, a6) => { await Task.Yield(); invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0207: OnSuccessBind on a successful Result of 6 values with a failing asynchronous function should return the bound error.")]
        public async Task RTO0207()
        {
            // act
            var result = await Result<(int, int, int, int, int, int), string>.Success((1, 2, 3, 4, 5, 6)).OnSuccessBind(async (a1, a2, a3, a4, a5, a6) => { await Task.Yield();  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0208: OnSuccessDo on a successful Result of 6 values with an asynchronous action should pass the values in order and return the source.")]
        public async Task RTO0208()
        {
            // arrange
            string? received = null;

            // act
            var result = await Result<(int, int, int, int, int, int), string>.Success((1, 2, 3, 4, 5, 6)).OnSuccessDo(async (a1, a2, a3, a4, a5, a6) => { await Task.Delay(20); received = string.Join(",", new object[] { a1, a2, a3, a4, a5, a6 }); });

            // assert
            received.Should().Be("1,2,3,4,5,6");
            result.Should().SucceedWith((1, 2, 3, 4, 5, 6));
        }

        [Fact(DisplayName = "RTO-0209: OnSuccessDo on a failed Result of 6 values with an asynchronous action should not run the action and return the source.")]
        public async Task RTO0209()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Result<(int, int, int, int, int, int), string>.Failure("source failed").OnSuccessDo(async (a1, a2, a3, a4, a5, a6) => { await Task.Yield(); invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0210: OnSuccessMap on a successful awaitable Result of 6 values with an asynchronous function should pass the values in order.")]
        public async Task RTO0210()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int, int), string>.Success((1, 2, 3, 4, 5, 6))).OnSuccessMap(async (a1, a2, a3, a4, a5, a6) => { await Task.Yield();  return string.Join(",", new object[] { a1, a2, a3, a4, a5, a6 }); });

            // assert
            result.Should().SucceedWith("1,2,3,4,5,6");
        }

        [Fact(DisplayName = "RTO-0211: OnSuccessMap on a failed awaitable Result of 6 values with an asynchronous function should return the error and not map.")]
        public async Task RTO0211()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int, int), string>.Failure("source failed")).OnSuccessMap(async (a1, a2, a3, a4, a5, a6) => { await Task.Yield(); invoked = true; return 0; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0212: OnSuccessBind on a successful awaitable Result of 6 values with an asynchronous function should pass the values in order and return the bound Result.")]
        public async Task RTO0212()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int, int), string>.Success((1, 2, 3, 4, 5, 6))).OnSuccessBind(async (a1, a2, a3, a4, a5, a6) => { await Task.Yield();  return Result<string, string>.Success(string.Join(",", new object[] { a1, a2, a3, a4, a5, a6 })); });

            // assert
            result.Should().SucceedWith("1,2,3,4,5,6");
        }

        [Fact(DisplayName = "RTO-0213: OnSuccessBind on a failed awaitable Result of 6 values with an asynchronous function should return the error and not bind.")]
        public async Task RTO0213()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int, int), string>.Failure("source failed")).OnSuccessBind(async (a1, a2, a3, a4, a5, a6) => { await Task.Yield(); invoked = true; return Result<string, string>.Success(""); });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0214: OnSuccessBind on a successful awaitable Result of 6 values with a failing asynchronous function should return the bound error.")]
        public async Task RTO0214()
        {
            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int, int), string>.Success((1, 2, 3, 4, 5, 6))).OnSuccessBind(async (a1, a2, a3, a4, a5, a6) => { await Task.Yield();  return Result<string, string>.Failure("bind failed"); });

            // assert
            result.Should().FailWith("bind failed");
        }

        [Fact(DisplayName = "RTO-0215: OnSuccessDo on a successful awaitable Result of 6 values with an asynchronous action should pass the values in order and return the source.")]
        public async Task RTO0215()
        {
            // arrange
            string? received = null;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int, int), string>.Success((1, 2, 3, 4, 5, 6))).OnSuccessDo(async (a1, a2, a3, a4, a5, a6) => { await Task.Delay(20); received = string.Join(",", new object[] { a1, a2, a3, a4, a5, a6 }); });

            // assert
            received.Should().Be("1,2,3,4,5,6");
            result.Should().SucceedWith((1, 2, 3, 4, 5, 6));
        }

        [Fact(DisplayName = "RTO-0216: OnSuccessDo on a failed awaitable Result of 6 values with an asynchronous action should not run the action and return the source.")]
        public async Task RTO0216()
        {
            // arrange
            var invoked = false;

            // act
            var result = await Task.FromResult(Result<(int, int, int, int, int, int), string>.Failure("source failed")).OnSuccessDo(async (a1, a2, a3, a4, a5, a6) => { await Task.Yield(); invoked = true; });

            // assert
            invoked.Should().BeFalse();
            result.Should().FailWith("source failed");
        }

        [Fact(DisplayName = "RTO-0217: Match on a successful Result of 6 values should run onSuccess with the values in order.")]
        public void RTO0217()
        {
            // act
            var text = Result<(int, int, int, int, int, int), string>.Success((1, 2, 3, 4, 5, 6)).Match((a1, a2, a3, a4, a5, a6) => string.Join(",", new object[] { a1, a2, a3, a4, a5, a6 }), error => "error: " + error);

            // assert
            text.Should().Be("1,2,3,4,5,6");
        }

        [Fact(DisplayName = "RTO-0218: Match on a failed Result of 6 values should run onError.")]
        public void RTO0218()
        {
            // act
            var text = Result<(int, int, int, int, int, int), string>.Failure("source failed").Match((a1, a2, a3, a4, a5, a6) => string.Join(",", new object[] { a1, a2, a3, a4, a5, a6 }), error => "error: " + error);

            // assert
            text.Should().Be("error: source failed");
        }

        [Fact(DisplayName = "RTO-0219: Match on a successful awaitable Result of 6 values should run onSuccess with the values in order.")]
        public async Task RTO0219()
        {
            // act
            var text = await Task.FromResult(Result<(int, int, int, int, int, int), string>.Success((1, 2, 3, 4, 5, 6))).Match((a1, a2, a3, a4, a5, a6) => string.Join(",", new object[] { a1, a2, a3, a4, a5, a6 }), error => "error: " + error);

            // assert
            text.Should().Be("1,2,3,4,5,6");
        }

        [Fact(DisplayName = "RTO-0220: Match on a failed awaitable Result of 6 values should run onError.")]
        public async Task RTO0220()
        {
            // act
            var text = await Task.FromResult(Result<(int, int, int, int, int, int), string>.Failure("source failed")).Match((a1, a2, a3, a4, a5, a6) => string.Join(",", new object[] { a1, a2, a3, a4, a5, a6 }), error => "error: " + error);

            // assert
            text.Should().Be("error: source failed");
        }
    }
}
