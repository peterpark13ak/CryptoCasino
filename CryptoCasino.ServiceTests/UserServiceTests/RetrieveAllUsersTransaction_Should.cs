using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;
using WebCasino.Service.Abstract;

namespace WebCasino.ServiceTests.UserServiceTests
{
    [TestClass]
    public class RetrieveAllUsersTransaction_Should
    {
        [TestMethod]
        public async Task ThrowArgumentNullException_WhenIdParameterIsNull()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenIdParameterIsNull")
                .Options;

            var currencyServiceMock = new Mock<ICurrencyRateApiService>();

            using (var context = new CasinoContext(contextOptions))
            {
                var transactionService = new TransactionService(context, currencyServiceMock.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                    () => transactionService.RetrieveAllUsersTransaction(null)
                );
            }
        }


        [TestMethod]
        public async Task ReturnAllUserTransactionsCorrectParametersArePassed_AndUserIdIsInDb()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
            .UseInMemoryDatabase(databaseName: "ReturnAllUserTransactionsCorrectParametersArePassed_AndUserIdIsInDb")
            .Options;

            var currencyServiceMock = new Mock<ICurrencyRateApiService>();

            var userId = "user-id";
            var type = "Win";


            var transactionsData = new Transaction()
            {
                Id = userId,
                UserId = userId,
                User = new User()
                {
                    Id = userId,

                },
                TransactionType = new TransactionType()
                {
                    Name = type
                }
            };

            using (var context = new CasinoContext(contextOptions))
            {
                var transactionService = new TransactionService(context, currencyServiceMock.Object);

                context.Transactions.Add(transactionsData);

                await context.SaveChangesAsync();

                var result = await transactionService.RetrieveAllUsersTransaction(userId);

                Assert.IsInstanceOfType(result, typeof(IEnumerable<Transaction>));

            }
        }
    }
}
