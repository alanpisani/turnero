using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using Turnero.Models;

namespace Turnero.Data;

public partial class TurneroContext : DbContext
{
    public TurneroContext()
    {
    }

    public TurneroContext(DbContextOptions<TurneroContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuthToken> AuthTokens { get; set; }

    public virtual DbSet<Consultum> Consulta { get; set; }

    public virtual DbSet<Especialidad> Especialidads { get; set; }

    public virtual DbSet<HistorialTurno> HistorialTurnos { get; set; }

    public virtual DbSet<HorarioLaboral> HorarioLaborals { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Profesional> Profesionals { get; set; }

    public virtual DbSet<ProfesionalEspecialidad> ProfesionalEspecialidads { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=turnero;user=Alancio;password=callefalsa123", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AuthToken>(entity =>
        {
            entity.HasKey(e => e.IdAuthToken).HasName("PRIMARY");

            entity.ToTable("auth_token");

            entity.HasIndex(e => e.IdUsuario, "id_usuario");

            entity.Property(e => e.IdAuthToken).HasColumnName("id_auth_token");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.Expiracion)
                .HasColumnType("datetime")
                .HasColumnName("expiracion");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Token)
                .HasMaxLength(500)
                .HasColumnName("token");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.AuthTokens)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auth_token_ibfk_1");
        });

        modelBuilder.Entity<Consultum>(entity =>
        {
            entity.HasKey(e => e.IdConsulta).HasName("PRIMARY");

            entity.ToTable("consulta");

            entity.HasIndex(e => e.IdPaciente, "id_paciente");

            entity.HasIndex(e => e.IdProfesional, "id_profesional");

            entity.Property(e => e.IdConsulta).HasColumnName("id_consulta");
            entity.Property(e => e.Diagnostico)
                .HasMaxLength(50)
                .HasColumnName("diagnostico");
            entity.Property(e => e.FechaConsulta).HasColumnName("fecha_consulta");
            entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");
            entity.Property(e => e.IdProfesional).HasColumnName("id_profesional");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(250)
                .HasColumnName("observaciones");
            entity.Property(e => e.Tratamiento)
                .HasMaxLength(100)
                .HasColumnName("tratamiento");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consulta_ibfk_1");

            entity.HasOne(d => d.IdProfesionalNavigation).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.IdProfesional)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consulta_ibfk_2");
        });

        modelBuilder.Entity<Especialidad>(entity =>
        {
            entity.HasKey(e => e.IdEspecialidad).HasName("PRIMARY");

            entity.ToTable("especialidad");

            entity.Property(e => e.IdEspecialidad).HasColumnName("id_especialidad");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.NombreEspecialidad)
                .HasMaxLength(25)
                .HasColumnName("nombre_especialidad");
        });

        modelBuilder.Entity<HistorialTurno>(entity =>
        {
            entity.HasKey(e => e.IdHistorialTurno).HasName("PRIMARY");

            entity.ToTable("historial_turno");

            entity.HasIndex(e => e.IdTurno, "id_turno");

            entity.HasIndex(e => e.IdUsuario, "id_usuario");

            entity.Property(e => e.IdHistorialTurno).HasColumnName("id_historial_turno");
            entity.Property(e => e.FechaCambio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_cambio");
            entity.Property(e => e.IdTurno).HasColumnName("id_turno");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Motivo)
                .HasMaxLength(100)
                .HasColumnName("motivo");
            entity.Property(e => e.TipoAccion)
                .HasDefaultValueSql("'Creación'")
                .HasColumnType("enum('Creación','Modificación','Cancelación','Ausencia')")
                .HasColumnName("tipo_accion");

            entity.HasOne(d => d.IdTurnoNavigation).WithMany(p => p.HistorialTurnos)
                .HasForeignKey(d => d.IdTurno)
                .HasConstraintName("historial_turno_ibfk_4");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.HistorialTurnos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("historial_turno_ibfk_3");
        });

        modelBuilder.Entity<HorarioLaboral>(entity =>
        {
            entity.HasKey(e => e.IdHorarioLaboral).HasName("PRIMARY");

            entity.ToTable("horario_laboral");

            entity.HasIndex(e => e.IdProfesional, "horario_laboral_ibfk_1");

            entity.Property(e => e.IdHorarioLaboral).HasColumnName("id_horario_laboral");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.DiaSemana).HasColumnName("dia_semana");
            entity.Property(e => e.DuracionTurno)
                .HasDefaultValueSql("'30'")
                .HasColumnName("duracion_turno");
            entity.Property(e => e.HoraFin)
                .HasColumnType("time")
                .HasColumnName("hora_fin");
            entity.Property(e => e.HoraInicio)
                .HasColumnType("time")
                .HasColumnName("hora_inicio");
            entity.Property(e => e.IdProfesional).HasColumnName("id_profesional");

            entity.HasOne(d => d.IdProfesionalNavigation).WithMany(p => p.HorarioLaborals)
                .HasForeignKey(d => d.IdProfesional)
                .HasConstraintName("horario_laboral_ibfk_1");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("paciente");

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnName("id_usuario");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Paciente)
                .HasForeignKey<Paciente>(d => d.IdUsuario)
                .HasConstraintName("paciente_ibfk_1");
        });

        modelBuilder.Entity<Profesional>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("profesional");

            entity.HasIndex(e => e.Matricula, "matricula").IsUnique();

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnName("id_usuario");
            entity.Property(e => e.Matricula).HasColumnName("matricula");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Profesional)
                .HasForeignKey<Profesional>(d => d.IdUsuario)
                .HasConstraintName("profesional_ibfk_1");
        });

        modelBuilder.Entity<ProfesionalEspecialidad>(entity =>
        {
            entity.HasKey(e => e.IdProfesionalEspecialidad).HasName("PRIMARY");

            entity.ToTable("profesional_especialidad");

            entity.HasIndex(e => e.IdEspecialidad, "id_especialidad");

            entity.HasIndex(e => e.IdProfesional, "id_profesional");

            entity.Property(e => e.IdProfesionalEspecialidad).HasColumnName("id_profesional_especialidad");
            entity.Property(e => e.IdEspecialidad).HasColumnName("id_especialidad");
            entity.Property(e => e.IdProfesional).HasColumnName("id_profesional");

            entity.HasOne(d => d.IdEspecialidadNavigation).WithMany(p => p.ProfesionalEspecialidads)
                .HasForeignKey(d => d.IdEspecialidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("profesional_especialidad_ibfk_2");

            entity.HasOne(d => d.IdProfesionalNavigation).WithMany(p => p.ProfesionalEspecialidads)
                .HasForeignKey(d => d.IdProfesional)
                .HasConstraintName("profesional_especialidad_ibfk_1");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.IdTurno).HasName("PRIMARY");

            entity.ToTable("turno");

            entity.HasIndex(e => e.IdEspecialidad, "id_especialidad");

            entity.HasIndex(e => e.IdPaciente, "id_paciente");

            entity.HasIndex(e => e.IdProfesional, "id_profesional");

            entity.Property(e => e.IdTurno).HasColumnName("id_turno");
            entity.Property(e => e.EstadoTurno)
                .HasDefaultValueSql("'Solicitado'")
                .HasColumnType("enum('Solicitado','Confirmado','Reprogramado','Cancelado','Ausente','Atendido')")
                .HasColumnName("estado_turno");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaTurno)
                .HasColumnType("datetime")
                .HasColumnName("fecha_turno");
            entity.Property(e => e.IdEspecialidad).HasColumnName("id_especialidad");
            entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");
            entity.Property(e => e.IdProfesional).HasColumnName("id_profesional");

            entity.HasOne(d => d.IdEspecialidadNavigation).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.IdEspecialidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("turno_ibfk_4");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.IdPaciente)
                .HasConstraintName("turno_ibfk_2");

            entity.HasOne(d => d.IdProfesionalNavigation).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.IdProfesional)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("turno_ibfk_3");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Dni, "dni").IsUnique();

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Apellido)
                .HasMaxLength(20)
                .HasColumnName("apellido");
            entity.Property(e => e.Dni).HasColumnName("dni");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .HasColumnName("email");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Rol)
                .HasDefaultValueSql("'Paciente'")
                .HasColumnType("enum('Paciente','Profesional','Recepcionista','Admin')")
                .HasColumnName("rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
