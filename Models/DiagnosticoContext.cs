using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiCentroMedico.Models;

public partial class DiagnosticoContext : DbContext
{
    public DiagnosticoContext()
    {
    }

    public DiagnosticoContext(DbContextOptions<DiagnosticoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Especialidade> Especialidades { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<ObrasSociale> ObrasSociales { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Especialidade>(entity =>
        {
            entity.HasKey(e => e.Idespecialidad).HasName("PK__ESPECIAL__08AA2A04ED2C6DDD");

            entity.ToTable("ESPECIALIDADES");

            entity.Property(e => e.Idespecialidad)
                .ValueGeneratedNever()
                .HasColumnName("IDESPECIALIDAD");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AI")
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.Idmedico).HasName("PK__MEDICOS__B8874AE4F9830AB9");

            entity.ToTable("MEDICOS");

            entity.Property(e => e.Idmedico)
                .ValueGeneratedNever()
                .HasColumnName("IDMEDICO");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("APELLIDO");
            entity.Property(e => e.CostoConsulta)
                .HasColumnType("money")
                .HasColumnName("COSTO_CONSULTA");
            entity.Property(e => e.Fechaingreso).HasColumnName("FECHAINGRESO");
            entity.Property(e => e.Fechanac).HasColumnName("FECHANAC");
            entity.Property(e => e.Idespecialidad).HasColumnName("IDESPECIALIDAD");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SEXO");

            entity.HasOne(d => d.IdespecialidadNavigation).WithMany(p => p.Medicos)
                .HasForeignKey(d => d.Idespecialidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MEDICOS__IDESPEC__403A8C7D");
        });

        modelBuilder.Entity<ObrasSociale>(entity =>
        {
            entity.HasKey(e => e.Idobrasocial).HasName("PK__OBRAS_SO__F287F6AFBEBCD7B2");

            entity.ToTable("OBRAS_SOCIALES");

            entity.Property(e => e.Idobrasocial)
                .ValueGeneratedNever()
                .HasColumnName("IDOBRASOCIAL");
            entity.Property(e => e.Cobertura)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("COBERTURA");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.Idpaciente).HasName("PK__PACIENTE__D1D53BB9EAFCA454");

            entity.ToTable("PACIENTES");

            entity.Property(e => e.Idpaciente)
                .ValueGeneratedNever()
                .HasColumnName("IDPACIENTE");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AI")
                .HasColumnName("APELLIDO");
            entity.Property(e => e.Fechanac).HasColumnName("FECHANAC");
            entity.Property(e => e.Idobrasocial).HasColumnName("IDOBRASOCIAL");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AI")
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SEXO");

            entity.HasOne(d => d.IdobrasocialNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.Idobrasocial)
                .HasConstraintName("FK__PACIENTES__IDOBR__3A81B327");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.Idturno).HasName("PK__TURNOS__95839F86D15CB64D");

            entity.ToTable("TURNOS");

            entity.Property(e => e.Idturno)
                .ValueGeneratedNever()
                .HasColumnName("IDTURNO");
            entity.Property(e => e.Duracion).HasColumnName("DURACION");
            entity.Property(e => e.Fechahora)
                .HasColumnType("datetime")
                .HasColumnName("FECHAHORA");
            entity.Property(e => e.Idmedico).HasColumnName("IDMEDICO");
            entity.Property(e => e.Idpaciente).HasColumnName("IDPACIENTE");

            entity.HasOne(d => d.IdmedicoNavigation).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.Idmedico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TURNOS__IDMEDICO__44FF419A");

            entity.HasOne(d => d.IdpacienteNavigation).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.Idpaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TURNOS__IDPACIEN__45F365D3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
