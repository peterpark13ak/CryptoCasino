using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Service;
using WebCasino.Service.Abstract;
using WebCasino.Service.DTO.Filtering;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestManager;
using WebCasino.Service.Utility.TableFilterUtilities;

namespace WebCasino.ServiceTests.TransactionServiceTest
{
    [TestClass]
    public class GetFiltered_Should
    {
        [TestMethod]
        public async Task ThrowIfModelIsNull()
        {
            var contextMock = new Mock<CasinoContext>();

            var apiProviderMock = new Mock<ICurrencyRateApiService>();

            var transactionService = new TransactionService(contextMock.Object, apiProviderMock.Object);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => transactionService.GetFiltered(null));
        }

        [TestMethod]
        public async Task ReturnCorrectModelIfParametersValid()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ReturnCorrectModelIfParametersValid")
                .Options;

            var apiProviderMock = new Mock<ICurrencyRateApiService>();



            var model = new DataTableModel()
            {
                length = 10,
                start = 0
                
            };

            using (var context = new CasinoContext(contextOptions))
            {
                var transactionService = new TransactionService(context, apiProviderMock.Object);
                var result = await transactionService.GetFiltered(model);

                Assert.IsInstanceOfType(result, typeof(TableFilteringDTO));
            }




        }
    }
}
