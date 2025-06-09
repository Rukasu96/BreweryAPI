using BreweryAPI.Models.Account;
using BreweryAPI.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BreweryAPI.Requests
{
    public class AccountRequest
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapPost("/register", AccountRequest.Register);
            app.MapPost("/login", AccountRequest.Login);
            app.MapPut("/update", AccountRequest.Update);
            app.MapDelete("/delete", AccountRequest.Delete);
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

        [Authorize(Roles = "Brewery")]
        public static IResult Update(IAccountService accountService,[FromBody] AccountUpdateDto dto)
        {
            accountService.UpdateAccount(dto);
            return Results.Ok(dto);
        }

        [Authorize(Roles = "Brewery")]
        public static IResult Delete(IAccountService accountService)
        {
            accountService.DeleteAccount();
            return Results.NoContent();
        }
    }
}
