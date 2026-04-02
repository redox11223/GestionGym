using System;
using Autenticacion.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Autenticacion.API.Data;

public class AutenticacionDbContext:DbContext
{
    public AutenticacionDbContext(DbContextOptions<AutenticacionDbContext> options): base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Rol> Roles { get; set; } 
    public DbSet<UsuarioRol> UsuarioRoles { get; set; } 

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UsuarioRol>()
            .HasKey(ur => new { ur.UsuarioId, ur.RolId });

        modelBuilder.Entity<UsuarioRol>()
            .HasOne(ur => ur.Usuario)
            .WithMany(u => u.UsuarioRoles)
            .HasForeignKey(ur => ur.UsuarioId);

        modelBuilder.Entity<UsuarioRol>()
            .HasOne(ur => ur.Rol)
            .WithMany()
            .HasForeignKey(ur => ur.RolId);
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
