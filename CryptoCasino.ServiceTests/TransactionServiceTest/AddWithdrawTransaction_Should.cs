using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;
using WebCasino.Service.Abstract;
using WebCasino.Service.Exceptions;

namespace WebCasino.ServiceTests.TransactionServiceTest
{
	[TestClass]
	public class AddWithdrawTransaction_Should
	{
		//Method Parameters Tests
		[TestMethod]
		public async Task ThrowArgumentNullException_WhenUserIdIsNull()
		{
			var casinoContextMock = new Mock<CasinoContext>();
			var currencyServiceMock = new Mock<ICurrencyRateApiService>();

			string userId = null;
            string cardId = "cardId";
			double amountInUserCurrency = 50;
			string description = "1234567890";

			var transactionService = new TransactionService(casinoContextMock.Object, currencyServiceMock.Object);

			await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => transactionService.AddWithdrawTransaction(userId, cardId, amountInUserCurrency, description));
		}

        [TestMethod]
        public async Task ThrowArgumentNullException_WhenCardIdIsNull()
        {
            var casinoContextMock = new Mock<CasinoContext>();
            var currencyServiceMock = new Mock<ICurrencyRateApiService>();

            string userId = "user-id";
            string cardId = null;
            double amountInUserCurrency = 50;
            string description = "1234567890";

            var transactionService = new TransactionService(casinoContextMock.Object, currencyServiceMock.Object);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => transactionService.AddWithdrawTransaction(userId, cardId, amountInUserCurrency, description));
        }

        [TestMethod]
		public async Task ThrowArgumentNullException_WhenDesctiptionIsNull()
		{
			var casinoContextMock = new Mock<CasinoContext>();
			var currencyServiceMock = new Mock<ICurrencyRateApiService>();

			string userId = "userId";
            string cardId = "cardId";
            double amountInUserCurrency = 50;
			string description = null;

			var transactionService = new TransactionService(casinoContextMock.Object, currencyServiceMock.Object);

			await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => transactionService.AddWithdrawTransaction(userId, cardId, amountInUserCurrency, description));
		}

		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_WhenDesctiptionIsShorterThenTen()
		{
			var casinoContextMock = new Mock<CasinoContext>();
			var currencyServiceMock = new Mock<ICurrencyRateApiService>();

			string userId = "userId";
            string cardId = "cardId";
            double amountInUserCurrency = 50;
			string description = "123456789";

			var transactionService = new TransactionService(casinoContextMock.Object, currencyServiceMock.Object);

			await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => transactionService.AddWithdrawTransaction(userId, cardId, amountInUserCurrency, description));
		}

		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_WhenDesctiptionIsLongerThen100()
		{
			var casinoContextMock = new Mock<CasinoContext>();
			var currencyServiceMock = new Mock<ICurrencyRateApiService>();

			string userId = "userId";
            string cardId = "cardId";
            double amountInUserCurrency = 50;
			string description = new string('-', 101);

			var transactionService = new TransactionService(casinoContextMock.Object, currencyServiceMock.Object);

			await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => transactionService.AddWithdrawTransaction(userId, cardId, amountInUserCurrency, description));
		}

		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_WhenAmountOfUserCurrencyIsSmallerThenZero()
		{
			var casinoContextMock = new Mock<CasinoContext>();
			var currencyServiceMock = new Mock<ICurrencyRateApiService>();

			string userId = "userId";
            string cardId = "cardId";
            double amountInUserCurrency = -1;
			string description = "1234567890";

			var transactionService = new TransactionService(casinoContextMock.Object, currencyServiceMock.Object);

			await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => transactionService.AddWithdrawTransaction(userId, cardId, amountInUserCurrency, description));
		}

		//End of Method Parameters tests

		[TestMethod]
		public async Task ThrowEntityNotFoundException_WhenUserWithGivenIdIsNotFoundInDatabase()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "WithdrawThrowEntityNotFoundException_WhenUserWithGivenIdIsNotFoundInDatabase")
				.Options;

			var currencyServiceMock = new Mock<ICurrencyRateApiService>();

			string userId = "userId";
            string cardId = "cardId";
            double amountInUserCurrency = 50.05;
			string description = "1234567890";

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context, currencyServiceMock.Object);

				await Assert.ThrowsExceptionAsync<EntityNotFoundException>(() => transactionService.AddWithdrawTransaction(userId, cardId, amountInUserCurrency, description));
			}
		}

        [TestMethod]
        public async Task ThrowEntityNotFoundException_WhenCardWithGivenIdIsNotFoundInDatabase()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ThrowEntityNotFoundException_WhenCardWithGivenIdIsNotFoundInDatabase")
                .Options;

            var currencyServiceMock = new Mock<ICurrencyRateApiService>();

            string userId = "userId";
            string cardId = "cardId";
            double amountInUserCurrency = 50.05;
            string description = "1234567890";

            using (var context = new CasinoContext(contextOptions))
            {
                var usre = new User()
                {
                    Id = userId
                };
                context.SaveChanges();
                var transactionService = new TransactionService(context, currencyServiceMock.Object);

                await Assert.ThrowsExceptionAsync<EntityNotFoundException>(() => transactionService.AddWithdrawTransaction(userId, cardId, amountInUserCurrency, description));
            }
        }

        [TestMethod]
		public async Task ThrowEntityCurrencyNotFoundException_WhenUserUseUnMaitainableCurrency()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "WithdrawThrowEntityCurrencyNotFoundException_WhenUserUseUnMaitainableCurrency")
				.Options;

			var currencyServiceMock = new Mock<ICurrencyRateApiService>();

			var serviceReturn = new ConcurrentDictionary<string, double>();
			serviceReturn.TryAdd("USD", 4);
			currencyServiceMock.Setup(s => s.GetRatesAsync()).Returns(Task.FromResult(serviceReturn));

			string userId = "userId";
            string cardId = "cardId";
            double amountInUserCurrency = 50.05;
			string description = "1234567890";

			var user = new User()
			{
				Id = userId,
				Wallet = new Wallet()
				{
					Id = "walledId",
					Currency = new Currency()
					{
						Id = 1,
						Name = "GBP"
					}
				},
                Cards = new List<BankCard>()
                {
                    new BankCard(){Id="cardId"}
                }
            };

			using (var context = new CasinoContext(contextOptions))
			{
				context.Users.Add(user);
				await context.SaveChangesAsync();

				var transactionService = new TransactionService(context, currencyServiceMock.Object);

				await Assert.ThrowsExceptionAsync<EntityCurrencyNotFoundException>(() 
                    => transactionService.AddWithdrawTransaction(userId, cardId, amountInUserCurrency, description));
			}
		}

		[TestMethod]
		public async Task Add_Withdraw_WithCorrectAmountOfCurrency()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "withdraw_Stake_WithCorrectAmountOfCurrency")
				.Options;

			var currencyServiceMock = new Mock<ICurrencyRateApiService>();

			var baseCurrency = "USD";
			var currency = 1.50;
			var serviceReturn = new ConcurrentDictionary<string, double>();
			serviceReturn.TryAdd(baseCurrency, currency);
			currencyServiceMock.Setup(s => s.GetRatesAsync()).Returns(Task.FromResult(serviceReturn));

			string userId = "userId";
            string cardId = "cardId";
            double amountInUserCurrency = 50;
			string description = "1234567890";

			var user = new User()
			{
				Id = userId,
				Wallet = new Wallet()
				{
					Id = "walledId",
					Currency = new Currency()
					{
						Id = 1,
						Name = baseCurrency
					},
                    DisplayBalance = 750,
                    NormalisedBalance = 500
				},
                Cards = new List<BankCard>()
                {
                    new BankCard(){Id="cardId"}
                }
            };

			using (var context = new CasinoContext(contextOptions))
			{
				context.Users.Add(user);
				await context.SaveChangesAsync();

				var transactionService = new TransactionService(context, currencyServiceMock.Object);

				var savedTransaction = await transactionService.AddWithdrawTransaction(userId, cardId, amountInUserCurrency, description);

				var originalAmount = savedTransaction.OriginalAmount;
				var normalisedAmount = savedTransaction.NormalisedAmount;
				var userAmount = context.Users.Find(userId).Wallet.NormalisedBalance;
                var userDisplayAmount = context.Users.Find(userId).Wallet.DisplayBalance;
                var winTransaction = savedTransaction.TransactionTypeId;

				Assert.AreEqual(amountInUserCurrency, originalAmount);
				Assert.AreEqual(amountInUserCurrency / currency, normalisedAmount);
				Assert.AreEqual(750 - amountInUserCurrency, userDisplayAmount);
                Assert.AreEqual(500 - amountInUserCurrency / currency, userAmount);
				Assert.IsTrue(winTransaction == 4);
			}
		}

        [TestMethod]
        public async Task ThrowWhenInsufficientFundsForOperation()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ThrowWhenInsufficientFundsForOperation")
                .Options;

            var currencyServiceMock = new Mock<ICurrencyRateApiService>();

            var baseCurrency = "USD";
            var currency = 1.50;
            string cardId = "cardId";
            var serviceReturn = new ConcurrentDictionary<string, double>();
            serviceReturn.TryAdd(baseCurrency, currency);
            currencyServiceMock.Setup(s => s.GetRatesAsync()).Returns(Task.FromResult(serviceReturn));

            string userId = "userId";
            double amountInUserCurrency = 50;
            string description = "1234567890";

            var user = new User()
            {
                Id = userId,
                Wallet = new Wallet()
                {
                    Id = "walledId",
                    Currency = new Currency()
                    {
                        Id = 1,
                        Name = baseCurrency
                    },

                },
                Cards = new List<BankCard>()
                {
                    new BankCard(){Id="cardId"}
                }
            };

            using (var context = new CasinoContext(contextOptions))
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();

                var transactionService = new TransactionService(context, currencyServiceMock.Object);

                await Assert.ThrowsExceptionAsync<InsufficientFundsException>(
                    () => transactionService.AddWithdrawTransaction(userId, cardId, amountInUserCurrency, description));
            }
        }
    }
}