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
                new User{Username = "admin", Password = "admin", Role = true, SignUpDate = DateTime.Now, Id=1},
                new User{Username = "edu", Password = "edu12345", Role = false, SignUpDate = DateTime.Now, Id=2}

            );
             modelBuilder.Entity<Game>().HasData(
                new Game { Id = 1, Name = "The legend of Zelda ", Description="Primer juego de la saga The Legend of Zelda", Quantity=10, Price=20},
                new Game { Id = 2, Name = "Zelda: Tears of the Kingdom ", Description="Secuela de Zelda Breath of the Wild", Quantity=50, Price=60},
                new Game { Id = 3, Name = "Super Mario Oddisey ", Description="Juego de Mario 3D", Quantity=30, Price=50},
                new Game { Id = 4, Name = "Pokemon Púrpura ", Description="Pokemon edición Españita", Quantity=25, Price=40},
                new Game { Id = 5, Name = "El juego de Dora Exploradora ", Description="Dora la Exploradora vuelve para switch", Quantity=15, Price=10}
             );
        }
    }
}