﻿// <auto-generated />
using CardosoRestaurante.Services.CupaoAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CardosoRestaurante.Services.CupaoAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240506220932_AtualizarCodigo")]
    partial class AtualizarCodigo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CardosoRestaurante.Services.CupaoAPI.Models.Cupao", b =>
                {
                    b.Property<int>("CupaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CupaoId"));

                    b.Property<string>("CupaoCodigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Desconto")
                        .HasColumnType("float");

                    b.Property<int>("ValorMinimo")
                        .HasColumnType("int");

                    b.HasKey("CupaoId");

                    b.ToTable("Cupoes");

                    b.HasData(
                        new
                        {
                            CupaoId = 1,
                            CupaoCodigo = "PJ10",
                            Desconto = 10.0,
                            ValorMinimo = 10
                        },
                        new
                        {
                            CupaoId = 2,
                            CupaoCodigo = "RBOTICAS10",
                            Desconto = 10.0,
                            ValorMinimo = 10
                        },
                        new
                        {
                            CupaoId = 3,
                            CupaoCodigo = "MARCOS10",
                            Desconto = 10.0,
                            ValorMinimo = 10
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
