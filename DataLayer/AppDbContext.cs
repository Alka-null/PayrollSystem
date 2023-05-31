//using EWallet.Entities.DbEntities;
using DataLayer.DTO.Utilities;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> accountDbOptions) : base(accountDbOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Cadre>()
            //    .WillCascadeOnDelete(false);
        }


            //    modelBuilder.Entity<Wallet>()
            //    .Property(b => b.AccountBalance)
            //    .HasPrecision(30, 6);

            //    modelBuilder.Entity<Transaction>()
            //    .Property(b => b.Amount)
            //    .HasPrecision(30, 6);

            //    modelBuilder.Entity<SettlementAccount>()
            //    .Property(b => b.AccountBalance)
            //    .HasPrecision(30, 6);

            //    modelBuilder.Entity<Customer>()
            //        .HasOne<ProfilePhoto>(s => s.ProfilePhoto)
            //        .WithOne(y => y.Customer)
            //        .HasForeignKey<ProfilePhoto>(y => y.CustomerId);


            //    modelBuilder.Entity<Wallet>()
            //     .HasOne<Customer>(s => s.Customer)
            //     .WithMany(g => g.Wallets)
            //     .HasForeignKey(s => s.CustomerId)
            //     .OnDelete(DeleteBehavior.Cascade);

            //    //modelBuilder.Entity<Transaction>()
            //    // .HasOne<Wallet>(s => s.Wallet)
            //    // .WithMany(g => g.Transactions)
            //    // .HasForeignKey(s => s.WalletId)
            //    // .OnDelete(DeleteBehavior.Cascade);

            //    modelBuilder.Entity<Currency>()
            //        .HasOne<CurrencyLogo>(s => s.CurrencyLogo)
            //        .WithOne(y => y.Currency)
            //        .HasForeignKey<CurrencyLogo>(y => y.CurrencyId);
            //}

            public DbSet<BasicSalary> BasicSalaries { get; set; }

            public DbSet<Cadre> Cadres { get; set; }

            public DbSet<Position> Positions { get; set; }

            public DbSet<Category> Categories { get; set; }

            public DbSet<Employee> Employees { get; set; }

            public DbSet<HousingAllowance> HousingAllowances { get; set; }

            public DbSet<Pension> Pensions { get; set; }
            public DbSet<Tax> TaxRates { get; set; }
            public DbSet<Transaction> Transactions { get; set; }
    }
    }

