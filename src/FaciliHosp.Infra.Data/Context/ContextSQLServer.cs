using FaciliHosp.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace FaciliHosp.Infra.Data.Context
{
    public class ContextSQLServer : DbContext
    {
        public ContextSQLServer(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Hospital> Hospitais { get; set; }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }

    }
}
