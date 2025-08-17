using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NaureBack.Models;

public partial class NaureContext : DbContext
{
    public NaureContext()
    {
    }

    public NaureContext(DbContextOptions<NaureContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cita> Cita { get; set; }

    public virtual DbSet<Consulta> Consulta { get; set; }

    public virtual DbSet<Contratacion> Contratacions { get; set; }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<Localidad> Localidads { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Tramo> Tramos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=5CD3157JNL\\SQLEXPRESS;Database=Naure;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cita__3214EC07013325E2");

            entity.HasOne(d => d.IdContratacionNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdContratacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cita__IdContrata__37FA4C37");
        });

        modelBuilder.Entity<Consulta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Consulta__3214EC072150D6CA");

            entity.Property(e => e.Apellidos)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Consulta__IdClie__2E70E1FD");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.IdServicio)
                .HasConstraintName("FK__Consulta__IdServ__2D7CBDC4");
        });

        modelBuilder.Entity<Contratacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contrata__3214EC07C310E8C6");

            entity.ToTable("Contratacion");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Contratacions)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contratac__IdCli__324172E1");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Contratacions)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contratac__IdSer__314D4EA8");
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC0771C71451");

            entity.ToTable("Documento");

            entity.Property(e => e.Archivo)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Documentos)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__IdSer__27C3E46E");
        });

        modelBuilder.Entity<Localidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Localida__3214EC0703D50A41");

            entity.ToTable("Localidad");

            entity.Property(e => e.Geometria)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Servicio__3214EC076901ED05");

            entity.ToTable("Servicio");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.Imagen)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tramo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tramo__3214EC0720590C6F");

            entity.ToTable("Tramo");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Importe).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdContratacionNavigation).WithMany(p => p.Tramos)
                .HasForeignKey(d => d.IdContratacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tramo__IdContrat__351DDF8C");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC0718301C1D");

            entity.ToTable("Usuario");

            entity.Property(e => e.Apellidos)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CuentaBancaria)
                .HasMaxLength(24)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdLocalidadNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdLocalidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__IdLocal__2AA05119");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
