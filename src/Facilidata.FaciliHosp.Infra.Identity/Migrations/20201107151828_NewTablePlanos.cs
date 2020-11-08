using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Facilidata.FaciliHosp.Infra.Identity.Migrations
{
    public partial class NewTablePlanos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planos",
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
                    Descricao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Valor = table.Column<double>(nullable: false),
                    Armazenamento = table.Column<int>(nullable: false),
                    QuantidadeExameSangue = table.Column<int>(nullable: false),
                    QuantidadeExameImagem = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Planos");
        }
    }
}
