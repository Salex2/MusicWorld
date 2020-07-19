using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicData.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SongName = table.Column<string>(nullable: true),
                    Duration = table.Column<float>(nullable: false),
                    DateOfCreation = table.Column<DateTime>(nullable: false),
                    Released = table.Column<bool>(nullable: false),
                    SongUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Bio = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    MusicGenre = table.Column<string>(nullable: false),
                    NumberOfAlbums = table.Column<int>(nullable: false),
                    NumberOfLikes = table.Column<int>(nullable: false),
                    ArtistSongsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artists_Songs_ArtistSongsId",
                        column: x => x.ArtistSongsId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artists_ArtistSongsId",
                table: "Artists",
                column: "ArtistSongsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "Songs");
        }
    }
}
