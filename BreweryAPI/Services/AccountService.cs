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
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);
        void DeleteAccount();
        void UpdateAccount(AccountUpdateDto dto);
    }

    public class AccountService : IAccountService
    {
        private readonly DBaseContext context;
        private readonly IPasswordHasher<UserAccount> passwordHasher;
        private readonly IAuthorizationService authorizationService;
        private readonly AuthenticationSettings authenticationSettings;
        private readonly IUserContextService userContext;

        public AccountService(DBaseContext context, IPasswordHasher<UserAccount> passwordHasher, AuthenticationSettings authenticationSettings, IAuthorizationService authorizationService, IUserContextService userContext)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
            this.authenticationSettings = authenticationSettings;
            this.authorizationService = authorizationService;
            this.userContext = userContext;
        }

        public string GenerateJwt(LoginDto dto)
        {
            UserAccount account;

            account = context.UserAccounts.Include(x => x.Role).FirstOrDefault(x => x.Email == dto.Email);

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
                    context.CompanyAccounts.Add(newUser as CompanyAccount);
                    break;
                case 2:
                    newUser = new Wholesaler()
                    {
                        Email = dto.Email,
                        Name = dto.Name,
                        PhoneNumber = dto.PhoneNumber,
                        RoleId = dto.RoleId,
                    };
                    context.CompanyAccounts.Add(newUser as CompanyAccount);
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
            var account = context.UserAccounts.FirstOrDefault(x => x.Id == userContext.GetUserId);

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
            context.Addresses.Add(account.Address);
            context.SaveChanges();
        }

        public void DeleteAccount()
        {
            //GetAccount Type and delete logged user
            var user = context.UserAccounts.FirstOrDefault(x => x.Id == userContext.GetUserId);
            
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }


            //if(user.RoleId == 0)
            //{
            //    context.UserAccounts.Remove(user);
            //}
            //else
            //{
            //    var companyAccount = context.CompanyAccounts.Include(x => x.Stocks).FirstOrDefault(x => x.Id == user.Id);
            //    context.CompanyAccounts.Remove(companyAccount);
            //}
            context.UserAccounts.Remove(user);
            context.SaveChanges();
        }
    }
}
