using Facilidata.FaciliHosp.Domain.Entidades;
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


    }
}
