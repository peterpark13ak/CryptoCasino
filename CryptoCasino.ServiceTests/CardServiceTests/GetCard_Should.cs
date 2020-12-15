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
	public class GetCard_Should
	{
		[TestMethod]
		public async Task ThrowArgumentNullException_WhenCardNumberIsNull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenUserIDIsNull")
				.Options;

			string cardNumber = null;

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new CardService(context);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() => transactionService.GetCard(cardNumber)
				);
			}
		}

		[TestMethod]
		public async Task ThrowCardNumberException_WhenGetCardCardNumberIsNull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowCardNumberException_WhenGetCardCardNumberIsNull")
				.Options;

			string cardId = "a00000000b000000";
			
			DateTime expiration = new DateTime(2017, 11, 10);

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new CardService(context);

				await Assert.ThrowsExceptionAsync<EntityNotFoundException>(
					() => transactionService.GetCard(cardId)
				);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentNullException_WhenCardNumberNotFoundInDb()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenCardNumberNotFoundInDb")
				.Options;

			string cardNumber = "0000000000000000";

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new CardService(context);

				await Assert.ThrowsExceptionAsync<EntityNotFoundException>(
					() => transactionService.GetCard(cardNumber)
				);
			}
		}

		[TestMethod]
		public async Task SucesGetACard()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
			.UseInMemoryDatabase(databaseName: "SucesGetAllCard")
			.Options;

			string cardId = "0000000000000000";
			var newBankCard = new BankCard()
			{
				Id = cardId
            };

			DateTime expiration = new DateTime(2019, 11, 10);

			using (var context = new CasinoContext(contextOptions))
			{
				var cardService = new CardService(context);

				await context.BankCards.AddAsync(newBankCard);
				await context.SaveChangesAsync();

				var card = await cardService.GetCard(cardId);

				Assert.IsInstanceOfType(card, typeof(BankCard));
				Assert.IsTrue(card.Id == cardId);
			}
		}
	}
}