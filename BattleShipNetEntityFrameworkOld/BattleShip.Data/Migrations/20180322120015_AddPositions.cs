using Microsoft.EntityFrameworkCore.Migrations;

namespace BattleShip.Data.Migrations
{
    public partial class AddPositions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            int i = 1;
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    migrationBuilder.InsertData("Positions", new string[] { "Id", "X", "Y" }, new object[] { i, x, y });
                    i++;
                }
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"TRUNCATE TABLE Positions");
        }
    }
}
