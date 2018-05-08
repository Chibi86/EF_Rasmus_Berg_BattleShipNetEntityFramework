using Microsoft.EntityFrameworkCore;
using BattleShip.Domain;
using System.Configuration;

namespace BattleShip.Data
{
    public class BattleShipContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRank> AccountRanks { get; set; }
        public DbSet<GameBoard> GameBoards { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Boat> Boats { get; set; }
        public DbSet<BoatType> BoatTypes { get; set; }
        public DbSet<Position> Positions { get; set; }

        public DbSet<AccountRecovery> AccountRecoveries { get; set; }
        public DbSet<PlayerHit> PlayerHits { get; set; }
        public DbSet<BoatHit> BoatHits { get; set; }
        public DbSet<BoatPosition> BoatPositions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Ignore(a => a.GameBoards)
                .Property(a => a.Password)
                .HasField("_password")
                .IsRequired();

            modelBuilder.Entity<Account>()
                .Property(a => a.RankId)
                .HasDefaultValue(1);

            modelBuilder.Entity<GameBoard>()
                .HasKey(g => new { g.Key });

            modelBuilder.Entity<GameBoard>()
                .HasOne(g => g.TurnPlayer)
                .WithMany()
                .HasForeignKey(g => g.TurnPlayerId);

            modelBuilder.Entity<GameBoard>()
            .HasMany(g => g.Players)
            .WithOne(p => p.GameBoard);

            modelBuilder.Entity<Position>().HasIndex(p => new { p.X, p.Y }).IsUnique();

            modelBuilder.Entity<PlayerHit>().HasKey(ph => new { ph.PlayerId, ph.PositionId });
            modelBuilder.Entity<BoatPosition>().HasKey(bp => new { bp.BoatId, bp.PositionId });
            modelBuilder.Entity<BoatHit>().HasKey(bh => new { bh.BoatId, bh.PositionId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["BattleShipDB"].ConnectionString);
        }
    }
}