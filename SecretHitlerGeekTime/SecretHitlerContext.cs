using Microsoft.EntityFrameworkCore;
using SecretHitlerGeekTime.Models;
using SecretHitlerGeekTime.Models.WinLoss;
using SecretHitlerGeekTime.Models.JoinTables;
namespace SecretHitlerGeekTime
{
    
    public class SecretHitlerContext: DbContext
    {

     

        public DbSet<PlayerGame> playerGames { get; set; }

        public DbSet<Player> players { get; set; }

        public DbSet<Game> games { get; set; }


        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerGame>().HasOne(a => a.Player).WithMany(a => a.Games).HasForeignKey(a=>a.PlayerName);
            modelBuilder.Entity<PlayerGame>().HasOne(a => a.Game).WithMany(a => a.players).HasForeignKey(a => a.GameId);
            modelBuilder.Entity<Win>().HasOne(a => a.Player).WithMany(a => a.Wins).HasForeignKey(a => a.PlayerName);
            modelBuilder.Entity<Win>().HasOne(a => a.Game).WithMany(a => a.wins).HasForeignKey(a => a.GameId);
            modelBuilder.Entity<Loss>().HasOne(a => a.Player).WithMany(a => a.Losses).HasForeignKey(a => a.PlayerName);
            modelBuilder.Entity<Loss>().HasOne(a => a.Game).WithMany(a => a.losses).HasForeignKey(a => a.GameId);
            modelBuilder.Entity<Game>().Property(a=>a.auto_increment_index).ValueGeneratedOnAdd();
            modelBuilder.Entity<Player>().Property(a => a.auto_increment_index).ValueGeneratedOnAdd();
        }
        }
}
