using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facilidata.FaciloHosp.Infra.Data.MapsEntidades
{
    public class ExameMap : IEntityTypeConfiguration<Exame>
    {
        public void Configure(EntityTypeBuilder<Exame> builder)
        {
            builder.HasKey(exame => exame.Id);

            builder.HasOne(exame => exame.Tipo)
                .WithMany(exameTipo => exameTipo.Exames)
                .HasForeignKey(exameTipo => exameTipo.TipoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(exame => exame.Formato)
                .HasConversion<string>()
                 .HasColumnType("varchar(20)")
                 .HasMaxLength(20);

            builder.Property(exame => exame.TipoOutro)
                .HasColumnType("varchar(251)")
                .HasMaxLength(251);

            builder.Property(exame => exame.Medico)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(exame => exame.Resultado)
                .HasMaxLength(5000)
                .HasColumnType("varchar(5000)");

            builder.Property(exame => exame.Fornecedor)
                .HasColumnType("varchar(250)")
                .HasMaxLength(250);

            builder.Property(exame => exame.FornecedorId)
                .HasColumnType("varchar(40)")
                .HasMaxLength(40);

            builder.Property(exame => exame.UsuarioId)
                .HasColumnType("varchar(40)")
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(exame => exame.Url)
                .HasColumnType("varchar(500)")
                .HasMaxLength(500);

            builder.Property(exame => exame.ContentType)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(exame => exame.NomeArquivo)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(exame => exame.ResultadoAvaliacao)
             .HasConversion<string>()
             .HasDefaultValue(EExameResultadoAvaliacao.Nenhum)
                 .HasColumnType("varchar(10)")
                 .HasMaxLength(10);

            builder.Property(exame => exame.Retorno)
                .HasColumnType("varchar(5000)")
                .HasMaxLength(5000);


            builder.Property(exame => exame.RetornoUsuario)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

        }
    }
}
