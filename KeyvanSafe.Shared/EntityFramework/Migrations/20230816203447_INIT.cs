using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyvanSafe.Shared.EntityFramework.Migrations
{
    public partial class INIT : Migration
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
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMobileConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    LastPasswordChangeTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FailedLoginCount = table.Column<int>(type: "int", nullable: false),
                    LockoutEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    State = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    SecurityStamp = table.Column<string>(type: "nchar(64)", fixedLength: true, maxLength: 64, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsLockedOut = table.Column<bool>(type: "bit", nullable: false),
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
                columns: new[] { "Id", "CreatedAt", "Name", "Title", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6243), "UserManagement", "مدیریت کاربران", new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6246), "identity.users.command" },
                    { 2, new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6252), "RoleManagement", "مدیریت نقش‌ها", new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6253), "identity.roles.command" },
                    { 3, new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6256), "ClaimManagement", "مدیریت دسترسی ها", new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6257), "identity.claims.command" },
                    { 4, new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6260), "UserView", "نمایش  مدیریت کاربران", new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6261), "identity.users.query" },
                    { 5, new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6263), "RoleView", "نمایش  مدیریت نقش ها", new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6265), "identity.roles.query" },
                    { 6, new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6269), "ClaimView", "نمایش  مدیریت دسترسی ها", new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6271), "identity.claims.query" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedAt", "Title", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2023, 8, 16, 13, 34, 47, 147, DateTimeKind.Local).AddTicks(3626), "Owner", new DateTime(2023, 8, 16, 13, 34, 47, 147, DateTimeKind.Local).AddTicks(3628) });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedAt", "Email", "FailedLoginCount", "FirstName", "FullName", "IsLockedOut", "IsMobileConfirmed", "LastLoginDate", "LastName", "LastPasswordChangeTime", "LockoutEndTime", "Mobile", "PasswordHash", "SecurityStamp", "State", "UpdatedAt", "UserName" },
                values: new object[] { 1, "BNBURMMZA41CB27XQQ9BAVQPJTBVR41T", new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6039), "bamdadtabari@outlook.com", 0, "Bamdad", "Bamdad Tabari", false, false, null, "Tabari", new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6028), null, "09301724389", "lpMIfapII2rgoO4V/isM9sj+NJjWZb9n8YLZQRFJNxg=.Rn2VMN+QnKfux9uWOOOrTQ==", "HF55X2EXOEZAG6BEYB6PHR1HR3TXW2FDY8ZTS0F8QBYUYKKCA5LKX1B6LXD5VELV", "Active", new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6041), "Illegible_Owner" });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "Id", "PermissionId", "RoleId", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { 0, 1, 1, new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6352), new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6354) },
                    { 0, 2, 1, new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6425), new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6427) },
                    { 0, 3, 1, new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6430), new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6431) },
                    { 0, 4, 1, new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6433), new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6435) },
                    { 0, 5, 1, new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6437), new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6438) },
                    { 0, 6, 1, new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6441), new DateTime(2023, 8, 16, 13, 34, 47, 156, DateTimeKind.Local).AddTicks(6443) }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "RoleId", "UserId", "CreatedAt", "UpdatedAt" },
                values: new object[] { 0, 1, 1, new DateTime(2023, 8, 16, 13, 34, 47, 147, DateTimeKind.Local).AddTicks(3562), new DateTime(2023, 8, 16, 13, 34, 47, 147, DateTimeKind.Local).AddTicks(3594) });

            migrationBuilder.CreateIndex(
                name: "IX_Claim_UserId",
                table: "Claim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
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
