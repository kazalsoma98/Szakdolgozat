using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OwinWebApi.DB.Models;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OwinWebApi.DB
{
    public partial class SolarBoatContext : DbContext
    {
        public SolarBoatContext()
        {
        }

        public SolarBoatContext(DbContextOptions<SolarBoatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Data> Data { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string constr = ConfigurationManager.AppSettings["connectionstring"];
                optionsBuilder.UseSqlServer(constr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Data>(entity =>
            {
                entity.Property(e => e.Idopont).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
