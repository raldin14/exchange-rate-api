using Microsoft.EntityFrameworkCore;
using ExchangeRate.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using ExchangeRate.Data.Models;
using ExchangeRate.Data.Services;
using ExchangeRate.Data.ViewModels;

namespace ExchangeRateTest
{
    public class TransacctionserviceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "ExchangeRateDbTest")
            .Options;

        AppDbContext context;
        TransactionService transactionService;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();

            transactionService = new TransactionService(context);
        }

        [Test, Order(1)]
        public void GetAllTransactions_WithoutParameters()
        {
            var result = transactionService.GetAllTransactions();

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.AreEqual(result.Count, 3);
        }

        [Test, Order(2)]
        public void GetAllTransactions_WithUserId()
        {
            var result = transactionService.GetAllTransactionsByUser(1);

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.AreEqual(result.Count, 2);
        }

        [Test, Order(3)]
        public void GetAllTransactions_WithWrongUserId()
        {
            var result = transactionService.GetAllTransactionsByUser(3);

            Assert.That(result.Count, Is.EqualTo(0));
            Assert.AreEqual(result.Count, 0);
        }

        [Test, Order(4)]
        public void GetAllTransactions_WithDate()
        {
            var result = transactionService.GetAllTransactionsByDate(DateTime.Now.Date);

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.AreEqual(result.Count, 3);
        }

        [Test, Order(5)]
        public void GetAllTransactions_WithWrongDate()
        {
            var result = transactionService.GetAllTransactionsByDate(DateTime.Now.AddDays(10));

            Assert.That(result.Count, Is.EqualTo(0));
            Assert.AreEqual(result.Count, 0);
        }

        [Test, Order(6)]
        public void GetAllTransactions_WithWrongIdAndWrongDate()
        {
            var result = transactionService.GetAllTransactionsByUserAndDate(0, DateTime.Now.AddDays(10));

            Assert.That(result.Count, Is.EqualTo(0));
            Assert.AreEqual(result.Count, 0);
        }

        [Test, Order(7)]
        public void GetAllTransactions_WithRightIdAndWrongDate()
        {
            var result = transactionService.GetAllTransactionsByUserAndDate(2, DateTime.Now.AddDays(10));

            Assert.That(result.Count, Is.EqualTo(0));
            Assert.AreEqual(result.Count, 0);
        }

        [Test, Order(8)]
        public void GetAllTransactions_WithRightIdAndRightDate()
        {
            var result = transactionService.GetAllTransactionsByUserAndDate(2, DateTime.Now.Date);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.AreEqual(result.Count, 1);
        }

        [Test]
        public void Add_Transaction()
        {
            
            var newTransaction = new TransactionVM()
            {
                UserId = 2,
                Monto = 1000,
                Exchange_rate = float.Parse("10.98"),
                Iso_Code = "BRL",
                Purchased_Date = DateTime.Now.Date
            };

            transactionService.AddTransaction(newTransaction);

            Assert.Pass();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var transactions = new List<Transaction>
            {
                new Transaction()
                {
                    Id = 1,
                    UserId = 1,
                    Monto = 1000,
                    Exchange_rate = float.Parse("10.98"),
                    Iso_Code = "USD",
                    Purchased_Date = DateTime.Now.Date
                },
                new Transaction()
                {
                    Id = 2,
                    UserId = 1,
                    Monto = 1000,
                    Exchange_rate = float.Parse("10.98"),
                    Iso_Code = "BRL",
                    Purchased_Date = DateTime.Now.Date
                },
                new Transaction()
                {
                    Id = 3,
                    UserId = 2,
                    Monto = 1000,
                    Exchange_rate = float.Parse("10.98"),
                    Iso_Code = "USD",
                    Purchased_Date = DateTime.Now.Date
                },
            };

            context.Transactions.AddRange(transactions);
            context.SaveChanges();
        }
    }
}