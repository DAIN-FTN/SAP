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
                .HasAlternateKey(x => x.Name);

            modelBuilder.Entity<StockLocation>()
               .HasAlternateKey(x => x.Code);
        }

        public DbSet<BakingProgram> BakingProgram { get; }
        public DbSet<Order> Order { get;  }
        public DbSet<Product> Product { get; }
        public DbSet<Oven> Oven { get;  }
        public DbSet<StockedProduct> StockedProduct { get;}
        public DbSet<StockLocation> StockLocation { get; }
        public DbSet<ReservedOrderProduct> ReservedOrderProduct { get;  }
        public DbSet<ProductToPrepare> ProductToPrepare { get; }
        public DbSet<Customer> Customer { get; }
        public DbSet<User> User { get; }
    }
    
}
