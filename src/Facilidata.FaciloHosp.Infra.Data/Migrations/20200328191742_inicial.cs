using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Facilidata.FaciloHosp.Infra.Data.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hospitais",
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
                    Nome = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Endereco = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    Cep = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    Bairro = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    Cidade = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    Estado = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exames",
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
                    HospitalId = table.Column<string>(nullable: false),
                    UsuarioId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Tipo = table.Column<string>(type: "varchar(251)", maxLength: 251, nullable: false),
                    Resultado = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    Url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    ContentType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NomeArquivo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
 
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exames_Hospitais_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exames_HospitalId",
                table: "Exames",
                column: "HospitalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exames");

            migrationBuilder.DropTable(
                name: "Hospitais");
        }
    }
}
