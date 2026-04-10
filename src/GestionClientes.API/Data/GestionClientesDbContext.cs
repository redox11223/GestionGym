using System;
using GestionClientes.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionClientes.API.Data;

public class GestionClientesDbContext:DbContext
{
    public GestionClientesDbContext(DbContextOptions<GestionClientesDbContext> options) : base(options)
    {
    }

    // DbSet para cada entidad
    public DbSet<Socios> Socios { get; set; }
    public DbSet<SocioEntrenador> SocioEntrenador { get; set; }
    public DbSet<Entrenadores> Entrenadores { get; set; }
    public DbSet<Membresias> Membresias { get; set; }
    public DbSet<SocioMembresia> SocioMembresia { get; set; }
    public DbSet<Asistencias> Asistencias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Foreign key único para UsuarioId en Socios
        modelBuilder.Entity<Socios>()
            .HasIndex(s => s.UsuarioId)
            .IsUnique();
        //Foreign key único para UsuarioId en Entrenadores
        modelBuilder.Entity<Entrenadores>()
            .HasIndex(e => e.UsuarioId)
            .IsUnique();            
        // Configuración de la entidad SocioEntrenador
        modelBuilder.Entity<SocioEntrenador>(entity =>
        {
            entity.HasKey(se => new { se.SocioId, se.EntrenadorId });

            entity.HasOne(se=> se.Socio)
                  .WithMany(s=> s.SocioEntrenadores) 
                  .HasForeignKey(se => se.SocioId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(se=> se.Entrenador)
                  .WithMany(e => e.SocioEntrenadores)
                  .HasForeignKey(se => se.EntrenadorId)
                  .OnDelete(DeleteBehavior.Cascade); 
        });

        modelBuilder.Entity<Membresias>()
            .Property(m => m.Precio)
            .HasPrecision(18, 2);
        modelBuilder.Entity<SocioMembresia>()
            .Property(sm => sm.MontoPagado)
            .HasPrecision(18, 2);  
    }
}
