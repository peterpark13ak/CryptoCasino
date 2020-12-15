using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestManager;

namespace WebCasino.ServiceTests.CurrencyRateApiServiceTests
{
    [TestClass]
    public class ApiRequester_Should
    {
        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        public async Task ThrowIfConnectionParamEmpty(string connection)
        {
            var httpWrapper = new Mock<IHttpWrapper>();

            var apiRequester = new APIRequester(httpWrapper.Object);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => apiRequester.Request(connection));
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        public async Task ThrowIfReponseIsEmpty(string response)
        {
            var httpWrapper = new Mock<IHttpWrapper>();
            httpWrapper.Setup(hw => hw.GetStringAsync("connection"))
                .ReturnsAsync(response);

            var apiRequester = new APIRequester(httpWrapper.Object);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => apiRequester.Request("connection"));
        }

        [TestMethod]
        public async Task ReturnReponseIfEverythingIsValid()
        {
            var httpWrapper = new Mock<IHttpWrapper>();
            httpWrapper.Setup(hw => hw.GetStringAsync("connection"))
                .ReturnsAsync("response");

            var apiRequester = new APIRequester(httpWrapper.Object);

            var result = await apiRequester.Request("connection");

            Assert.AreEqual("response", result);
        }
    }
}
