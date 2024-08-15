using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPISucursal.Models
{
    public partial class TestDBContext : IdentityDbContext
    {
        public TestDBContext()
        {
        }

        public TestDBContext(DbContextOptions<TestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblMonedaAh> TblMonedaAhs { get; set; }
        public virtual DbSet<TblSucursalAh> TblSucursalAhs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=ins-dllo-test-01.public.33e082952ab4.database.windows.net,3342; Database=TestDB; User ID=prueba; Password=pruebaconcepto;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<TblMonedaAh>(entity =>
            {
                entity.HasKey(e => e.MonId);

                entity.ToTable("TBL_MONEDA_AH");

                entity.Property(e => e.MonId).HasColumnName("MON_ID");

                entity.Property(e => e.MonActivo).HasColumnName("MON_ACTIVO");

                entity.Property(e => e.MonDescripcion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("MON_DESCRIPCION");
            });

            modelBuilder.Entity<TblSucursalAh>(entity =>
            {
                entity.HasKey(e => e.SucId)
                    .HasName("PK_TBL_SUCURSALES_AH");

                entity.ToTable("TBL_SUCURSAL_AH");

                entity.HasIndex(e => e.SucIdentificacion, "IX_Identificacion_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.SucId).HasColumnName("SUC_ID");

                entity.Property(e => e.MonId).HasColumnName("MON_ID");

                entity.Property(e => e.SucCodigo).HasColumnName("SUC_CODIGO");

                entity.Property(e => e.SucDescripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("SUC_DESCRIPCION");

                entity.Property(e => e.SucDirrecion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("SUC_DIRRECION");

                entity.Property(e => e.SucFechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("SUC_FECHA_CREACION");

                entity.Property(e => e.SucFechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("SUC_FECHA_MODIFICACION");

                entity.Property(e => e.SucIdentificacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SUC_IDENTIFICACION");

                entity.HasOne(d => d.Mon)
                    .WithMany(p => p.TblSucursalAhs)
                    .HasForeignKey(d => d.MonId)
                    .HasConstraintName("FK_TBL_SUCURSAL_AH_TBL_MONEDA_AH");
            });

            //OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
