using CoinApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace CoinApplication.Data
{
    public sealed class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<Money> Monies { get; set; }

        public DbSet<ExchangeOperation> ExchangeOperations { get; set; }

        public DbSet<ExchangeOperationItem> ExchangeOperationItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Money>().HasData(new object[]
            {
                new 
                {
                    Id = 1,
                    Amount = 100,
                    Acceptable = false,
                    DenominationValue = 50,
                    MaxAmount = 20
                },
                new 
                {
                    Id = 2,
                    Amount = 100,
                    Acceptable = false,
                    DenominationValue = 100,
                    MaxAmount = 10
                },
                new 
                {
                    Id = 3,
                    Amount = 0,
                    Acceptable = true,
                    DenominationValue = 5,
                    MaxAmount = 100
                },
                new 
                {
                    Id = 4,
                    Amount = 0,
                    Acceptable = true,
                    DenominationValue = 10,
                    MaxAmount = 50
                }
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
