using Facilidata.FaciliHosp.Infra.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facilidata.FaciliHosp.Infra.Identity.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasOne(usuario => usuario.Conta)
                .WithOne(conta => conta.Usuario)
                .HasForeignKey<Usuario>(usuario => usuario.ContaId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
