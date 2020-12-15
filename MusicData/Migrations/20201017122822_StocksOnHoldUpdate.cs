using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicData.Migrations
{
    public partial class StocksOnHoldUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "StocksOnHold",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "StocksOnHold");
        }
    }
}
