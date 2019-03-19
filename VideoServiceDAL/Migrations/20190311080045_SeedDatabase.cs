using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoServiceDAL.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT [dbo].[Genres] ([Name]) VALUES ( N'Action')");

            migrationBuilder.Sql("INSERT [dbo].[Genres] ([Name]) VALUES (N'Thriller')");

            migrationBuilder.Sql("INSERT [dbo].[Genres] ([Name]) VALUES (N'Family')");

            migrationBuilder.Sql("INSERT [dbo].[Genres] ([Name]) VALUES (N'Romance')");

            migrationBuilder.Sql("INSERT [dbo].[Genres] ([Name]) VALUES (N'Comedy')");


            var actionGenre = "(SELECT [Id] FROM [dbo].[Genres] WHERE [Name] = 'Action' )";

            var thrillerGenre = "(SELECT [Id] FROM [dbo].[Genres] WHERE [Name] = 'Thriller' )";

            var familyGenre = "(SELECT [Id] FROM [dbo].[Genres] WHERE [Name] = 'Family' )";

            var romanceGenre = "(SELECT [Id] FROM [dbo].[Genres] WHERE [Name] = 'Romance' )";

            var comeduGenre = "(SELECT [Id] FROM [dbo].[Genres] WHERE [Name] = 'Comedy' )";


            migrationBuilder.Sql("INSERT [dbo].[Movies] ([Name], [GenreId], [DateAdded], [ReleaseDate], [NumberInStock], [NumberAvailable], [Rate]) VALUES (N'Terminator',"+ actionGenre + " , CAST(N'2019-02-02T00:00:00.0000000' AS DateTime2), CAST(N'2019-03-22T00:00:00.0000000' AS DateTime2), 5, 2, 5)");

            migrationBuilder.Sql("INSERT [dbo].[Movies] ([Name], [GenreId], [DateAdded], [ReleaseDate], [NumberInStock], [NumberAvailable], [Rate]) VALUES (N'Die Hard', " + actionGenre + ", CAST(N'2018-03-01T00:00:00.0000000' AS DateTime2), CAST(N'2019-04-01T00:00:00.0000000' AS DateTime2), 3, 1, 3.5)");

            migrationBuilder.Sql("INSERT [dbo].[Movies] ([Name], [GenreId], [DateAdded], [ReleaseDate], [NumberInStock], [NumberAvailable], [Rate]) VALUES (N'Trip to Italy', "+ comeduGenre + ", CAST(N'2017-03-01T00:00:00.0000000' AS DateTime2), CAST(N'2019-04-01T00:00:00.0000000' AS DateTime2), 1, 1, 1.5)");

            migrationBuilder.Sql("INSERT [dbo].[Movies] ([Name], [GenreId], [DateAdded], [ReleaseDate], [NumberInStock], [NumberAvailable], [Rate]) VALUES (N'It', "+ familyGenre + ", CAST(N'2017-03-08T00:00:00.0000000' AS DateTime2), CAST(N'2019-05-01T00:00:00.0000000' AS DateTime2), 2, 1, 2)");

            migrationBuilder.Sql("INSERT [dbo].[Movies] ([Name], [GenreId], [DateAdded], [ReleaseDate], [NumberInStock], [NumberAvailable], [Rate]) VALUES (N'The Mule', " + familyGenre + ", CAST(N'2017-02-01T00:00:00.0000000' AS DateTime2), CAST(N'2019-04-20T00:00:00.0000000' AS DateTime2), 6, 2, 3)");

            migrationBuilder.Sql("INSERT [dbo].[Movies] ([Name], [GenreId], [DateAdded], [ReleaseDate], [NumberInStock], [NumberAvailable], [Rate]) VALUES (N'Marta', "+ romanceGenre + ", CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 8, 0, 10)");

            migrationBuilder.Sql("INSERT [dbo].[Movies] ([Name], [GenreId], [DateAdded], [ReleaseDate], [NumberInStock], [NumberAvailable], [Rate]) VALUES (N'Fall to love', " + comeduGenre + ", CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 50, 0, 9)");

            migrationBuilder.Sql("INSERT [dbo].[Movies] ([Name], [GenreId], [DateAdded], [ReleaseDate], [NumberInStock], [NumberAvailable], [Rate]) VALUES (N'Legion', " + actionGenre + ", CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 2, 0, 10)");

            migrationBuilder.Sql("INSERT [dbo].[Movies] ([Name], [GenreId], [DateAdded], [ReleaseDate], [NumberInStock], [NumberAvailable], [Rate]) VALUES (N'g', "+ thrillerGenre + ", CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 2, 0, 5)");



            migrationBuilder.Sql("INSERT [dbo].[Users] ([FirstName], [LastName], [Username], [Password], [Role]) VALUES (NULL, NULL, N'Artem', N'$2a$11$zc0KyphTJLtHtytTuMAQqOcYukPFN.Z5OIPXlKXRNDxg7mqfv33lq', 1)");

            migrationBuilder.Sql("INSERT [dbo].[Users] ([FirstName], [LastName], [Username], [Password], [Role]) VALUES (NULL, NULL, N'Alex', N'$2a$11$mBZ7zDSNZY2BbDqQ3lm8e.VwWHgEH4LJjvLaBg.0PLx50PcC3EB5W', 2)");

            migrationBuilder.Sql("INSERT [dbo].[Users] ([FirstName], [LastName], [Username], [Password], [Role]) VALUES (NULL, NULL, N'Anatol', N'$2a$11$CAcz.PmB/fF2miEPPTbyRumCGWVUo.GLf0fjCJZIqV/4Vx8zW3Dcm', 2)");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
