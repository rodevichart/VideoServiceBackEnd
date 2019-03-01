﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoServiceDAL.Migrations
{
    public partial class SeedDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Genres (Id, Name) VALUES (1, 'Action')");
            migrationBuilder.Sql("INSERT INTO Genres (Id, Name) VALUES (2, 'Thriller')");
            migrationBuilder.Sql("INSERT INTO Genres (Id, Name) VALUES (3, 'Family')");
            migrationBuilder.Sql("INSERT INTO Genres (Id, Name) VALUES (4, 'Romance')");
            migrationBuilder.Sql("INSERT INTO Genres (Id, Name) VALUES (5, 'Comedy')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Genres where Name IN('Action', 'Thriller', 'Family', 'Romance', 'Comedy')");
        }
    }
}
