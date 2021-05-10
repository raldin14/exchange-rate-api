using ExchangeRate.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasOne(c => c.Iso_Code)
                .WithMany(u => u.Transactions)
                .HasForeignKey(ci => ci.ExchangeId);

            modelBuilder.Entity<Transaction>()
                .HasOne(c => c.User)
                .WithMany(u => u.Transactions) 
                .HasForeignKey(ci => ci.UserId);
        }*/

        public DbSet<CurrencyExchange> CurrenciesExchange { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ForeignCurrency> ForeignCurrencies { get; set; }
    }
}
