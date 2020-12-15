using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;
using WebCasino.Service.Abstract;
using WebCasino.Service.Exceptions;

namespace WebCasino.ServiceTests.UserServiceTests
{
    [TestClass]
    public class RetrieveUserTransaction_Should
    {
        [TestMethod]
        public async Task ThrowArgumentNullException_WhenUserIdParameterIsNull()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenUserIdParameterIsNull")
                .Options;

            var currencyServiceMock = new Mock<ICurrencyRateApiService>();

            using (var context = new CasinoContext(contextOptions))
            {
                var transactionService = new TransactionService(context, currencyServiceMock.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                    () => transactionService.RetrieveUserTransaction(null)
                );
            }
        }

        [TestMethod]
        public async Task ThrowArgumentNullException_WhenUserWithParameterIdIsNotFound()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
            .UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenUserWithParameterIdIsNotFound")
            .Options;

            var currencyServiceMock = new Mock<ICurrencyRateApiService>();

            var userId = "no-such-id";

            using (var context = new CasinoContext(contextOptions))
            {
                var transactionService = new TransactionService(context, currencyServiceMock.Object);

                await Assert.ThrowsExceptionAsync<EntityNotFoundException>(
                    () => transactionService.RetrieveUserTransaction(userId)
                );
            }
        }

        [TestMethod]
        public async Task ReturnUserTransactionsCorrectParametersArePassed_AndUserIdIsInDb()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
            .UseInMemoryDatabase(databaseName: "ReturnUserTransactionshenCorrectParametersArePassed_AndUserIdIsInDb")
            .Options;

            var currencyServiceMock = new Mock<ICurrencyRateApiService>();

            var userId = "user-id";
            var type = "Win";
            var currency = "USD";

            var transactionsData = new Transaction()
            {
                Id = userId,
                UserId = userId,
                User = new User()
                {
                    Id = userId,
                    Wallet = new Wallet()
                    {
                        Currency = new Currency()
                        {
                            Name = currency
                        }
                    },
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

                var result = await transactionService.RetrieveUserTransaction(userId);

                Assert.IsInstanceOfType(result, typeof(Transaction));
                Assert.IsTrue(result.UserId.Equals(userId));
                Assert.IsTrue(result.TransactionType.Name.Equals(type));
                Assert.IsTrue(result.User.Id.Equals(userId));
                Assert.IsTrue(result.User.Wallet.Currency.Name.Equals(currency));

            }
        }
    }
}
