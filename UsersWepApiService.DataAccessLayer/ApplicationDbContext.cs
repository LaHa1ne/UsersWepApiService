using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using UsersWepApiService.DataLayer.Entities;
using UsersWepApiService.DataLayer.Helpers;

namespace UsersWepApiService.DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /*public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlite("Data Source=C:\\Users\\LaHaine\\source\\repos\\UsersWepApiService\\UsersWepApiService\\Database\\UsersWebApiServiceDB2.db");

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }*/

        public class BloggingContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlite("Data Source=Database\\UsersWebApiServiceDB.db");

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Guid = Guid.NewGuid(),
                    Login = "Admin",
                    Password = HashPasswordHelper.GetHashPassword("Admin"),
                    Name = "Admin",
                    Gender = 2,
                    Birthday = new DateTime(2012, 12, 12, 12, 12, 12),
                    Admin = true,
                    CreatedOn = DateTime.Now,
                    CreatedBy = "Admin"

                });
        }
    }
}
