using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieAppWeb.Models;

namespace MovieAppWeb.Data;

public class MovieContext : IdentityDbContext
{
  public MovieContext(DbContextOptions<MovieContext> dbo)
      : base(dbo)
  {

  }
  public DbSet<Usuario> Usuarios { get; set; }
  public DbSet<Pelicula> Peliculas { get; set; }
  public DbSet<Noticia> Noticias { get; set; }
  public DbSet<Reseña> Reseñas { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación para evitar la eliminación en cascada
            modelBuilder.Entity<Reseña>()
                .HasOne(r => r.Usuario)  // Reseña tiene un Usuario
                .WithMany(u => u.Reseñas)  // Usuario tiene muchas Reseñas
                .HasForeignKey(r => r.UsuarioId)  // Clave foránea en Reseña
                .OnDelete(DeleteBehavior.Restrict);  // Evita la eliminación en cascada
        }
}
