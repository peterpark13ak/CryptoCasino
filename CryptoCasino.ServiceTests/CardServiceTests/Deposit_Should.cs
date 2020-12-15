using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;
using WebCasino.Service.Exceptions;

namespace WebCasino.ServiceTests.CardServiceTests
{
    [TestClass]
    public class Deposit_Should
    {
        [TestMethod]
        public async Task ThrowArgumentNullException_WhenWDepositCardNumberIsNull()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenWDepositCardNumberIsNull")
                .Options;

            string cardNumber = null;
            double amount = 100.2;

            using (var context = new CasinoContext(contextOptions))
            {
                var transactionService = new CardService(context);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                    () => transactionService.Deposit(cardNumber, amount)
                );
            }
        }


        [TestMethod]
        public async Task ThrowArgumentNullException_WhenDepositAmountIsLessThenZero()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenWithdrawAmountIsLessThenZero")
                .Options;

            string cardNumber = "0000000000000000";
            double amount = -0.1;

            using (var context = new CasinoContext(contextOptions))
            {
                var transactionService = new CardService(context);

                await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(
                    () => transactionService.Withdraw(cardNumber, amount)
                );
            }
        }

        [TestMethod]
        public async Task ThrowArgumentNullException_WhenDepositCardNumberNotFoundInDb()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenDepositCardNumberNotFoundInDb")
                .Options;

            string cardNumber = "0000000000000000";
            double amount = 100.2;

            using (var context = new CasinoContext(contextOptions))
            {
                var transactionService = new CardService(context);

                await Assert.ThrowsExceptionAsync<EntityNotFoundException>(
                    () => transactionService.Deposit(cardNumber, amount)
                );
            }
        }

        [TestMethod]
        public async Task ThrowArgumentNullException_WhenDepositCardDateIsExpired()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
            .UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenDepositCardDateIsExpired")
            .Options;

            string cardNumber = "0000000000000000";
            double amount = 100.2;

            var newBankCard = new BankCard()
            {
                Id = cardNumber,
                Expiration = new DateTime(2017, 2, 2)
            };

            using (var context = new CasinoContext(contextOptions))
            {
                var cardService = new CardService(context);

                await context.BankCards.AddAsync(newBankCard);
                await context.SaveChangesAsync();

                await Assert.ThrowsExceptionAsync<CardExpirationException>(
                    () => cardService.Deposit(cardNumber, amount)
                    );
            }
        }

        [TestMethod]
        public async Task DepositSuccesfully()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
            .UseInMemoryDatabase(databaseName: "DepositSuccesfully")
            .Options;

            string cardNumber = "0000000000000000";
            double amount = 100.2;

            var newBankCard = new BankCard()
            {
                Id = cardNumber,
                Expiration = new DateTime(2019, 2, 2)
            };

            using (var context = new CasinoContext(contextOptions))
            {
                var cardService = new CardService(context);

                await context.BankCards.AddAsync(newBankCard);
                await context.SaveChangesAsync();

                await cardService.Deposit(cardNumber, amount);

                Assert.IsTrue(newBankCard.MoneyAdded == amount);
            }
        }
    }
}
