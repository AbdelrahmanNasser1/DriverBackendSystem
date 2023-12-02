using FluentValidation.Results;
using System.Text;

namespace Driver_WebAPI.Validators;

public static class ValidationErrors
{
    public static void GetInputValidationErrors(ValidationResult result)
    {
        var errorsString = new StringBuilder();
        foreach (var failure in result.Errors)
        {
            errorsString.AppendLine(failure.ErrorMessage);
        }
        throw new ApplicationException($"{errorsString}");
    }
}
