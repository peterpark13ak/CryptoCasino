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
	public class RemoveCard_Should
	{
		[TestMethod]
		public async Task ThrowArgumentNullException_WhenRemoveCardNumberIsNull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenRemoveCardNumberIsNull")
				.Options;

			string cardNumber = null;

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new CardService(context);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() => transactionService.RemoveCard(cardNumber)
				);
			}
		}

		[TestMethod]
		public async Task ThrowCardNumberException_WhenRemoveCardCardNumberIsNull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowCardNumberException_WhenGetCardCardNumberIsNull")
				.Options;

			string cardNumber = "a00000000b000000";

			DateTime expiration = new DateTime(2017, 11, 10);

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new CardService(context);

				await Assert.ThrowsExceptionAsync<CardNumberException>(
					() => transactionService.RemoveCard(cardNumber)
				);
			}
		}


		[TestMethod]
		public async Task ThrowArgumentNullException_WhenRemoveCardNumberNotFoundInDb()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenRemoveCardNumberNotFoundInDb")
				.Options;

			string cardNumber = "0000000000000000";

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new CardService(context);

				await Assert.ThrowsExceptionAsync<EntityNotFoundException>(
					() => transactionService.RemoveCard(cardNumber)
				);
			}
		}

		[TestMethod]
		public async Task SucesRemoveingCard()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
			.UseInMemoryDatabase(databaseName: "SucesGetAllCard")
			.Options;

			string cardNumber = "0000000000000000";
			var newBankCard = new BankCard()
			{
				CardNumber = cardNumber
			};

			DateTime expiration = new DateTime(2019, 11, 10);

			using (var context = new CasinoContext(contextOptions))
			{
				var cardService = new CardService(context);

				await context.BankCards.AddAsync(newBankCard);
				await context.SaveChangesAsync();

				var card = await cardService.RemoveCard(cardNumber);

				Assert.IsInstanceOfType(card, typeof(BankCard));
				Assert.IsTrue(card.CardNumber == cardNumber);
				Assert.IsTrue(card.IsDeleted == true);
			}
		}
	}
}