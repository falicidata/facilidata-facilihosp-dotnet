using Facilidata.FaciliHosp.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facilidata.FaciloHosp.Infra.Data.MapsEntidades
{
    public class ExameTipoMap : IEntityTypeConfiguration<ExameTipo>
    {
        public void Configure(EntityTypeBuilder<ExameTipo> builder)
        {
            builder.HasKey(exameTipo => exameTipo.Id);

            builder.Property(exameTipo => exameTipo.Nome)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(exameTipo => exameTipo.Descricao)
              .HasColumnType("varchar(250)")
              .HasMaxLength(250);
        }
    }
}
