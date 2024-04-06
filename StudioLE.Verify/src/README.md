## About

A library to verify the products of test logic. Whether it's a complex object, or obscure file type `StudioLE.Verify` can handle it.

### Key Features

- Verify strings
- Verify files
- Verify any `object` by serializing with `StudioLE.Serialization`

### Standards

- Follows the [Dependency injection (DI) software design pattern](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection) ensuring ease of extensibility and adaptation to your use case.
- Project structure and code standards follow the [StudioLE Guidelines](https://github.com/StudioLE/Example).

## How to Use

Add references for `StudioLE.Verify` and the `StudioLE.Diagnostics.*` package for your test context to your test project.

```xml
<PackageReference Include="StudioLE.Diagnostics.NUnit" Version="0.9.0" />
<PackageReference Include="StudioLE.Verify" Version="0.9.0" />
```

Refer to the
[VerifyTests.cs](../tests/VerifyTests.cs) file for examples of how to use the verify extension methods.
