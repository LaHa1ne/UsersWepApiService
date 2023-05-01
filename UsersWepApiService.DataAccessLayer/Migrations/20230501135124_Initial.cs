using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsersWepApiService.DataAccessLayer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    Birthday = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Admin = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    RevokedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RevokedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Guid);
                    table.UniqueConstraint("AK_users_Login", x => x.Login);
                    table.ForeignKey(
                        name: "FK_users_users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "users",
                        principalColumn: "Login",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "users",
                        principalColumn: "Login");
                    table.ForeignKey(
                        name: "FK_users_users_RevokedBy",
                        column: x => x.RevokedBy,
                        principalTable: "users",
                        principalColumn: "Login");
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Guid", "Admin", "Birthday", "CreatedBy", "CreatedOn", "Gender", "Login", "ModifiedBy", "ModifiedOn", "Name", "Password", "RevokedBy", "RevokedOn" },
                values: new object[] { new Guid("ad097d49-4f4f-4be6-97ba-b57217598e58"), true, new DateTime(2012, 12, 12, 12, 12, 12, 0, DateTimeKind.Unspecified), "Admin", new DateTime(2023, 5, 1, 16, 51, 24, 70, DateTimeKind.Local).AddTicks(6769), 2, "Admin", null, null, "Admin", "c1c224b03cd9bc7b6a86d77f5dace40191766c485cd55dc48caf9ac873335d6f", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_users_CreatedBy",
                table: "users",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_users_Login",
                table: "users",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_ModifiedBy",
                table: "users",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_users_RevokedBy",
                table: "users",
                column: "RevokedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
