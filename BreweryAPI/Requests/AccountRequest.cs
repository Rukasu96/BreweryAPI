using BreweryAPI.Models;
using BreweryAPI.Services;
using FluentValidation;

namespace BreweryAPI.Requests
{
    public class AccountRequest
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapPost("/register", AccountRequest.Register);
            app.MapPost("/login", AccountRequest.Login);
        }

        public static IResult Register(IAccountService accountService, RegisterUserDto dto, IValidator<RegisterUserDto> validator)
        {
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid) 
            {
                return Results.BadRequest(validationResult.Errors);
            }
            accountService.RegisterUser(dto);
            return Results.Ok(dto);
        }

        public static IResult Login(IAccountService accountService, LoginDto dto)
        {
            string token = accountService.GenerateJwt(dto);
            return Results.Ok(token);
        }
    }
}
