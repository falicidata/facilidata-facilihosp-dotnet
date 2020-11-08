using Facilidata.FaciliHosp.Infra.Identity.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facilidata.FaciliHosp.Infra.Identity.Mapping
{
    public class PlanoMap : IEntityTypeConfiguration<Plano>
    {
        public void Configure(EntityTypeBuilder<Plano> builder)
        {
            builder.ToTable("Planos");

            builder.Property(plano => plano.Descricao)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(plano => plano.Valor)
                        .IsRequired();

            builder.Property(plano => plano.Armazenamento)
                        .IsRequired();

            builder.Property(plano => plano.QuantidadeExameSangue)
                        .IsRequired();

            builder.Property(plano => plano.QuantidadeExameImagem)
                        .IsRequired();
        }

    }
}
