using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TransactionTestAPI.Models
{
    public partial class TransactionDBContext : DbContext
    {
        public TransactionDBContext()
        {
        }

        public TransactionDBContext(DbContextOptions<TransactionDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Cuenta> Cuentas { get; set; } = null!;
        public virtual DbSet<Movimiento> Movimientos { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<TiposCuentum> TiposCuenta { get; set; } = null!;
        public virtual DbSet<TiposMovimiento> TiposMovimientos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("TransactionDBConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.Clave).HasMaxLength(16);

                entity.Property(e => e.PersonaId).HasColumnName("PersonaID");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Persona_Cliente");
            });

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.Property(e => e.CuentaId).HasColumnName("CuentaID");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.NumeroCuenta).HasMaxLength(25);

                entity.Property(e => e.SaldoInicial).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TipoId).HasColumnName("TipoID");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cliente_Cuentas");

                entity.HasOne(d => d.Tipo)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.TipoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tipo_Cuentas");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.Property(e => e.MovimientoId).HasColumnName("MovimientoID");

                entity.Property(e => e.CuentaId).HasColumnName("CuentaID");

                entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TipoId).HasColumnName("TipoID");

                entity.HasOne(d => d.Cuenta)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.CuentaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cuenta_Movimientos");

                entity.HasOne(d => d.Tipo)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.TipoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tipo_Movimientos");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.Property(e => e.PersonaId).HasColumnName("PersonaID");

                entity.Property(e => e.Direccion).HasMaxLength(150);

                entity.Property(e => e.Genero).HasMaxLength(1);

                entity.Property(e => e.Identificacion).HasMaxLength(14);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Telefono).HasMaxLength(10);
            });

            modelBuilder.Entity<TiposCuentum>(entity =>
            {
                entity.HasKey(e => e.TipoCuentaId);

                entity.Property(e => e.TipoCuentaId).HasColumnName("TipoCuentaID");

                entity.Property(e => e.Nombre).HasMaxLength(25);
            });

            modelBuilder.Entity<TiposMovimiento>(entity =>
            {
                entity.HasKey(e => e.TipoId);

                entity.ToTable("TiposMovimiento");

                entity.Property(e => e.TipoId).HasColumnName("TipoID");

                entity.Property(e => e.Nombre).HasMaxLength(25);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
