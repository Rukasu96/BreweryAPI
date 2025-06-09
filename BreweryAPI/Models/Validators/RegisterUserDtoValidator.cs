using BreweryAPI.Entities;
using BreweryAPI.Models.Account;
using FluentValidation;

namespace BreweryAPI.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(BreweryContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = dbContext.Breweries.Any(u => u.Email == value);

                if (emailInUse)
                {
                    context.AddFailure("Email", "That email is taken");
                }
            });
        }
    }
}
