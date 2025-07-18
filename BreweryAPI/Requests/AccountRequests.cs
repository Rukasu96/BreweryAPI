﻿using BreweryAPI.Models.Account;
using BreweryAPI.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Requests
{
    public class AccountRequests
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapPost("/register", AccountRequests.Register);
            app.MapPost("/login", AccountRequests.Login);
            app.MapPut("/update", AccountRequests.Update);
            app.MapDelete("/delete", AccountRequests.Delete);
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

        [Authorize(Roles = "Brewery, Wholesaler, Client")]
        public static IResult Update(IAccountService accountService,[FromBody] AccountUpdateDto dto)
        {
            accountService.UpdateAccount(dto);
            return Results.Ok(dto);
        }

        [Authorize(Roles = "Brewery, Wholesaler, Client")]
        public static IResult Delete(IAccountService accountService)
        {
            accountService.DeleteAccount();
            return Results.NoContent();
        }
    }
}
