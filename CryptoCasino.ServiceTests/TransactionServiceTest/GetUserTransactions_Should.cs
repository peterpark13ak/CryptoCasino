using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;
using WebCasino.Service.Abstract;

namespace WebCasino.ServiceTests.TransactionServiceTest
{
	[TestClass]
	public class GetUserTransactions_Should
	{
		[TestMethod]
		public async Task ThrowArgumentNullException_WhenUserIdIsNull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenUserIdIsNull")
				.Options;

			var currencyServiceMock = new Mock<ICurrencyRateApiService>();

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context, currencyServiceMock.Object);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() => transactionService.GetUserTransactions(null)
				);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentNullException_WhenNoSuchUserInDb()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
			.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenNoSuchUserInDb")
			.Options;

			var currencyServiceMock = new Mock<ICurrencyRateApiService>();

			var userId = "noId";

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context, currencyServiceMock.Object);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() => transactionService.GetUserTransactions(userId)
				);
			}
		}

        [TestMethod]
        public async Task ReturnUserWhenCorrectParametersArePassed_AndUserIdIsInDb()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
            .UseInMemoryDatabase(databaseName: "ReturnUserWhenCorrectParametersArePassed_AndUserIdIsInDb")
            .Options;

            var currencyServiceMock = new Mock<ICurrencyRateApiService>();

            var userId = "user-id";

            var transactionsData = new Transaction()
            {
                UserId = userId,
                TransactionType = new TransactionType()
            };


            using (var context = new CasinoContext(contextOptions))
            {
                var transactionService = new TransactionService(context, currencyServiceMock.Object);
                context.Transactions.Add(transactionsData);

                await context.SaveChangesAsync();

                var result = await transactionService.GetUserTransactions(userId);

                Assert.AreEqual(1, result.Count());
                Assert.IsInstanceOfType(result, typeof(IEnumerable<Transaction>));

            }
        }
    }
}