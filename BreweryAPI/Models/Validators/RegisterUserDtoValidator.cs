using BreweryAPI.Entities;
using BreweryAPI.Models.Account;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BreweryAPI.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(DBaseContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.PhoneNumber).NotEmpty()
                .WithMessage("Phone Number is required.")
                .MinimumLength(9).WithMessage("PhoneNumber must not be less than 10 characters.")
                .MaximumLength(11).WithMessage("PhoneNumber must not exceed 11 characters.")
                .Matches(new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{3,6}$")).WithMessage("PhoneNumber not valid");

            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = dbContext.UserAccounts.Any(u => u.Email == value);

                if (emailInUse)
                {
                    context.AddFailure("Email", "That email is taken");
                }
            });
        }
    }
}
