﻿// <auto-generated />
using System;
using Hotel.LogicaAccesoDatos.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hotel.LogicaAccesoDatos.Migrations
{
    [DbContext(typeof(HotelContext))]
    [Migration("20230507194826_nombreObjeto-topesDescripcion-unico")]
    partial class nombreObjetotopesDescripcionunico
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hotel.LogicaNegocio.Entidades.TopesDescripcion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NombreObj")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TopeMax")
                        .HasColumnType("int");

                    b.Property<int>("TopeMin")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "NombreObj" }, "INX_NombreObjeto")
                        .IsUnique();

                    b.ToTable("TopesDescripciones");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.TipoCabanha", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CantMaxPersonas")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HabilitadaParaReservas")
                        .HasColumnType("bit");

                    b.Property<int>("IdTipoCabanha")
                        .HasColumnType("int");

                    b.Property<bool>("Jacuzzi")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("NumHabitacion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdTipoCabanha");

                    b.HasIndex(new[] { "Nombre" }, "INX_NombreCabanha")
                        .IsUnique();

                    b.HasIndex(new[] { "NumHabitacion" }, "INX_NumHabitacion")
                        .IsUnique();

                    b.ToTable("Cabanhas");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Mantenimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("CostoMantenimiento")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCabanha")
                        .HasColumnType("int");

                    b.Property<string>("NomRealizoTrabajo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdCabanha");

                    b.ToTable("Mantenimientos");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.TipoCabanha", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("CostoPorHuesped")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Nombre" }, "INX_NombreTipoCabanha")
                        .IsUnique();

                    b.ToTable("TipoCabanhas");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Usuarios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Email" }, "INX_EmailUsuario")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.TipoCabanha", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.TipoCabanha", "TipoCabanha")
                        .WithMany()
                        .HasForeignKey("IdTipoCabanha")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Hotel.LogicaNegocio.Entidades.Foto", "MisFotos", b1 =>
                        {
                            b1.Property<int>("IdCabanha")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("NombreFoto")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Secuenciador")
                                .HasColumnType("int");

                            b1.HasKey("IdCabanha", "Id");

                            b1.ToTable("FotosCabanhas");

                            b1.WithOwner("MiCabanha")
                                .HasForeignKey("IdCabanha");

                            b1.Navigation("MiCabanha");
                        });

                    b.Navigation("MisFotos");

                    b.Navigation("TipoCabanha");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.Mantenimiento", b =>
                {
                    b.HasOne("LogicaNegocio.Entidades.TipoCabanha", "TipoCabanha")
                        .WithMany("MisMantenimientos")
                        .HasForeignKey("IdCabanha")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoCabanha");
                });

            modelBuilder.Entity("LogicaNegocio.Entidades.TipoCabanha", b =>
                {
                    b.Navigation("MisMantenimientos");
                });
#pragma warning restore 612, 618
        }
    }
}