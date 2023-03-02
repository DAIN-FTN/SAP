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
            modelBuilder.Entity<BakingProgram>()
                .HasAlternateKey(x => x.Code);

            modelBuilder.Entity<Oven>()
                .HasAlternateKey(x => x.Code);

            modelBuilder.Entity<Product>()
                .HasAlternateKey(x => x.Name);

            modelBuilder.Entity<StockLocation>()
               .HasAlternateKey(x => x.Code);

        }

        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Oven> Oven { get; set; }
        public DbSet<StockedProduct> StockedProduct { get; set;}
        public DbSet<StockLocation> StockLocation { get; set; }
        public DbSet<ReservedOrderProduct> ReservedOrderProduct { get; set; }
        public DbSet<ProductToPrepare> ProductToPrepare { get; set; }
    }
    
}
