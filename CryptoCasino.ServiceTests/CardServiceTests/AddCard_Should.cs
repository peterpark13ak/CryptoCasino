using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Service;
using WebCasino.Service.Exceptions;

namespace WebCasino.ServiceTests.CardServiceTests
{
	[TestClass]
	public class AddCard_Should
	{
		[TestMethod]
		public async Task ThrowArgumentNullException_WhenCardNumberIsNull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenCardNumberIsNull")
				.Options;

			string cardNumber = null;
			string userId = "userId";
			DateTime expiration = new DateTime();

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new CardService(context);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() => transactionService.AddCard(cardNumber, userId, expiration)
				);
			}
		}

		[TestMethod]
		public async Task ThrowCardNumberException_WhenCardNumberIsInvalid()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowCardNumberException_WhenCardNumberIsInvalid")
				.Options;

			string cardNumber = "a00000000b000000";
			string userId = "userId";
			DateTime expiration = new DateTime(2017, 11, 10);

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new CardService(context);

				await Assert.ThrowsExceptionAsync<CardNumberException>(
					() => transactionService.AddCard(cardNumber, userId, expiration)
				);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentNullException_WhenUserIdIsNull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenUserIdIsNull")
				.Options;

			string cardNumber = new string('0',16);
			string userId = null;

			DateTime expiration = new DateTime();

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new CardService(context);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() =>  transactionService.AddCard(cardNumber, userId, expiration)
				);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentException_WhenDateIsInvalid()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentException_WhenDateIsInvalid")
				.Options;

			string cardNumber = new string('0', 16);
			string userId = "userId";

			DateTime expiration = new DateTime(2017,11,10);

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new CardService(context);

				await Assert.ThrowsExceptionAsync<CardExpirationException>(
					() => transactionService.AddCard(cardNumber, userId, expiration)
				);
			}
		}

		[TestMethod]
		public async Task AddCardSucessfull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
			.UseInMemoryDatabase(databaseName: "AddCardSucessfull")
			.Options;

			string cardNumber = new string('0', 16);
			string userId = "userId";

			DateTime expiration = new DateTime(2019, 11, 10);

			using (var context = new CasinoContext(contextOptions))
			{
				var cardService = new CardService(context);
				
				await cardService.AddCard(cardNumber, userId,expiration);

				var card = await context.BankCards
					.Where(c => c.CardNumber == cardNumber )
					.FirstAsync();
					

				Assert.AreEqual(cardNumber, card.CardNumber);
                Assert.IsNotNull(context.BankCards.FirstOrDefault(ca => ca.CardNumber == cardNumber));
			}
		}

	}
}
