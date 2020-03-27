﻿using Facilidata.FaciliHosp.Infra.Identity.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facilidata.FaciliHosp.Infra.Identity.Mapping
{
    public class PacienteMap : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.Property(paciente => paciente.CPF)
                 .HasMaxLength(11)
                 .HasColumnType("varchar(11)")
                 .IsRequired();

            builder.Property(paciente => paciente.ConvenioMedico)
             .HasMaxLength(250)
             .HasColumnType("varchar(250)");
             
        }
    }
}
