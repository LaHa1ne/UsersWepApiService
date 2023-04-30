using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersWepApiService.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UsersWepApiService.DataAccessLayer.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _db;

        public BaseRepository(ApplicationDbContext db) => _db = db;

        public async Task Create(T Entity)
        {
            await _db.Set<T>().AddAsync(Entity);
            await _db.SaveChangesAsync();
        }
        public async Task Update(T Entity)
        {
            _db.Set<T>().Update(Entity);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(T Entity)
        {
            _db.Set<T>().Remove(Entity);
            await _db.SaveChangesAsync();
        }
    }
}
