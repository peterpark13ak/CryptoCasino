using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Service;

namespace WebCasino.ServiceTests.AdminDashboardServiceTests
{

    [TestClass]
    public class GetMonthsTransactions_Should
    {
        [TestMethod]
        public async Task ThrowExceptionArgumentNullException_WhenParameterTypeIsNull()
        {
            var casinoContextMock = new Mock<CasinoContext>();


            string transactionType = null;
            DateTime date = DateTime.Parse("12/12/2018");
            int monthCount = 1;

            var adminDashboardService = new AdminDashboardService(casinoContextMock.Object);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => adminDashboardService.GetMonthsTransactions(date, transactionType, monthCount));

        }

        [TestMethod]
        public async Task ThrowExceptionArgumentOutOfRangeException_WhenParameterMonthCountIsLessThenOne()
        {
            var casinoContextMock = new Mock<CasinoContext>();

            string transactionType = "Win";
            DateTime date = DateTime.Parse("12/12/2018");
            int monthCount = 0;

            var adminDashboardService = new AdminDashboardService(casinoContextMock.Object);

            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => adminDashboardService.GetMonthsTransactions(date, transactionType, monthCount));

        }

        [TestMethod]
        public async Task ThrowExceptionArgumentOutOfRangeException_WhenParameterMonthCountIsMoreThen12()
        {
            var casinoContextMock = new Mock<CasinoContext>();


            string transactionType = "Win";
            DateTime date = DateTime.Parse("12/12/2018");
            int monthCount = 13;

            var adminDashboardService = new AdminDashboardService(casinoContextMock.Object);

            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => adminDashboardService.GetMonthsTransactions(date, transactionType, monthCount));

        }

        [TestMethod]

        public async Task ReturnThreeMontsValueByMonthDTO()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ReturnZeroValueByMonthDTO")
                .Options;

            using (var context = new CasinoContext(contextOptions))
            {
                var adminService = new AdminDashboardService(context);

                var transactionType = "Win";
                DateTime date = DateTime.Parse("12/12/2018");
                var month = 2;

                var result = await adminService.GetMonthsTransactions(date, transactionType, month);

                Assert.AreEqual(3, result.ValuesByMonth.Count);

            }
        }

    }
}

