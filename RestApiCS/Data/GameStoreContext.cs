using Microsoft.EntityFrameworkCore;
using RestApiCS.Entities;

namespace RestApiCS.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Genre> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(tb =>
        {
            tb.Property(g => g.Name).IsRequired();
            tb.Property(g => g.Price).HasColumnType("decimal(6,2)");
        });

        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Action" },
            new Genre { Id = 2, Name = "Adventure" },
            new Genre { Id = 3, Name = "RPG" },
            new Genre { Id = 4, Name = "Simulation" },
            new Genre { Id = 5, Name = "Strategy" }
        );
    }
}
