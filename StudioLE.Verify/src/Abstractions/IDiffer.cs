using StudioLE.Core.Patterns;
using StudioLE.Core.Results;

namespace StudioLE.Verify.Abstractions;

// TODO: Revise to accept an ILogger instead of returning IResult

public interface IDiffer : IStrategy<IReadOnlyCollection<VerifyFile>, Task<IResult>>
{
}
