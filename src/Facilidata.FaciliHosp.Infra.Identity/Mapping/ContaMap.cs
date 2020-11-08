using Facilidata.FaciliHosp.Infra.Identity.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Facilidata.FaciliHosp.Infra.Identity.Mapping
{
    public class ContaMap : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.ToTable("Contas");

            builder.HasOne(conta => conta.Plano)
                .WithMany(plano => plano.Contas)
                .HasForeignKey(conta => conta.PlanoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(conta => conta.Sexo)
                .HasConversion<string>()
                .HasColumnType("varchar(20)")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(conta => conta.DataNascimento)
                        .IsRequired();


            builder.Property(conta => conta.Cpf)
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(15);
        }
    }
}
