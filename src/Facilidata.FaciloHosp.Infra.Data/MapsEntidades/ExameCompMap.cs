using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facilidata.FaciloHosp.Infra.Data.MapsEntidades
{
    public class ExameCompMap : IEntityTypeConfiguration<ExameComp>
    {
        public void Configure(EntityTypeBuilder<ExameComp> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Exame)
                .WithMany(e => e.Compartilhamentos)
                .HasForeignKey(e => e.ExameId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(e => e.Key)
                .HasMaxLength(40)
                .HasColumnType("varchar(40)");

            builder.Property(e => e.UsuarioId)
                .HasMaxLength(40)
                .HasColumnType("varchar(40)");

            builder.Property(e => e.Periodo)
                .HasConversion<string>()
                .HasDefaultValue(EPeriodoComp.Hora)
                .HasMaxLength(10)
                .HasColumnType("varchar(10)");
        }
    }
}
