using System.Security.Claims;
using UsersWepApiService.DataLayer.Responses;
using UsersWepApiService.DataLayer.ViewModels.Account;

namespace UsersWepApiService.Services.Interfaces
{
    public interface IAccountService
    {
        Task<BaseRepsonse<string>> Login(LoginViewModel loginModel);
    }
}
