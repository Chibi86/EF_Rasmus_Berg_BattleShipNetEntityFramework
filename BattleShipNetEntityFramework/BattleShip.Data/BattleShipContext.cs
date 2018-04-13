using Microsoft.EntityFrameworkCore;
using BattleShip.Domain;
using System.Configuration;

namespace BattleShip.Data
{
    public class BattleShipContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<GameBoard> GameBoards { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Boat> Boats { get; set; }
        public DbSet<BoatType> BoatTypes { get; set; }
        public DbSet<Position> Positions { get; set; }

        public DbSet<PlayerHit> PlayerHits { get; set; }
        public DbSet<PlayerBoat> PlayerBoats { get; set; }
        public DbSet<BoatHit> BoatHits { get; set; }
        public DbSet<BoatPosition> BoatPositions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().Ignore(a => a.GameBoards);

            modelBuilder.Entity<GameBoard>()
                .HasKey(g => new { g.GameKey });

            modelBuilder.Entity<GameBoard>()
                .HasOne(g => g.TurnPlayer)
                .WithMany()
                .HasForeignKey(g => g.TurnPlayerId);

            modelBuilder.Entity<GameBoard>()
            .HasMany(g => g.Players)
            .WithOne(p => p.GameBoard);

            modelBuilder.Entity<Position>().HasIndex(p => new { p.X, p.Y }).IsUnique();

            modelBuilder.Entity<PlayerHit>().HasKey(ph => new { ph.PlayerId, ph.PositionId });
            modelBuilder.Entity<PlayerBoat>().HasKey(pb => new { pb.PlayerId, pb.BoatId });
            modelBuilder.Entity<BoatPosition>().HasKey(bp => new { bp.BoatId, bp.PositionId });
            modelBuilder.Entity<BoatHit>().HasKey(bh => new { bh.BoatId, bh.PositionId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["BattleShipContext"].ConnectionString);
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = BattleShipNet; Trusted_Connection = True;");
        }
    }
}