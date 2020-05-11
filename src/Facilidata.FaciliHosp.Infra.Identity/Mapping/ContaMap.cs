using Facilidata.FaciliHosp.Infra.Identity.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facilidata.FaciliHosp.Infra.Identity.Mapping
{
    public class ContaMap : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.ToTable("Contas");

            builder.Property(conta => conta.Sexo)
                .HasConversion<string>()
                .HasColumnType("varchar(20)")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(conta => conta.DataNascimento)
                        .IsRequired();


            builder.Property(conta => conta.Crm)
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);
        }
    }
}
