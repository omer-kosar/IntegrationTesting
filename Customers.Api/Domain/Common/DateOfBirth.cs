using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace Customers.Api.Domain.Common
{
    public class DateOfBirth : ValueOf<DateTime, DateOfBirth>
    {
        protected override void Validate()
        {
            if (Value > DateTime.Now)
            {
                const string message = "Date of birth cannot be in the future.";
                throw new ValidationException(message, new[] { new ValidationFailure(nameof(DateOfBirth), message) });
            }
        }
    }
}
