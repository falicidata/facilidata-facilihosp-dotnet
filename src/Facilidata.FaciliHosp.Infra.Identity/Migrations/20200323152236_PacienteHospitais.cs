using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Facilidata.FaciliHosp.Infra.Identity.Migrations
{
    public partial class PacienteHospitais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContaId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contas",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CriadoPor = table.Column<string>(nullable: true),
                    CriadoEm = table.Column<DateTime>(nullable: true),
                    AtualizadoPor = table.Column<string>(nullable: true),
                    AtualizadoEm = table.Column<DateTime>(nullable: true),
                    DeletadoPor = table.Column<string>(nullable: true),
                    DeletadoEm = table.Column<DateTime>(nullable: true),
                    Deletado = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ContaId",
                table: "AspNetUsers",
                column: "ContaId",
                unique: true,
                filter: "[ContaId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Contas_ContaId",
                table: "AspNetUsers",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Contas_ContaId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Contas");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ContaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ContaId",
                table: "AspNetUsers");
        }
    }
}
