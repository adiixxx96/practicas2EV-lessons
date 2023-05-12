using Microsoft.EntityFrameworkCore;
using videogames.Models;

namespace videogames.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<UserGame> UserGame { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User{Username = "admin", Password = "admin", Role = true, SignUpDate = DateTime.Now, Id=1}
            );
             modelBuilder.Entity<Game>().HasData(
                new Game { Id = 1, Name = "The legend of Zelda ", Description="Primer juego de la saga The Legend of Zelda", Quantity=10, Price=20}
             );
            modelBuilder.Entity<UserGame>().HasData(
                new UserGame { Id = 1, UserId = 1, GameId= 1 }
            );

        }
    }
}