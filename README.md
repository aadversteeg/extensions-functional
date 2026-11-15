# Ave.Extensions.Functional

Functional programming extensions and utilities for .NET applications.

## Features

- Maybe type (Option pattern) implementation
- Result type for error handling
- Fluent extension methods for functional composition
- FluentAssertions extensions for testing functional code

## Packages

| Package | Description |
|---------|-------------|
| `Ave.Extensions.Functional.Core` | Core functional types and utilities |
| `Ave.Extensions.Functional` | Extension methods for working with functional types |
| `Ave.Extensions.Functional.FluentAssertions` | FluentAssertions extensions for testing functional code |

## Getting Started

Install the packages from NuGet:

```shell
dotnet add package Ave.Extensions.Functional
```

Basic usage:

```csharp
using Ave.Extensions.Functional;
using Ave.Extensions.Functional.Core;

// Using the Maybe type
Maybe<string> maybeName = userName ?? Maybe<string>.None;
string greeting = maybeName
    .OnSome(name => $"Hello, {name}!")
    .OnNone(() => "Hello, guest!");

// Using the Result type
Result<User, Error> result = userService.GetUser(userId);
result
    .OnSuccess(user => DisplayUser(user))
    .OnFailure(error => DisplayError(error));
```

### Maybe and null

Null and None are the same thing. `Maybe.From(null)`, `Maybe<T>.From(null)` and an implicit
conversion from null all produce `None`, so a `Maybe` that reports `HasValue` never holds a null
value and `Value` is safe to use once `HasValue` has been checked. A `Nullable<T>` without a value
is treated as null and also yields `None`.

Only null yields `None`: default values such as `0`, `false` and the empty string are values.

```csharp
Maybe<string>.From(null).HasNoValue;          // true
Maybe<string>.From(string.Empty).HasValue;    // true
Maybe<int>.From(0).HasValue;                  // true
```

Mapping follows the same rule: if the mapping function returns null, `OnSomeMap` and `OnNoneMap`
produce `None` rather than a `Maybe` holding null.

### Mapping a list: TraverseAll

`TraverseAll` applies a function that returns a `Result` to every item of a sequence. If every item
succeeds, you get all values, in input order. If one or more items fail, you get one error: the
`combine` function receives every error, in input order, and turns them into one.

Every item is visited, also after a failure. That is the difference with a loop that stops at the
first error: a caller that validates a list hears about all problems at once.

```csharp
Result<int, string> Parse(string text) =>
    int.TryParse(text, out var number)
        ? Result<int, string>.Success(number)
        : Result<int, string>.Failure($"'{text}' is not a number");

var ok = new[] { "3", "1", "2" }.TraverseAll(Parse, errors => string.Join("; ", errors));
// ok.Value: [3, 1, 2]

var bad = new[] { "a", "1", "b" }.TraverseAll(Parse, errors => string.Join("; ", errors));
// bad.Error: "'a' is not a number; 'b' is not a number"
```

- An empty sequence succeeds with an empty list.
- `combine` is called only when something failed; a single failure arrives as a list of one.
- The function may be asynchronous (`Func<T, Task<Result<TOut, E>>>`). The items are then mapped one
  after the other, never in parallel.
- `Ave.Extensions.ErrorPaths.Functional` adds an overload for its `Error` type that needs no
  `combine`: it collects the errors with `Errors.Aggregate`.

## Development

### Building

```shell
dotnet build
```

### Running Tests

```shell
dotnet test
```

## License

MIT