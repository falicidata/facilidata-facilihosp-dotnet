using Microsoft.EntityFrameworkCore.Migrations;

namespace Facilidata.FaciliHosp.Infra.Identity.Migrations
{
    public partial class TabelaContaColunaCpf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Crm",
                table: "Contas");

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Contas",
                type: "varchar(15)",
                maxLength: 15,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Contas");

            migrationBuilder.AddColumn<string>(
                name: "Crm",
                table: "Contas",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true);
        }
    }
}
