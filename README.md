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