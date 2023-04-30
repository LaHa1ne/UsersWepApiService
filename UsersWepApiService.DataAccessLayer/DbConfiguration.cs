using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersWepApiService.DataLayer.Entities;

namespace UsersWepApiService.DataAccessLayer
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {

            entity.ToTable("users");

            entity.HasKey(e => e.Guid);

            entity.Property(e => e.Login).IsRequired();
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Gender).IsRequired();

            entity.HasIndex(e => e.Login).IsUnique();


            entity
                .HasOne(u => u.CreatedByUser)
                .WithMany(u => u.CreatedUsers)
                .HasForeignKey(u => u.CreatedBy)
                .HasPrincipalKey(u => u.Login);


            entity
                .HasOne(u => u.ModifiedByUser)
                .WithMany(u => u.ModifiedUsers)
                .HasForeignKey(u => u.ModifiedBy)
                .HasPrincipalKey(u => u.Login);

            entity
                .HasOne(u => u.RevokedByUser)
                .WithMany(u => u.RevokedUsers)
                .HasForeignKey(u => u.RevokedBy)
                .HasPrincipalKey(u => u.Login);

        }
    }
}
