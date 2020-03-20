using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FaciliHosp.Infra.Data.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exameis",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DeletadoPor = table.Column<string>(nullable: true),
                    DeletadoEm = table.Column<DateTime>(nullable: true),
                    AtualizadoPor = table.Column<string>(nullable: true),
                    AtualizadoEm = table.Column<DateTime>(nullable: true),
                    CriadoPor = table.Column<string>(nullable: true),
                    CriadoEm = table.Column<DateTime>(nullable: true),
                    Deletado = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Endereco = table.Column<string>(nullable: true),
                    Cep = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exameis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Atendimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DeletadoPor = table.Column<string>(nullable: true),
                    DeletadoEm = table.Column<DateTime>(nullable: true),
                    AtualizadoPor = table.Column<string>(nullable: true),
                    AtualizadoEm = table.Column<DateTime>(nullable: true),
                    CriadoPor = table.Column<string>(nullable: true),
                    CriadoEm = table.Column<DateTime>(nullable: true),
                    Deletado = table.Column<bool>(nullable: false),
                    DataHora = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Codigo = table.Column<string>(nullable: true),
                    Avaliacao = table.Column<string>(nullable: true),
                    PacienteId = table.Column<Guid>(nullable: false),
                    HospitalId = table.Column<Guid>(nullable: false),
                    PacientePlanoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Exameis_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Exameis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exames",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DeletadoPor = table.Column<string>(nullable: true),
                    DeletadoEm = table.Column<DateTime>(nullable: true),
                    AtualizadoPor = table.Column<string>(nullable: true),
                    AtualizadoEm = table.Column<DateTime>(nullable: true),
                    CriadoPor = table.Column<string>(nullable: true),
                    CriadoEm = table.Column<DateTime>(nullable: true),
                    Deletado = table.Column<bool>(nullable: false),
                    AtendimentoId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    HospitalId = table.Column<Guid>(nullable: false),
                    Tipo = table.Column<string>(nullable: true),
                    Resultado = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Anexo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exames_Atendimentos_AtendimentoId",
                        column: x => x.AtendimentoId,
                        principalTable: "Atendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exames_Exameis_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Exameis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_HospitalId",
                table: "Atendimentos",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Exames_AtendimentoId",
                table: "Exames",
                column: "AtendimentoId");

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
                name: "Atendimentos");

            migrationBuilder.DropTable(
                name: "Exameis");
        }
    }
}
