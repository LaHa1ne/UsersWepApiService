using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersWepApiService.DataAccessLayer.Interfaces;
using UsersWepApiService.DataLayer.Entities;

namespace UsersWepApiService.DataAccessLayer.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<User>> GetActiveUsers()
        {
            return await _db.Users.Where(u => u.RevokedOn == null).OrderBy(u=>u.CreatedOn).AsNoTracking().ToListAsync();
        }
        public async Task<User> GetUserByLogin(string Login)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Login == Login);
        }
        public async Task<User> GetUserByGuid(Guid Guid)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Guid == Guid);
        }
        public async Task<User> GetUserByLoginAndPassword(string Login, string HashPassword)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Login == Login);
            return (user == null || user.RevokedOn != null) ? null : user.Password == HashPassword ? user: null;
        }
        public async Task<IEnumerable<User>> GetUsersOverCertainAge(int Age)
        {
            return await _db.Users.Where(u => u.Birthday<=DateTime.Now.AddYears(-Age)).AsNoTracking().ToListAsync();
        }
    }
}
