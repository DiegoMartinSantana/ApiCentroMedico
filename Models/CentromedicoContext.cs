﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiCentroMedico.Models;

public partial class CentromedicoContext : DbContext
{
    public CentromedicoContext()
    {
    }

    public CentromedicoContext(DbContextOptions<CentromedicoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Especialidade> Especialidades { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<ObrasSociale> ObrasSociales { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Especialidade>(entity =>
        {
            entity.HasKey(e => e.Idespecialidad).HasName("PK__ESPECIAL__08AA2A04F27A03F6");

            entity.ToTable("ESPECIALIDADES");

            entity.Property(e => e.Idespecialidad).HasColumnName("IDESPECIALIDAD");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AI")
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.Idmedico).HasName("PK__MEDICOS__B8874AE42369B933");

            entity.ToTable("MEDICOS");

            entity.HasIndex(e => e.Dni, "UQ_DniMedicos").IsUnique();

            entity.Property(e => e.Idmedico).HasColumnName("IDMEDICO");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("APELLIDO");
            entity.Property(e => e.CostoConsulta)
                .HasColumnType("money")
                .HasColumnName("COSTO_CONSULTA");
            entity.Property(e => e.Dni).HasColumnName("DNI");
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
                .HasConstraintName("FK__MEDICOS__IDESPEC__44FF419A");
        });

        modelBuilder.Entity<ObrasSociale>(entity =>
        {
            entity.HasKey(e => e.Idobrasocial).HasName("PK__OBRAS_SO__F287F6AF53ECF107");

            entity.ToTable("OBRAS_SOCIALES");

            entity.Property(e => e.Idobrasocial).HasColumnName("IDOBRASOCIAL");
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
            entity.HasKey(e => e.Idpaciente).HasName("PK__PACIENTE__D1D53BB9280F99BB");

            entity.ToTable("PACIENTES");

            entity.HasIndex(e => e.Dni, "UQ_DniPaciente").IsUnique();

            entity.Property(e => e.Idpaciente).HasColumnName("IDPACIENTE");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AI")
                .HasColumnName("APELLIDO");
            entity.Property(e => e.Dni).HasColumnName("DNI");
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

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.Idpermiso);

            entity.ToTable("PERMISOS");

            entity.Property(e => e.Idpermiso).HasColumnName("IDPERMISO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.Idturno).HasName("PK__TURNOS__95839F86EAFB58AD");

            entity.ToTable("TURNOS");

            entity.Property(e => e.Idturno).HasColumnName("IDTURNO");
            entity.Property(e => e.Duracion).HasColumnName("DURACION");
            entity.Property(e => e.Fechahora)
                .HasColumnType("datetime")
                .HasColumnName("FECHAHORA");
            entity.Property(e => e.Idmedico).HasColumnName("IDMEDICO");
            entity.Property(e => e.Idpaciente).HasColumnName("IDPACIENTE");

            entity.HasOne(d => d.IdmedicoNavigation).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.Idmedico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TURNOS__IDMEDICO__49C3F6B7");

            entity.HasOne(d => d.IdpacienteNavigation).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.Idpaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TURNOS__IDPACIEN__4AB81AF0");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Email);

            entity.ToTable("USUARIOS");

            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IdMedico).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Pass)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMedicoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdMedico)
                .HasConstraintName("Fk_Usuario_Medico");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdPaciente)
                .HasConstraintName("Fk_Paciente_Usuario");

            entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdPermiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Permisos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
