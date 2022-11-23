using FluentResults;
using FluentValidation.Results;

namespace VerticalSliceArchitecture.Core.Common.Errors;
public class ValidationError : Error
{
    public ValidationError(List<ValidationFailure> failures)
    {
        Failures = failures;
    }

    public List<ValidationFailure> Failures { get; }
}
