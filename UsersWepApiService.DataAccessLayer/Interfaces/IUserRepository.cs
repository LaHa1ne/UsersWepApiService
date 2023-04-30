using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersWepApiService.DataLayer.DTO;
using UsersWepApiService.DataLayer.Entities;

namespace UsersWepApiService.DataAccessLayer.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IEnumerable<User>> GetActiveUsers();
        Task<User> GetUserByLogin (string Login);
        Task<User> GetUserByGuid (Guid Guid);
        Task<User> GetUserByLoginAndPassword(string Login, string HashPassword);
        Task<IEnumerable<User>> GetUsersOverCertainAge(int Age);


    }
}
