using Microsoft.EntityFrameworkCore;
using SAP_API.Models;

namespace SAP_API.DataAccess.DbContexts
{
    //TODO: Change to a more suitable name
    public class BakeryContext: DbContext
    {
        public BakeryContext(DbContextOptions<BakeryContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetUniqueConstraints(modelBuilder);
            DataSeeder.SeedData(modelBuilder);
        }

        private void SetUniqueConstraints(ModelBuilder modelBuilder) {
            modelBuilder.Entity<BakingProgram>()
               .HasAlternateKey(x => x.Code);

            modelBuilder.Entity<Oven>()
                .HasAlternateKey(x => x.Code);

            modelBuilder.Entity<Product>()
                .HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<StockLocation>()
               .HasAlternateKey(x => x.Code);

            modelBuilder.Entity<Role>()
                .HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<StockedProduct>()
                .HasAlternateKey(x => new { x.ProductId, x.LocationId })
                .HasName("UQ_ProductId_LocationId");

        }

        public DbSet<BakingProgram> BakingProgram { get; }
        public DbSet<Order> Order { get;  }
        public DbSet<Product> Product { get; }
        public DbSet<Oven> Oven { get;  }
        public DbSet<StockedProduct> StockedProduct { get;}
        public DbSet<StockLocation> StockLocation { get; }
        public DbSet<ReservedOrderProduct> ReservedOrderProduct { get;  }
        public DbSet<ProductToPrepare> ProductToPrepare { get; }
        public DbSet<Role> Role { get; }
        public DbSet<User> User { get;  }
    }
    
}
