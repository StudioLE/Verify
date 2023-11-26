using StudioLE.Patterns;

namespace StudioLE.Verify.Abstractions;

public interface IDiffer : IStrategy<IReadOnlyCollection<VerifyFile>, Task<IReadOnlyCollection<string>>>
{
}
