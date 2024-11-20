using FIAP.IRRIGACAO.API.Models;
using Microsoft.EntityFrameworkCore;
namespace FIAP.IRRIGACAO.API.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<FaucetModel> Faucet { get; set; }
        public DbSet<LocationModel> Location { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FaucetModel>(entity => {
                entity.ToTable("Faucet");
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Name).IsRequired();
                entity.Property(f => f.IsEnabled).IsRequired();
                entity.HasOne(f => f.Location).WithMany().HasForeignKey("LocationId").IsRequired(); ;
            });

            modelBuilder.Entity<LocationModel>(entity => {
                entity.ToTable("Location");
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Name).IsRequired();
            });
        }
    }
}