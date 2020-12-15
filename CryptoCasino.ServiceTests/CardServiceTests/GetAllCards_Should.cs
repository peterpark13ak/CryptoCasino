using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;
using System.Linq;

namespace WebCasino.ServiceTests.CardServiceTests
{
	[TestClass]
	public class GetAllCards_Should
	{
		[TestMethod]
		public async Task ThrowArgumentNullException_WhenUserIDIsNull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenUserIDIsNull")
				.Options;
			
			string userId = null;
			
			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new CardService(context);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() => transactionService.GetAllCards(userId)
				);
			}
		}

		[TestMethod]
		public async Task SucesGetAllCards()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
			.UseInMemoryDatabase(databaseName: "SucesGetAllCards")
			.Options;

			string userId = "userId";
			var newBankCard = new BankCard()
			{
				UserId = "userId",
			};

			DateTime expiration = new DateTime(2019, 11, 10);

			using (var context = new CasinoContext(contextOptions))
			{
				var cardService = new CardService(context);

				await context.BankCards.AddAsync(newBankCard);
				await context.SaveChangesAsync();

				var card = await cardService.GetAllCards(userId);


				Assert.AreEqual(1, card.Count());
			}
		}
	}
}
