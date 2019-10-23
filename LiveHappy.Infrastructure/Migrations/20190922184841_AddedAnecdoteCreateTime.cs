using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LiveHappy.Infrastructure.Migrations
{
    public partial class AddedAnecdoteCreateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Anecdotes",
                type: "datetime",
                nullable: false,
                defaultValueSql: "'1900-01-01T00:00:00.000'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Anecdotes");
        }
    }
}
