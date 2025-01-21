using FluentValidation.Results;
using System.Text.RegularExpressions;
using ValueOf;
using FluentValidation;

namespace Customers.Api.Domain.Common
{
    public class FullName : ValueOf<string, FullName>
    {
        private static readonly Regex FullNameRegex = new Regex(@"^[a-zÖöŞşçÇğĞİıüÜ ,.'-]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        protected override void Validate()
        {
            if (!FullNameRegex.IsMatch(Value))
            {
                var message = $"{Value} is not a valid  full name.";
                throw new ValidationException(message, new[] { new ValidationFailure(nameof(FullName), message) });
            }
        }
    }
}
