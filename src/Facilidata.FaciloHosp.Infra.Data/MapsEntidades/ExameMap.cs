using Facilidata.FaciliHosp.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facilidata.FaciloHosp.Infra.Data.MapsEntidades
{
    public class ExameMap : IEntityTypeConfiguration<Exame>
    {
        public void Configure(EntityTypeBuilder<Exame> builder)
        {
            builder.Property(exame => exame.Tipo)
                .HasColumnType("varchar(250)")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(exame => exame.Resultado)
             .HasColumnType("varchar(max)");

            builder.Property(exame => exame.UsuarioId)
            .HasColumnType("varchar(36)")
            .HasMaxLength(36);

            builder.Property(exame => exame.Url)
                .HasColumnType("varchar(500)")
                .HasMaxLength(500);

            builder.Property(exame => exame.ContentType)
              .HasColumnType("varchar(50)")
              .HasMaxLength(50);

            builder.Property(exame => exame.NomeArquivo)
              .HasColumnType("varchar(100)")
              .HasMaxLength(100);

            builder.HasOne(exame => exame.Hospital)
                .WithMany(hospital => hospital.Exames)
                .HasForeignKey(exame => exame.HospitalId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); ;


        }
    }
}
