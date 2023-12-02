using Driver_WebAPI.DTOs;
using Driver_WebAPI.Models;
using FluentValidation;

namespace Driver_WebAPI.Validators;

/// <summary>
/// Using Fluent Validation to apply validation on inputs for our DTO <DriverDto>
/// </summary>
public class DriverDtoValidators: AbstractValidator<DriverDto>
{
    public DriverDtoValidators()
    {
        RuleFor(driver => driver.FirstName).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(driver => driver.LastName).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(driver => driver.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(driver => driver.PhoneNumber).NotNull().NotEmpty().Matches(@"^[0-9]+$").MaximumLength(12).MinimumLength(8);
    }
}
