using Facilidata.FaciliHosp.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facilidata.FaciloHosp.Infra.Data.MapsEntidades
{
    public class HospitalMap : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            builder.HasKey(hospital => hospital.Id);

            builder.Property(hospital => hospital.Nome)
                .HasColumnType("varchar(250)")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(hospital => hospital.Endereco)
             .HasColumnType("varchar(250)")
             .HasMaxLength(250);

            builder.Property(hospital => hospital.Cep)
             .HasColumnType("varchar(250)")
             .HasMaxLength(250);

            builder.Property(hospital => hospital.Bairro)
             .HasColumnType("varchar(250)")
             .HasMaxLength(250);

            builder.Property(hospital => hospital.Cidade)
             .HasColumnType("varchar(250)")
             .HasMaxLength(250);

            builder.Property(hospital => hospital.Estado)
             .HasColumnType("varchar(250)")
             .HasMaxLength(250);


  

        }
    }
}
