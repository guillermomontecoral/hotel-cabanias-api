using Hotel.LogicaNegocio.Entidades;
using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAccesoDatos.EntityFramework
{
    public class HotelContext : DbContext
    {
        public DbSet<TipoCabanha> TipoCabanhas { get; set; }
        public DbSet<Cabanha> Cabanhas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Mantenimiento> Mantenimientos { get; set; }
        public DbSet<TopesDescripcion> TopesDescripciones { get; set; }

        public HotelContext(DbContextOptions options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //string cadenaConexion =
        //    //    @"SERVER=(localdb)\MSsqlLocaldb;
        //    //    DATABASE=OblHotelBD;
        //    //    INTEGRATED SECURITY=TRUE;
        //    //    ENCRYPT=False;
        //    //    MultipleActiveResultSets=true"; //Puede evitar problemas si no hay un certificado y se usa SSL

        //    //optionsBuilder.UseSqlServer(cadenaConexion)
        //    //    .EnableDetailedErrors();

        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cabanha>().OwnsMany(c => c.MisFotos);


            modelBuilder.Entity<Cabanha>()
                        .OwnsOne(c => c.Nombre, nomCab =>
                        {
                            nomCab.Property(n => n.Nombre).HasColumnName("Value");
                            nomCab.HasIndex(n => n.Nombre).IsUnique();
                        });

            modelBuilder.Entity<TipoCabanha>()
            .OwnsOne(c => c.Nombre, nomCab =>
            {
                nomCab.Property(n => n.Value).HasColumnName("Value");
                nomCab.HasIndex(n => n.Value).IsUnique();
            });

            //Para no eliminar en cascada
            //foreach(var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany( e => e.GetForeignKeys()))
            //{
            //    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            //}
        }
    }
}
