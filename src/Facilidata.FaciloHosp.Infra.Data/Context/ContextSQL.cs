using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.MapsEntidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Facilidata.FaciloHosp.Infra.Data.Context
{
    public class ContextSQL : DbContext
    {
        private readonly IUsuarioAspNet _usuarioAspNet;

        public ContextSQL()
        {

        }

        public ContextSQL(IUsuarioAspNet usuarioAspNet)
        {
            _usuarioAspNet = usuarioAspNet;
        }

        public DbSet<Exame> Exames { get; set; }
        public DbSet<ExameTipo> ExameTipos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExameMap());

            base.OnModelCreating(modelBuilder);
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
                entidade.DeletadoPor = _usuarioAspNet.GetUserName();

                entry.State = EntityState.Modified;
            }
        }
    }
}
