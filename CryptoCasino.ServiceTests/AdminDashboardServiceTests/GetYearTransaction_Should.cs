using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Service;

namespace WebCasino.ServiceTests.AdminDashboardServiceTests
{
    [TestClass]
    public class GetYearTransaction_Should
    {
        [TestMethod]
        public async Task ReturnOneYearMontsValueByMonthDTO()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ReturnOneYearMontsValueByMonthDTO")
                .Options;

            using (var context = new CasinoContext(contextOptions))
            {
                var adminService = new AdminDashboardService(context);

                var date = DateTime.Parse("12/14/2018");
                var result = await adminService.GetYearTransactions(date);

                Assert.AreEqual(12, result.ValuesByMonth.Count);

            }
        }

    }
}
