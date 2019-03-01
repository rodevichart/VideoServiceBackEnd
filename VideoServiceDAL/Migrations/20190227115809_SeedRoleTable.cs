using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoService.Migrations
{
    public partial class SeedRoleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Roles VALUES (1, 'Admin')");
            migrationBuilder.Sql("INSERT INTO Roles VALUES (2, 'User')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
