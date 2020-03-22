using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciloHosp.Infra.Data.MapsEntidades;
using Microsoft.EntityFrameworkCore;

namespace Facilidata.FaciloHosp.Infra.Data.Context
{
    public class ContextSQLS : DbContext
    {
        public ContextSQLS(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Hospital> Hospitais { get; set; }
        public DbSet<Exame> Exames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new HospitalMap());
            modelBuilder.ApplyConfiguration(new ExameMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
