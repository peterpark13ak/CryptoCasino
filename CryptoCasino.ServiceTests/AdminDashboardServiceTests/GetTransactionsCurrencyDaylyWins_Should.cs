using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;
using WebCasino.Service.Exceptions;

namespace WebCasino.ServiceTests.AdminDashboardServiceTests
{
    [TestClass]
    public class GetTransactionsCurrencyDaylyWins_Should
    {
        [TestMethod]
        public async Task ThrowsNotValidDayInMonthException_WhenDayIsNegative()
        {
            var casinoContextMock = new Mock<CasinoContext>();
            var adminDashboardService = new AdminDashboardService(casinoContextMock.Object);

            await Assert.ThrowsExceptionAsync<NotValidDayInMonthException>(() => adminDashboardService.GetTransactionsCurrencyDaylyWins(0));
        }

        [TestMethod]
        public async Task ThrowsNotValidDayInMonthException_WhenDayIsBiggerThen31()
        {
            var casinoContextMock = new Mock<CasinoContext>();
            var adminDashboardService = new AdminDashboardService(casinoContextMock.Object);

            await Assert.ThrowsExceptionAsync<NotValidDayInMonthException>(() => adminDashboardService.GetTransactionsCurrencyDaylyWins(32));
        }

        [TestMethod]
        public async Task ReturnDaylyTotalUsd()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ReturnDaylyTotalUsd")
                .Options;

            var normalisedAmount = 10;
            var transactionType = new TransactionType()
            {
                Id = 1,
                Name = "Win"
            };

            var wallet = new Wallet()
            {
                Currency = new Currency()

            };
            var user = new User()
            {
                Wallet = wallet
            };

            var transaction = new Transaction()
            {
                CreatedOn = DateTime.Now,
                TransactionType = transactionType,
                User = user,
                NormalisedAmount = normalisedAmount
            };

            var currentDay = DateTime.Now.Day;
            using (var context = new CasinoContext(contextOptions))
            {
                context.Transactions.Add(transaction);
                var adminService = new AdminDashboardService(context);
                await context.SaveChangesAsync();

                var result = await adminService.GetTransactionsCurrencyDaylyWins(currentDay);

                Assert.AreEqual(normalisedAmount, result.DaylyTotalUSD);

            }
        }

        [TestMethod]
        public async Task ReturnDaylyTotalInBGN()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ReturnDaylyTotalInBGN")
                .Options;

            var normalisedAmount = 10;
            var transactionType = new TransactionType()
            {
                Id = 1,
                Name = "Win"
            };

            var wallet = new Wallet()
            {
                Currency = new Currency()
                {
                    Name = "BGN"
                }
            };
            var user = new User()
            {
                Wallet = wallet
            };

            var transaction = new Transaction()
            {
                CreatedOn = DateTime.Now,
                TransactionType = transactionType,
                User = user,
                NormalisedAmount = normalisedAmount
            };

            var currentDay = DateTime.Now.Day;
            using (var context = new CasinoContext(contextOptions))
            {
                context.Transactions.Add(transaction);
                var adminService = new AdminDashboardService(context);
                await context.SaveChangesAsync();

                var result = await adminService.GetTransactionsCurrencyDaylyWins(currentDay);

                Assert.AreEqual(normalisedAmount, result.DaylyTotalUSD);

            }
        }

        [TestMethod]
        public async Task ReturnDaylyTotalInUSD()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ReturnDaylyTotalInUSD")
                .Options;

            var normalisedAmount = 10;
            var transactionType = new TransactionType()
            {
                Id = 1,
                Name = "Win"
            };

            var wallet = new Wallet()
            {
                Currency = new Currency()
                {
                    Name = "USD"
                }
            };
            var user = new User()
            {
                Wallet = wallet
            };

            var transaction = new Transaction()
            {
                CreatedOn = DateTime.Now,
                TransactionType = transactionType,
                User = user,
                NormalisedAmount = normalisedAmount
            };

            var currentDay = DateTime.Now.Day;
            using (var context = new CasinoContext(contextOptions))
            {
                context.Transactions.Add(transaction);
                var adminService = new AdminDashboardService(context);
                await context.SaveChangesAsync();

                var result = await adminService.GetTransactionsCurrencyDaylyWins(currentDay);

                Assert.AreEqual(normalisedAmount, result.DaylyTotalUSD);

            }
        }

        [TestMethod]
        public async Task ReturnDaylyTotalInGBP()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ReturnDaylyTotalInGBP")
                .Options;

            var normalisedAmount = 10;
            var transactionType = new TransactionType()
            {
                Id = 1,
                Name = "Win"
            };

            var wallet = new Wallet()
            {
                Currency = new Currency()
                {
                    Name = "GBP"
                }
            };
            var user = new User()
            {
                Wallet = wallet
            };

            var transaction = new Transaction()
            {
                CreatedOn = DateTime.Now,
                TransactionType = transactionType,
                User = user,
                NormalisedAmount = normalisedAmount
            };

            var currentDay = DateTime.Now.Day;
            using (var context = new CasinoContext(contextOptions))
            {
                context.Transactions.Add(transaction);
                var adminService = new AdminDashboardService(context);
                await context.SaveChangesAsync();

                var result = await adminService.GetTransactionsCurrencyDaylyWins(currentDay);

                Assert.AreEqual(normalisedAmount, result.DaylyTotalUSD);

            }
        }

        [TestMethod]
        public async Task ReturnDaylyTotalInEUR()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ReturnDaylyTotalInEUR")
                .Options;

            var normalisedAmount = 10;
            var transactionType = new TransactionType()
            {
                Id = 1,
                Name = "Win"
            };

            var wallet = new Wallet()
            {
                Currency = new Currency()
                {
                    Name = "EUR"
                }
            };
            var user = new User()
            {
                Wallet = wallet
            };

            var transaction = new Transaction()
            {
                CreatedOn = DateTime.Now,
                TransactionType = transactionType,
                User = user,
                NormalisedAmount = normalisedAmount
            };

            var currentDay = DateTime.Now.Day;
            using (var context = new CasinoContext(contextOptions))
            {
                context.Transactions.Add(transaction);
                var adminService = new AdminDashboardService(context);
                await context.SaveChangesAsync();

                var result = await adminService.GetTransactionsCurrencyDaylyWins(currentDay);

                Assert.AreEqual(normalisedAmount, result.DaylyTotalUSD);

            }
        }
    }
}
