using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;

namespace WebCasino.ServiceTests.AdminDashboardServiceTests
{
    [TestClass]
    public class GetTotalTransactionsByTypeCount_Should
    {
        [TestMethod]
        public async Task ThrowExceptionArgumentNullException_WhenParameterTypeIsNull()
        {
            var casinoContextMock = new Mock<CasinoContext>();

            string transactionType = null;

            var adminDashboardService = new AdminDashboardService(casinoContextMock.Object);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => adminDashboardService.GetTotaTransactionsByTypeCount(transactionType));

        }

        [TestMethod]
        public async Task ReturnZeroCount()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
               .UseInMemoryDatabase(databaseName: "ReturnZeroCount")
               .Options;

            using (var context = new CasinoContext(contextOptions))
            {
                var adminService = new AdminDashboardService(context);

                var transactionType = "Win";

                var result = await adminService.GetTotaTransactionsByTypeCount(transactionType);

                Assert.AreEqual(0, result);

            }
        }

        [TestMethod]
        public async Task ReturnResultOne()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
              .UseInMemoryDatabase(databaseName: "ReturnZeroCount")
              .Options;

            var type = "Win";

            var transactionType = new TransactionType()
            {
                Id = 1,
                Name = type
            };

            var transaction = new Transaction()
            {

                TransactionType = transactionType

            };

            using (var context = new CasinoContext(contextOptions))
            {
                context.Transactions.Add(transaction);
                await context.SaveChangesAsync();

                var adminService = new AdminDashboardService(context);

                var result = await adminService.GetTotaTransactionsByTypeCount(type);

                Assert.AreEqual(1, result);

            }
        }
    }
}
