using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersWepApiService.DataLayer.DTO;
using UsersWepApiService.DataLayer.Entities;
using UsersWepApiService.DataLayer.Responses;

namespace UsersWepApiService.Services.Interfaces
{
    public interface IUsersService
    {
        Task<BaseRepsonse<bool>> CreateUser(CreatedUserInfoDTO CreatedUserInfo, string CreateByGuid);
        Task<BaseRepsonse<bool>> UpdateMainUserData(UserPersonalInfoDTO UserPersonalInfo, string RequesterUserGuid);
        Task<BaseRepsonse<bool>> UpdateUserPassword(UserPersonalInfoDTO UserPersonalInfo, string RequesterUserGuid);
        Task<BaseRepsonse<bool>> UpdateUserLogin(UserPersonalInfoDTO UserPersonalInfo, string RequesterUserGuid);

        Task<BaseRepsonse<IEnumerable<UserInfoDTO>>> GetActiveUsers();

        Task<BaseRepsonse<UserMainPersonalInfoDTO>> GetUserByLogin(string Login);

        Task<BaseRepsonse<UserMainPersonalInfoDTO>> GetUserByLoginAndPassword(string Login, string Password, string RequesterUserGuid);

        Task<BaseRepsonse<IEnumerable<UserInfoDTO>>> GetUsersOverCertainAge(string Age);

        Task<BaseRepsonse<bool>> SoftDeleteUser(string LoginOfDeletedUser, string RequesterUserGuid);

        Task<BaseRepsonse<bool>> DeleteUser(string LoginOfDeletedUser);

        Task<BaseRepsonse<bool>> RevokeUser(string LoginOfRevokedUser);

        Task<BaseRepsonse<bool>> CheckIsRequesterUserExists(string RequesterUserGuid);

    }
}
