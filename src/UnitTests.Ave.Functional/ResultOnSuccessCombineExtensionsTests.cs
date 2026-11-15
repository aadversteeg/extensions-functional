using Ave.Extensions.Functional;
using Ave.Extensions.Functional.FluentAssertions;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Ave.Functional
{
    public class ResultOnSuccessCombineExtensionsTests
    {
        private record Head(int UnitsPerEm);
        private record Hhea(int NumberOfHMetrics);
        private record Maxp(int GlyphCount);
        private record Hmtx(int Metrics, int Glyphs);
        private record TrueTypeFont(Head Head, Hhea Hhea, Maxp Maxp, Hmtx Hmtx);

        /// <summary>Reads the tables of a font; records every call so a test can see which steps ran.</summary>
        private sealed class FontReader
        {
            public List<string> Calls { get; } = new List<string>();
            public string? FailAt { get; init; }

            private Result<T, string> Read<T>(string table, T value)
            {
                Calls.Add(table);
                return table == FailAt
                    ? Result<T, string>.Failure($"{table} is missing")
                    : Result<T, string>.Success(value);
            }

            public Result<Head, string> ReadHead(string directory) => Read("head", new Head(1000));
            public Result<Hhea, string> ReadHhea(string directory) => Read("hhea", new Hhea(3));
            public Result<Maxp, string> ReadMaxp(string directory) => Read("maxp", new Maxp(7));
            public Result<Hmtx, string> ReadHmtx(string directory, int numberOfHMetrics, int glyphCount) =>
                Read("hmtx", new Hmtx(numberOfHMetrics, glyphCount));

            public async Task<Result<Head, string>> ReadHeadAsync(string directory) { await Task.Yield(); return ReadHead(directory); }
            public async Task<Result<Hhea, string>> ReadHheaAsync(string directory) { await Task.Yield(); return ReadHhea(directory); }
            public async Task<Result<Hmtx, string>> ReadHmtxAsync(string directory, int numberOfHMetrics, int glyphCount)
            {
                await Task.Yield();
                return ReadHmtx(directory, numberOfHMetrics, glyphCount);
            }

            // Exactly the usage from the design brief.
            public Result<TrueTypeFont, string> ReadFont(string directory)
            {
                return ReadHead(directory)
                    .OnSuccessCombine(head => ReadHhea(directory))
                    .OnSuccessCombine((head, hhea) => ReadMaxp(directory))
                    .OnSuccessCombine((head, hhea, maxp) => ReadHmtx(directory, hhea.NumberOfHMetrics, maxp.GlyphCount))
                    .OnSuccessMap((head, hhea, maxp, hmtx) => new TrueTypeFont(head, hhea, maxp, hmtx));
            }

            // The same chain with asynchronous steps mixed in: every source/step shape occurs once.
            public Task<Result<TrueTypeFont, string>> ReadFontAsync(string directory)
            {
                return ReadHeadAsync(directory)                                         // Task source
                    .OnSuccessCombine(head => ReadHheaAsync(directory))                 // Task source, async step
                    .OnSuccessCombine((head, hhea) => ReadMaxp(directory))              // Task source, sync step
                    .OnSuccessCombine((head, hhea, maxp) => ReadHmtxAsync(directory, hhea.NumberOfHMetrics, maxp.GlyphCount))
                    .OnSuccessMap((head, hhea, maxp, hmtx) => new TrueTypeFont(head, hhea, maxp, hmtx));
            }
        }

        [Fact(DisplayName = "ROSCE-0001: The example chain should pass every earlier value to the later steps and map them in order.")]
        public void ROSCE0001()
        {
            // arrange
            var reader = new FontReader();

            // act
            var font = reader.ReadFont("fonts/a");

            // assert
            font.Should().SucceedWith(new TrueTypeFont(new Head(1000), new Hhea(3), new Maxp(7), new Hmtx(3, 7)));
            reader.Calls.Should().Equal("head", "hhea", "maxp", "hmtx");
        }

        [Fact(DisplayName = "ROSCE-0002: In the example chain the first failure should be returned unchanged and later steps should not run.")]
        public void ROSCE0002()
        {
            // arrange
            var reader = new FontReader { FailAt = "hhea" };

            // act
            var font = reader.ReadFont("fonts/a");

            // assert
            font.Should().FailWith("hhea is missing");
            reader.Calls.Should().Equal("head", "hhea");
        }

        [Fact(DisplayName = "ROSCE-0003: In the example chain a failure of the first step should stop the whole chain.")]
        public void ROSCE0003()
        {
            // arrange
            var reader = new FontReader { FailAt = "head" };

            // act
            var font = reader.ReadFont("fonts/a");

            // assert
            font.Should().FailWith("head is missing");
            reader.Calls.Should().Equal("head");
        }

        [Fact(DisplayName = "ROSCE-0004: The asynchronous example chain should give the same font.")]
        public async Task ROSCE0004()
        {
            // arrange
            var reader = new FontReader();

            // act
            var font = await reader.ReadFontAsync("fonts/a");

            // assert
            font.Should().SucceedWith(new TrueTypeFont(new Head(1000), new Hhea(3), new Maxp(7), new Hmtx(3, 7)));
            reader.Calls.Should().Equal("head", "hhea", "maxp", "hmtx");
        }

        [Fact(DisplayName = "ROSCE-0005: In the asynchronous example chain the first failure should stop the later steps.")]
        public async Task ROSCE0005()
        {
            // arrange
            var reader = new FontReader { FailAt = "maxp" };

            // act
            var font = await reader.ReadFontAsync("fonts/a");

            // assert
            font.Should().FailWith("maxp is missing");
            reader.Calls.Should().Equal("head", "hhea", "maxp");
        }

        [Fact(DisplayName = "ROSCE-0006: A chain up to six values should keep every value in order.")]
        public void ROSCE0006()
        {
            // act
            var result = Result<int, string>.Success(1)
                .OnSuccessCombine(a => Result<string, string>.Success("two"))
                .OnSuccessCombine((a, b) => Result<double, string>.Success(3.0))
                .OnSuccessCombine((a, b, c) => Result<char, string>.Success('4'))
                .OnSuccessCombine((a, b, c, d) => Result<long, string>.Success(5L))
                .OnSuccessCombine((a, b, c, d, e) => Result<bool, string>.Success(true));

            // assert
            result.Should().SucceedWith((1, "two", 3.0, '4', 5L, true));
        }

        [Fact(DisplayName = "ROSCE-0007: Existing single-parameter lambdas on a tuple Result should keep using the original overloads.")]
        public async Task ROSCE0007()
        {
            // arrange
            var pair = Result<(int, int), string>.Success((2, 3));
            (int, int) seen = default;

            // act
            var mapped = pair.OnSuccessMap(t => t.Item1 * t.Item2);
            var bound = pair.OnSuccessBind(t => Result<int, string>.Success(t.Item1 + t.Item2));
            var done = pair.OnSuccessDo(t => seen = t);
            var matched = pair.Match(t => t.Item1, error => -1);
            var mappedAsync = await Task.FromResult(pair).OnSuccessMap(t => t.Item2);

            // assert
            mapped.Should().SucceedWith(6);
            bound.Should().SucceedWith(5);
            done.Should().SucceedWith((2, 3));
            seen.Should().Be((2, 3));
            matched.Should().Be(2);
            mappedAsync.Should().SucceedWith(3);
        }

        [Fact(DisplayName = "ROSCE-0008: OnSuccessCombine with a single-parameter lambda on a tuple Result should add the tuple as one value.")]
        public void ROSCE0008()
        {
            // act
            Result<((int, int), int), string> result = Result<(int, int), string>.Success((1, 2))
                .OnSuccessCombine(pair => Result<int, string>.Success(pair.Item1 + pair.Item2));

            // assert
            result.Should().SucceedWith(((1, 2), 3));
        }

        [Fact(DisplayName = "ROSCE-0009: A method group with separate parameters should bind to the spreading overload.")]
        public void ROSCE0009()
        {
            // arrange
            static string Describe(int number, string name) => $"{number}:{name}";

            // act
            var result = Result<(int, string), string>.Success((7, "seven")).OnSuccessMap(Describe);

            // assert
            result.Should().SucceedWith("7:seven");
        }
    }
}
