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

## Development

### Building

```shell
dotnet build
```

### Running Tests

```shell
dotnet test
```

### Publishing a New Version

1. Create a new tag with the version number:
   ```
   git tag v1.0.0
   git push origin v1.0.0
   ```

2. The GitHub Actions workflow will automatically:
   - Build and test the code
   - Generate NuGet packages
   - Create a GitHub Release
   - Publish packages to NuGet.org (when configured)

## License

MIT