using FIAP.IRRIGACAO.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FIAP.IRRIGACAO.API.Data.Context
{
    public class OracleContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<FaucetModel> Faucet { get; set; }
        public DbSet<LocationModel> Location { get; set; }
        public OracleContext(DbContextOptions<OracleContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.GetProperties()
                    .Where(p => p.ClrType == typeof(bool) || p.ClrType == typeof(bool?));

                foreach (var property in properties)
                {
                    property.SetColumnType("NVARCHAR2(10)");
                }
            }

            modelBuilder.Entity<FaucetModel>(entity => {
                entity.ToTable("Faucet");
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Name).IsRequired();
                entity.Property(f => f.IsEnabled).IsRequired();
                entity.HasOne(f => f.Location).WithMany().HasForeignKey("LocationId").IsRequired();
            });

            modelBuilder.Entity<LocationModel>(entity => {
                entity.ToTable("Location");
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Name).IsRequired();
            });
        }
    }
}
