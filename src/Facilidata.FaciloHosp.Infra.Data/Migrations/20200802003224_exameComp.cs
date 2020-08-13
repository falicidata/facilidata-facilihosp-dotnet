using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Facilidata.FaciloHosp.Infra.Data.Migrations
{
    public partial class exameComp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResultadoAvaliacao",
                table: "Exames",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "Nenhum");

            migrationBuilder.AddColumn<string>(
                name: "Retorno",
                table: "Exames",
                type: "varchar(5000)",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExameComps",
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
                    ExameId = table.Column<string>(nullable: true),
                    Key = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    Periodo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, defaultValue: "Hora"),
                    UsuarioId = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    ExpiraEm = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExameComps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExameComps_Exames_ExameId",
                        column: x => x.ExameId,
                        principalTable: "Exames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExameComps_ExameId",
                table: "ExameComps",
                column: "ExameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExameComps");

            migrationBuilder.DropColumn(
                name: "ResultadoAvaliacao",
                table: "Exames");

            migrationBuilder.DropColumn(
                name: "Retorno",
                table: "Exames");
        }
    }
}
