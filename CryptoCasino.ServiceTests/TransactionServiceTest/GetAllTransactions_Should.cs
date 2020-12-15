using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;
using WebCasino.Service.Abstract;

namespace WebCasino.ServiceTests.TransactionServiceTest
{
	[TestClass]
	public class GetAllTransactions_Should
	{
		[TestMethod]
		public async Task ReturnAllTransactions()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ReturnAllTransactions")
				.Options;

			var currencyServiceMock = new Mock<ICurrencyRateApiService>();

			var serviceReturn = new ConcurrentDictionary<string, double>();

			string userId = "userId";
			double amountInUserCurrency = 50;
			string description = "1234567890";


            var transactionType = new TransactionType()
            {
                Id = 1
            };

            var newTransaction = new Transaction()
			{
				UserId = userId,
				OriginalAmount = amountInUserCurrency,
				Description = description,
				TransactionTypeId = 1,
                TransactionType = transactionType
            };


			using (var context = new CasinoContext(contextOptions))
			{
				context.Transactions.Add(newTransaction);


				await context.SaveChangesAsync();

				var transactionService = new TransactionService(context, currencyServiceMock.Object);

				var transactions = await transactionService.GetAllTransactionsTable().ToListAsync();

				Assert.AreEqual(1, transactions.Count());
				Assert.IsTrue(transactions.First(u => u.UserId == userId).UserId == userId);
				Assert.IsTrue(transactions.First(u => u.UserId == userId).OriginalAmount == amountInUserCurrency);
				Assert.IsTrue(transactions.First(u => u.UserId == userId).Description == description);
			}
		}
	}
}