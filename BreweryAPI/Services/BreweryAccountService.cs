using BreweryAPI.Entities;
using BreweryAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BreweryAPI.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);
        void DeleteAccount();
        void UpdateAccount(AccountUpdateDto dto);
    }

    public class BreweryAccountService : IAccountService
    {
        private readonly BreweryContext context;
        private readonly IPasswordHasher<Brewery> passwordHasher;
        private readonly IAuthorizationService authorizationService;
        private readonly AuthenticationSettings authenticationSettings;
        private readonly IUserContextService userContext;

        public BreweryAccountService(BreweryContext context, IPasswordHasher<Brewery> passwordHasher, AuthenticationSettings authenticationSettings, IAuthorizationService authorizationService, IUserContextService userContext)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
            this.authenticationSettings = authenticationSettings;
            this.authorizationService = authorizationService;
            this.userContext = userContext;
        }

        public string GenerateJwt(LoginDto dto)
        {
            var brewery = context.Breweries.Include(x => x.Role).FirstOrDefault(x => x.Email == dto.Email);

            //badRequest
            var result = passwordHasher.VerifyHashedPassword(brewery, brewery.PasswordHash, dto.Password);
            //badRequest

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, brewery.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{brewery.Name}"),
                new Claim(ClaimTypes.Email, $"{brewery.Email}"),
                new Claim(ClaimTypes.Role, $"{brewery.Role.RoleName}"),
                new Claim(ClaimTypes.MobilePhone, $"{brewery.PhoneNumber}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(authenticationSettings.JwtIssuer,
                authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new Brewery()
            {
                Email = dto.Email,
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                RoleId = dto.RoleId,
            };

            var hashedPassword = passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPassword;
            context.Breweries.Add(newUser);
            context.SaveChanges();
        }


        public void UpdateAccount(AccountUpdateDto dto)
        {
            var brewery = context.Breweries.FirstOrDefault(x => x.Id == userContext.GetUserId);

            if (brewery == null)
            {
                //notFound
            }

            brewery.Name = dto.Name;
            brewery.Email = dto.Email;
            brewery.Address = new Address()
            {
                City = dto.City,
                Street = dto.Street,
                PostalCode = dto.PostalCode,
            };

            context.SaveChanges();
        }

        public void DeleteAccount()
        {
            var brewery = context.Breweries.FirstOrDefault(x => x.Id == userContext.GetUserId);
            
            if (brewery == null)
            {
                //notFound
            }

            context.Breweries.Remove(brewery);
            context.SaveChanges();
        }
    }
}
