using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorNVS.DAL.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZipCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZipcodeNo = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZipCode", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ZipCode",
                columns: new[] { "Id", "ZipcodeNo" },
                values: new object[] { 1, "2450" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZipCode");
        }
    }
}
