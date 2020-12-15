using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;
using WebCasino.Service.Exceptions;

namespace WebCasino.ServiceTests.CardServiceTests
{
	[TestClass]
	public class Withdraw_Should
	{
		[TestMethod]
		public async Task ThrowArgumentNullException_WhenWithdrawCardNumberIsNull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenWithdrawCardNumberIsNull")
				.Options;

			string cardNumber = null;
			double amount = 100.2;

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new CardService(context);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() => transactionService.Withdraw(cardNumber, amount)
				);
			}
		}

		[TestMethod]
		public async Task ThrowCardNumberException_WhenWidthdrawdNumberIsNull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowCardNumberException_WhenWidthdrawdNumberIsNull")
				.Options;

			string cardNumber = "a00000000b000000";
			double amount = 100.2;

			DateTime expiration = new DateTime(2017, 11, 10);

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new CardService(context);

				await Assert.ThrowsExceptionAsync<EntityNotFoundException>(
					() => transactionService.Withdraw(cardNumber, amount)
				);
			}
		}


		[TestMethod]
		public async Task ThrowArgumentNullException_WhenWithdrawAmountIsLessThenZero()
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
		public async Task ThrowArgumentNullException_WhenWithdrawCardNumberNotFoundInDb()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenWithdrawCardNumberNotFoundInDb")
				.Options;

			string cardNumber = "0000000000000000";
			double amount = 100.2;

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new CardService(context);

				await Assert.ThrowsExceptionAsync<EntityNotFoundException>(
					() => transactionService.Withdraw(cardNumber, amount)
				);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentNullException_WhenWithdrawCardDateIsExpired()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
			.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenWithdrawCardDateIsExpired")
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
					() => cardService.Withdraw(cardNumber, amount)
					);
			}
		}

		[TestMethod]
		public async Task WidthdrawSucessuly()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
			.UseInMemoryDatabase(databaseName: "WidthdrawSucessuly")
			.Options;

			string cardId = "0000000000000000";
			double amount = 100.2;

			var newBankCard = new BankCard()
			{
				Id = cardId,
				Expiration = new DateTime(2019, 2, 2)
			};

			using (var context = new CasinoContext(contextOptions))
			{
				var cardService = new CardService(context);

				await context.BankCards.AddAsync(newBankCard);
				await context.SaveChangesAsync();

				await cardService.Withdraw(cardId, amount);

				Assert.IsTrue(newBankCard.MoneyRetrieved == amount);
			}
		}
	}
}