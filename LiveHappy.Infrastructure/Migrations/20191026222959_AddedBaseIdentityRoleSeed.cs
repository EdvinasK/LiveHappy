using Microsoft.EntityFrameworkCore.Migrations;

namespace LiveHappy.Infrastructure.Migrations
{
    public partial class AddedBaseIdentityRoleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { "b154ed34-4353-49e5-bd7a-4c5786695d7b", "69fde719-7e1c-4f2b-83b3-35987adfecb6", "Administrator user", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { "0736d402-175c-4d36-ad73-cb1c1537fdb0", "a2899d94-6965-40c3-8fac-374f92ff5fcc", "User with additional features", "VIP", "VIP" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { "f2f5d08f-075e-4ff9-8bda-d385ebad0550", "80487ed3-d8bf-423e-a52f-0de56d5e47fe", "Basic user", "Member", "MEMBER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0736d402-175c-4d36-ad73-cb1c1537fdb0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b154ed34-4353-49e5-bd7a-4c5786695d7b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2f5d08f-075e-4ff9-8bda-d385ebad0550");
        }
    }
}
