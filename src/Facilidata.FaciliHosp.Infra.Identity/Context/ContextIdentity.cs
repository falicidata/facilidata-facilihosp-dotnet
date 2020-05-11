using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Entidades;
using Facilidata.FaciliHosp.Infra.Identity.Mapping;
using Facilidata.FaciliHosp.Infra.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Facilidata.FaciliHosp.Infra.Identity.Context
{
    public class ContextIdentity : IdentityDbContext
    {
        private readonly IUsuarioAspNet _usuarioAspNet;
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Conta> Contas { get; set; }


        public ContextIdentity()
        {

        }
        public ContextIdentity(DbContextOptions<ContextIdentity> options, IUsuarioAspNet usuarioAspNet) : base(options)
        {
            _usuarioAspNet = usuarioAspNet;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ContaMap());
            builder.ApplyConfiguration(new UsuarioMap());
            
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connectionString = configuration.GetConnectionString("SQLServer");
            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }
        public override int SaveChanges()
        {
            var inseridos = this.ChangeTracker.Entries().Where(entry => entry.Entity is Entidade && entry.State == EntityState.Added).ToList();
            var atualizados = this.ChangeTracker.Entries().Where(entry => entry.Entity is Entidade && entry.State == EntityState.Modified).ToList();
            var deletados = this.ChangeTracker.Entries().Where(entry => entry.Entity is Entidade && entry.State == EntityState.Deleted).ToList();

            if (inseridos.Any()) InsereEntidades(inseridos);
            if (atualizados.Any()) AtualizaEntidades(atualizados);
            if (deletados.Any()) DeletaEntidades(deletados);

            return base.SaveChanges();
        }

        private void InsereEntidades(List<EntityEntry> inseridos)
        {
            foreach (var entry in inseridos)
            {
                var entidade = (Entidade)entry.Entity;
                entidade.CriadoEm = DateTime.Now;
                entidade.CriadoPor = _usuarioAspNet.GetUserName();
            }
        }

        private void AtualizaEntidades(List<EntityEntry> atualizados)
        {
            foreach (var entry in atualizados)
            {
                entry.Property("CriadoEm").IsModified = false;
                entry.Property("CriadoPor").IsModified = false;
                entry.Property("DeletadoEm").IsModified = false;
                entry.Property("DeletadoPor").IsModified = false;

                var entidade = (Entidade)entry.Entity;
                entidade.AtualizadoEm = DateTime.Now;
                entidade.AtualizadoPor = _usuarioAspNet.GetUserName();
            }
        }

        private void DeletaEntidades(List<EntityEntry> deletados)
        {
            foreach (var entry in deletados)
            {
                entry.Property("CriadoEm").IsModified = false;
                entry.Property("CriadoPor").IsModified = false;
                entry.Property("AtualizadoEm").IsModified = false;
                entry.Property("AtualizadoPor").IsModified = false;

                var entidade = (Entidade)entry.Entity;
                entidade.DeletadoEm = DateTime.Now;
                entidade.Deletado = true;
                entidade.AtualizadoPor = _usuarioAspNet.GetUserName();

                entry.State = EntityState.Modified;
            }
        }


    }
}
