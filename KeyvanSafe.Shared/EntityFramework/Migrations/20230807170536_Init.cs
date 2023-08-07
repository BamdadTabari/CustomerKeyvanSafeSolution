using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyvanSafe.Shared.EntityFramework.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(140)", maxLength: 140, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(140)", maxLength: 140, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    UpdaterId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(140)", maxLength: 140, nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    UpdaterId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMobileConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    LastPasswordChangeTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FailedLoginCount = table.Column<int>(type: "int", nullable: false),
                    LockoutEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    State = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    SecurityStamp = table.Column<string>(type: "nchar(32)", fixedLength: true, maxLength: 32, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsLockedOut = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    UpdaterId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    UpdaterId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.RoleId, x.PermissionId, x.Id });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Claim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    UpdaterId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claim_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    UpdaterId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId, x.Id });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "CreatedAt", "CreatorId", "Name", "Title", "UpdatedAt", "UpdaterId", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9511), 0, "UserManagement", "مدیریت کاربران", new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9514), 0, "identity.users.command" },
                    { 2, new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9522), 0, "RoleManagement", "مدیریت نقش‌ها", new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9524), 0, "identity.roles.command" },
                    { 3, new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9527), 0, "ClaimManagement", "مدیریت دسترسی ها", new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9528), 0, "identity.claims.command" },
                    { 4, new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9531), 0, "UserView", "نمایش  مدیریت کاربران", new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9532), 0, "identity.users.query" },
                    { 5, new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9534), 0, "RoleView", "نمایش  مدیریت نقش ها", new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9536), 0, "identity.roles.query" },
                    { 6, new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9542), 0, "ClaimView", "نمایش  مدیریت دسترسی ها", new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9544), 0, "identity.claims.query" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedAt", "CreatorId", "Title", "UpdatedAt", "UpdaterId" },
                values: new object[] { 1, new DateTime(2023, 8, 7, 10, 5, 36, 324, DateTimeKind.Local).AddTicks(8153), 0, "Owner", new DateTime(2023, 8, 7, 10, 5, 36, 324, DateTimeKind.Local).AddTicks(8154), 0 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedAt", "CreatorId", "Email", "FailedLoginCount", "IsLockedOut", "IsMobileConfirmed", "LastLoginDate", "LastPasswordChangeTime", "LockoutEndTime", "Mobile", "PasswordHash", "SecurityStamp", "State", "UpdatedAt", "UpdaterId", "Username" },
                values: new object[] { 1, "6CENIMVUC1FV4MNX45OJMJFLEMZRGCEH", new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9258), 0, "bamdadtabari@outlook.com", 0, false, false, null, new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9239), null, "09301724389", "6HJ3nVXreGJGBxw4xSuaEOvuxDM/ljwLKgkJPCZ2FeE=.lOSe3TUpmCIMmw7yDPeGZA==", "QFRU1HHGR0DR1EZYMA2SH48EOA75H1JO", "Active", new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9260), 0, "Illegible_Owner" });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "Id", "PermissionId", "RoleId", "CreatedAt", "CreatorId", "UpdatedAt", "UpdaterId" },
                values: new object[,]
                {
                    { 0, 1, 1, new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9626), 0, new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9628), 0 },
                    { 0, 2, 1, new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9633), 0, new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9634), 0 },
                    { 0, 3, 1, new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9637), 0, new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9638), 0 },
                    { 0, 4, 1, new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9640), 0, new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9642), 0 },
                    { 0, 5, 1, new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9644), 0, new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9645), 0 },
                    { 0, 6, 1, new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9648), 0, new DateTime(2023, 8, 7, 10, 5, 36, 334, DateTimeKind.Local).AddTicks(9650), 0 }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "RoleId", "UserId", "CreatedAt", "CreatorId", "UpdatedAt", "UpdaterId" },
                values: new object[] { 0, 1, 1, new DateTime(2023, 8, 7, 10, 5, 36, 324, DateTimeKind.Local).AddTicks(8084), 0, new DateTime(2023, 8, 7, 10, 5, 36, 324, DateTimeKind.Local).AddTicks(8124), 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Claim_UserId",
                table: "Claim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claim");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
