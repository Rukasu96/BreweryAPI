using BreweryAPI.Authentication;
using BreweryAPI.Entities;
using BreweryAPI.Exceptions;
using BreweryAPI.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BreweryAPI.Services
{
    public enum AccType
    {
        Brewery = 1,
        Wholesaler = 2,
        Client = 3
    }

    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto, AccType accountType);
        void DeleteAccount();
        void UpdateAccount(AccountUpdateDto dto);
    }

    public class AccountService : IAccountService
    {
        private readonly dbContext context;
        private readonly IPasswordHasher<UserAccount> passwordHasher;
        private readonly IAuthorizationService authorizationService;
        private readonly AuthenticationSettings authenticationSettings;
        private readonly IUserContextService userContext;

        public AccountService(dbContext context, IPasswordHasher<UserAccount> passwordHasher, AuthenticationSettings authenticationSettings, IAuthorizationService authorizationService, IUserContextService userContext)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
            this.authenticationSettings = authenticationSettings;
            this.authorizationService = authorizationService;
            this.userContext = userContext;
        }

        public string GenerateJwt(LoginDto dto, AccType accountType)
        {
            UserAccount account;

            account = GetAccountType(dto, accountType);

            if(account == null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var result = passwordHasher.VerifyHashedPassword(account, account.PasswordHash, dto.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{account.Name}"),
                new Claim(ClaimTypes.Email, $"{account.Email}"),
                new Claim(ClaimTypes.Role, $"{account.Role.RoleName}"),
                new Claim(ClaimTypes.MobilePhone, $"{account.PhoneNumber}")
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
            UserAccount newUser = null;

            switch (dto.RoleId)
            {
                case 1:
                    newUser = new Brewery()
                    {
                        Email = dto.Email,
                        Name = dto.Name,
                        PhoneNumber = dto.PhoneNumber,
                        RoleId = dto.RoleId,
                    };
                    context.Breweries.Add(newUser as Brewery);
                    break;
                case 2:
                    newUser = new Wholesaler()
                    {
                        Email = dto.Email,
                        Name = dto.Name,
                        PhoneNumber = dto.PhoneNumber,
                        RoleId = dto.RoleId,
                    };
                    context.Wholesalers.Add(newUser as Wholesaler);
                    break;
                case 3:
                    newUser = new Client()
                    {
                        Email = dto.Email,
                        Name = dto.Name,
                        PhoneNumber = dto.PhoneNumber,
                        RoleId = dto.RoleId,
                    };
                    context.Clients.Add(newUser as Client);
                    break;
            }

            var hashedPassword = passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;

            context.SaveChanges();
        }


        public void UpdateAccount(AccountUpdateDto dto)
        {
            var account = context.Breweries.FirstOrDefault(x => x.Id == userContext.GetUserId);

            if (account == null)
            {
                throw new NotFoundException("User not found");
            }

            account.Name = dto.Name;
            account.Email = dto.Email;
            account.Address = new Address()
            {
                City = dto.City,
                Street = dto.Street,
                PostalCode = dto.PostalCode,
            };

            context.SaveChanges();
        }

        public void DeleteAccount()
        {
            //GetAccount Type and delete logged user
            var account = context.Breweries.Include(x => x.Stocks).FirstOrDefault(x => x.Id == userContext.GetUserId);
            
            if (account == null)
            {
                throw new NotFoundException("User not found");
            }

            context.Breweries.Remove(account);
            context.SaveChanges();
        }

        private UserAccount GetAccountType(LoginDto dto, AccType accountType)
        {
            switch (accountType)
            {
                case AccType.Brewery:
                    return context.Breweries.Include(x => x.Role).FirstOrDefault(x => x.Email == dto.Email);
                case AccType.Wholesaler:
                    return context.Wholesalers.Include(x => x.Role).FirstOrDefault(x => x.Email == dto.Email);
                case AccType.Client:
                    return context.Clients.Include(x => x.Role).FirstOrDefault(x => x.Email == dto.Email);
                default:
                    break;
            }

            return null;
        }
    }
}
