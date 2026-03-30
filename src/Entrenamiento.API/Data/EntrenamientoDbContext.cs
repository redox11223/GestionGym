using Microsoft.EntityFrameworkCore;
using Entrenamiento.API.Models.Entities;

namespace Entrenamiento.API.Data;

public class EntrenamientoDbContext : DbContext
{
    public EntrenamientoDbContext(DbContextOptions<EntrenamientoDbContext> options) : base(options)
    {
    }

    public DbSet<Ejercicios> Ejercicios { get; set; }
    public DbSet<Rutinas> Rutinas { get; set; }
    public DbSet<Rutina_Ejercicios> Rutina_Ejercicios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la entidad Rutina_Ejercicios
            modelBuilder.Entity<Rutina_Ejercicios>(entity =>
            {
               
                entity.HasKey(re => new { re.RutinaId, re.Orden });

                entity.ToTable(t => t.HasCheckConstraint("CK_RutinaEjercicios_Orden", "[Orden] > 0"));

                entity.HasOne<Rutinas>()
                      .WithMany() 
                      .HasForeignKey(re => re.RutinaId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<Ejercicios>()
                      .WithMany()
                      .HasForeignKey(re => re.EjercicioId)
                      .OnDelete(DeleteBehavior.ClientSetNull); 
            });
        }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<EntidadBase>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.FechaCreacion = DateTimeOffset.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.FechaModificacion = DateTimeOffset.UtcNow;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

}