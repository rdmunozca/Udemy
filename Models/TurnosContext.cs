using Microsoft.EntityFrameworkCore;

namespace Turnos.Models
{
    public class TurnosContext : DbContext
    {
        public TurnosContext(DbContextOptions<TurnosContext> opciones)
        :base(opciones)
        {
        }

        public DbSet<Especialidad> Especialidad {get; set; }

        public DbSet<Paciente> Paciente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Especialidad>(entidad =>
            {
                entidad.ToTable("Especialidad");
                entidad.HasKey(e => e.IdEspecialidad);
                entidad.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(false);
            }
            );
            modelBuilder.Entity<Paciente>(entidad =>
            {
                entidad.ToTable("Paciente");
                entidad.HasKey(p => p.IdPaciente);
                entidad.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
                entidad.Property(p => p.Apellido)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
                entidad.Property(p => p.Direccion)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false);
                entidad.Property(p => p.Telefono)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
                entidad.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);
            });
        }
        
    }
}