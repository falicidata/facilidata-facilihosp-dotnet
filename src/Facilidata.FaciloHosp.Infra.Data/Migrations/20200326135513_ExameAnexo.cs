using Microsoft.EntityFrameworkCore.Migrations;

namespace Facilidata.FaciloHosp.Infra.Data.Migrations
{
    public partial class ExameAnexo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Exames",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeArquivo",
                table: "Exames",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Exames");

            migrationBuilder.DropColumn(
                name: "NomeArquivo",
                table: "Exames");
        }
    }
}
