using Customers.Api.Contracts.Requests;
using FluentValidation;

namespace Customers.Api.Validation
{
    public class CustomerRequestValidator : AbstractValidator<CustomerRequest>
    {
        public CustomerRequestValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("FullName is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("DateOfBirth is required");
            RuleFor(x => x.GithubUserName).NotEmpty().WithMessage("GithubUserName is required");
        }
    }
}
