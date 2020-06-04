using Microsoft.EntityFrameworkCore.Migrations;

namespace Facilidata.FaciloHosp.Infra.Data.Migrations
{
    public partial class campoMedicoExame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Medico",
                table: "Exames",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Medico",
                table: "Exames");
        }
    }
}
