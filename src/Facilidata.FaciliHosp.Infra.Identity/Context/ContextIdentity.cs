using Facilidata.FaciliHosp.Infra.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Facilidata.FaciliHosp.Infra.Identity.Context
{
    public class ContextIdentity : IdentityDbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public ContextIdentity(DbContextOptions options) : base(options)
        {

        }


    }
}
