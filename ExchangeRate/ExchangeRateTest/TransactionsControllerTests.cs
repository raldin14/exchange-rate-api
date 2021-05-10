using ExchangeRate.Controllers;
using ExchangeRate.Data;
using ExchangeRate.Data.Models;
using ExchangeRate.Data.Services;
using ExchangeRate.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateTest
{
    class TransactionsControllerTests
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "ExchangeRateDbControllerTest")
            .Options;

        AppDbContext context;
        TransactionService transactionService;
        TransactionController transactionController;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();

            transactionService = new TransactionService(context);
            transactionController = new TransactionController(transactionService);
        }

        [Test, Order(1)]
        public void HTTP_GetAllTransactionsTest()
        {
            IActionResult actionResult = transactionController.GetAllTransactions();

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as List<Transaction>;

            Assert.That(actionResultData.First().UserId, Is.EqualTo(1));

            Assert.That(actionResultData.Count, Is.EqualTo(3));
        }

        [Test, Order(2)]
        public void HTTP_GetAllTransactionsByUserTest()
        {
            IActionResult actionResult = transactionController.GetAllTransactionsByUser(1);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as List<Transaction>;

            Assert.That(actionResultData.First().UserId, Is.EqualTo(1));

            Assert.That(actionResultData.Count, Is.EqualTo(2));
        }

        [Test, Order(3)]
        public void HTTP_GetAllTransactionsByUserNotfoundTest()
        {
            IActionResult actionResult = transactionController.GetAllTransactionsByUser(3);

            Assert.That(actionResult, Is.TypeOf<NotFoundResult>());
        }

        [Test, Order(4)]
        public void HTTP_GetAllTransactionsByDateTest()
        {
            IActionResult actionResult = transactionController.GetAllTransactionsByDate(DateTime.Now.Date);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as List<Transaction>;

            Assert.That(actionResultData.Count, Is.EqualTo(3));
        }

        [Test, Order(5)]
        public void HTTP_GetAllTransactionsByDateNotfoundTest()
        {
            IActionResult actionResult = transactionController.GetAllTransactionsByDate(DateTime.Now.AddDays(10));

            Assert.That(actionResult, Is.TypeOf<NotFoundResult>());
        }

        [Test, Order(6)]
        public void HTTP_GetAllTransactionsByUserAndDateTest()
        {
            IActionResult actionResult = transactionController.GetAllTransactionsByUserAndDate(2, DateTime.Now.Date);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as List<Transaction>;

            Assert.That(actionResultData.Count, Is.EqualTo(1));
        }

        [Test, Order(7)]
        public void HTTP_GetAllTransactionsByUserAndDateNotfoundTest()
        {
            IActionResult actionResult = transactionController.GetAllTransactionsByUserAndDate(1, DateTime.Now.AddDays(10));

            Assert.That(actionResult, Is.TypeOf<NotFoundResult>());
        }

        [Test, Order(8)]
        public void HTTPPOST_AddTransactionTest()
        {
            var newTransaction = new TransactionVM()
            {
                UserId = 2,
                Monto = 1000,
                Exchange_rate = float.Parse("10.98"),
                Iso_Code = "BRL",
                Purchased_Date = DateTime.Now.Date
            };

            IActionResult actionResult = transactionController.AddTransaction(newTransaction);

            Assert.That(actionResult, Is.TypeOf<OkResult>());
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
