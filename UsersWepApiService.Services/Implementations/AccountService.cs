using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UsersWepApiService.DataLayer.Entities;
using UsersWepApiService.DataLayer.Enums;
using UsersWepApiService.DataLayer.Helpers;
using UsersWepApiService.DataLayer.Responses;
using UsersWepApiService.DataLayer.ViewModels.Account;
using UsersWepApiService.DataAccessLayer.Interfaces;
using UsersWepApiService.Services.Interfaces;
using UsersWepApiService.DataLayer.DTO;
using Microsoft.Extensions.Configuration;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace UsersWepApiService.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AccountService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<BaseRepsonse<string>> Login(LoginViewModel loginModel)
        {
            try
            {
                var (IsDataValid, ErrorDescription) = DataValidationHelper.CheckUserLoginAndPasswordIsValid(loginModel.Login, loginModel.Password);
                if (!IsDataValid) return new BaseRepsonse<string>(Description: ErrorDescription, StatusCode: StatusCode.UnprocessableContent);

                var user = await _userRepository.GetUserByLoginAndPassword(loginModel.Login, HashPasswordHelper.GetHashPassword(loginModel.Password));
                if (user == null) return new BaseRepsonse<string>(Description: "Неправильный логин или пароль", StatusCode: StatusCode.NotFound);

                var token = GenerateJwtToken(user);

                return new BaseRepsonse<string>()
                {
                    Description = "Токен успешно получен",
                    Data = token,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<string>(Description: ex.Message, StatusCode: StatusCode.InternalServerError);
            }
        }

        private List<Claim> GetClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Guid.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Admin?"Admin":"User")
            };
        }

        private string GenerateJwtToken(User user)
        {
            var Jwt = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: GetClaims(user),
                    expires: DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:ExpireMinutes"])),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256)
                    );
            return new JwtSecurityTokenHandler().WriteToken(Jwt);
        }
    }
}
