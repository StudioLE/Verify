## About

A library providing extension methods for `StudioLE.Verify` to handle serializing objects to YAML.

### Key Features

- Verify any `object` by serializing to `YAML`

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
[VerifyTests.cs](../../StudioLE.Verify/tests/VerifyTests.cs) file for examples of how to use the verify extension methods.
