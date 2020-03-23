using Facilidata.FaciliHosp.Infra.Identity.Entidades;
using Facilidata.FaciliHosp.Infra.Identity.Mapping;
using Facilidata.FaciliHosp.Infra.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Facilidata.FaciliHosp.Infra.Identity.Context
{
    public class ContextIdentity : IdentityDbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public ContextIdentity(DbContextOptions<ContextIdentity> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ContaMap());
            builder.ApplyConfiguration(new UsuarioMap());
            
            base.OnModelCreating(builder);
        }


    }
}
