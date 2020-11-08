using Microsoft.EntityFrameworkCore.Migrations;

namespace Facilidata.FaciliHosp.Infra.Identity.Migrations
{
    public partial class TableContasNewColumnPlanoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlanoId",
                table: "Contas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contas_PlanoId",
                table: "Contas",
                column: "PlanoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contas_Planos_PlanoId",
                table: "Contas",
                column: "PlanoId",
                principalTable: "Planos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contas_Planos_PlanoId",
                table: "Contas");

            migrationBuilder.DropIndex(
                name: "IX_Contas_PlanoId",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "PlanoId",
                table: "Contas");
        }
    }
}
