﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UsersWepApiService.DataAccessLayer;

#nullable disable

namespace UsersWepApiService.DataAccessLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230501135124_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.16");

            modelBuilder.Entity("UsersWepApiService.DataLayer.Entities.User", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Guid");

                    b.Property<bool>("Admin")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RevokedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("RevokedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Guid");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("ModifiedBy");

                    b.HasIndex("RevokedBy");

                    b.ToTable("users", (string)null);

                    b.HasData(
                        new
                        {
                            Guid = new Guid("ad097d49-4f4f-4be6-97ba-b57217598e58"),
                            Admin = true,
                            Birthday = new DateTime(2012, 12, 12, 12, 12, 12, 0, DateTimeKind.Unspecified),
                            CreatedBy = "Admin",
                            CreatedOn = new DateTime(2023, 5, 1, 16, 51, 24, 70, DateTimeKind.Local).AddTicks(6769),
                            Gender = 2,
                            Login = "Admin",
                            Name = "Admin",
                            Password = "c1c224b03cd9bc7b6a86d77f5dace40191766c485cd55dc48caf9ac873335d6f"
                        });
                });

            modelBuilder.Entity("UsersWepApiService.DataLayer.Entities.User", b =>
                {
                    b.HasOne("UsersWepApiService.DataLayer.Entities.User", "CreatedByUser")
                        .WithMany("CreatedUsers")
                        .HasForeignKey("CreatedBy")
                        .HasPrincipalKey("Login")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UsersWepApiService.DataLayer.Entities.User", "ModifiedByUser")
                        .WithMany("ModifiedUsers")
                        .HasForeignKey("ModifiedBy")
                        .HasPrincipalKey("Login");

                    b.HasOne("UsersWepApiService.DataLayer.Entities.User", "RevokedByUser")
                        .WithMany("RevokedUsers")
                        .HasForeignKey("RevokedBy")
                        .HasPrincipalKey("Login");

                    b.Navigation("CreatedByUser");

                    b.Navigation("ModifiedByUser");

                    b.Navigation("RevokedByUser");
                });

            modelBuilder.Entity("UsersWepApiService.DataLayer.Entities.User", b =>
                {
                    b.Navigation("CreatedUsers");

                    b.Navigation("ModifiedUsers");

                    b.Navigation("RevokedUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
