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

        // Configuración de la entidad SocioEntrenador
        modelBuilder.Entity<SocioEntrenador>(entity =>
        {
            entity.HasKey(se => new { se.SocioId, se.EntrenadorId });

            entity.HasOne<Socios>()
                  .WithMany() 
                  .HasForeignKey(se => se.SocioId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<Entrenadores>()
                  .WithMany()
                  .HasForeignKey(se => se.EntrenadorId)
                  .OnDelete(DeleteBehavior.ClientSetNull); 
        });

        modelBuilder.Entity<Membresias>()
            .Property(m => m.Precio)
            .HasPrecision(18, 2);

    }
}
