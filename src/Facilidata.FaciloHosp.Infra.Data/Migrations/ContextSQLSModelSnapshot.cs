﻿// <auto-generated />
using System;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Facilidata.FaciloHosp.Infra.Data.Migrations
{
    [DbContext(typeof(ContextSQLS))]
    partial class ContextSQLSModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Facilidata.FaciliHosp.Domain.Entidades.Exame", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("Anexo")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime?>("AtualizadoEm")
                        .HasColumnType("datetime2");

                    b.Property<string>("AtualizadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CriadoEm")
                        .HasColumnType("datetime2");

                    b.Property<string>("CriadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Deletado")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DeletadoEm")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HospitalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Resultado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HospitalId");

                    b.ToTable("Exames");
                });

            modelBuilder.Entity("Facilidata.FaciliHosp.Domain.Entidades.Hospital", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("AtualizadoEm")
                        .HasColumnType("datetime2");

                    b.Property<string>("AtualizadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bairro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cep")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CriadoEm")
                        .HasColumnType("datetime2");

                    b.Property<string>("CriadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Deletado")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DeletadoEm")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletadoPor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Endereco")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hospitais");
                });

            modelBuilder.Entity("Facilidata.FaciliHosp.Domain.Entidades.Exame", b =>
                {
                    b.HasOne("Facilidata.FaciliHosp.Domain.Entidades.Hospital", "Hospital")
                        .WithMany()
                        .HasForeignKey("HospitalId");
                });
#pragma warning restore 612, 618
        }
    }
}
