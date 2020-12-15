using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebCasino.Service;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestManager;
using WebCasino.Service.Utility.Wrappers;

namespace WebCasino.ServiceTests.CurrencyRateApiServiceTests
{
    [TestClass]
    public class GetRatesAsync_Should
    {
        [TestMethod]
        public async Task ReturnSavedRatesIfCacheNotExpired()
        {
            var apiRequester = new Mock<IAPIRequester>();

            var dateWrapper = new Mock<IDateWrapper>();

            var date = DateTime.UtcNow;

            dateWrapper.Setup(dw => dw.Now()).Returns(date);

            var dictionary = new ConcurrentDictionary<string, double>();

            dictionary["USD"] = 1;

            var sut = new CurrencyRateApiService(apiRequester.Object, dateWrapper.Object);

            Type sutType = typeof(CurrencyRateApiService);

            FieldInfo ratesField = sutType.GetField("rates", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            ratesField.SetValue(sut, dictionary);

            FieldInfo dateTimeField = sutType.GetField("lastUpdate", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            dateTimeField.SetValue(sut, date.AddHours(1));

            var result = await sut.GetRatesAsync();

            Assert.IsNotNull(result["USD"]);

            Assert.AreEqual(1, result["USD"]);
        } 

        [TestMethod]
        public async Task ReturnNewDictionaryIfCacheIsExpired()
        {
            var apiRequester = new Mock<IAPIRequester>();

            apiRequester.Setup(ar => ar.Request("https://api.exchangeratesapi.io/latest?base=USD&symbols=EUR,BGN,GBP,USD"))
                .ReturnsAsync("{\"date\": \"2018-12-07\",\"rates\": {\"BGN\": 1.7199894468,\"USD\": 1,},\"base\": \"USD\"}");

            var dateWrapper = new Mock<IDateWrapper>();

            var date = DateTime.UtcNow;

            dateWrapper.Setup(dw => dw.Now()).Returns(date);

            var dictionary = new ConcurrentDictionary<string, double>();

            dictionary["USD"] = 2;

            var sut = new CurrencyRateApiService(apiRequester.Object, dateWrapper.Object);

            Type sutType = typeof(CurrencyRateApiService);

            FieldInfo ratesField = sutType.GetField("rates", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            ratesField.SetValue(sut, dictionary);

            FieldInfo dateTimeField = sutType.GetField("lastUpdate", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            dateTimeField.SetValue(sut, date.Subtract(TimeSpan.FromHours(25)));

            var result = await sut.GetRatesAsync();

            Assert.IsNotNull(result["USD"]);
            Assert.IsNotNull(result["BGN"]);

            Assert.AreEqual(1.7199894468, result["BGN"]);
            Assert.AreEqual(1, result["USD"]);
        }

        [TestMethod]
        public async Task ReturnNewDictionaryIfDictionaryFieldIsNull()
        {
            var apiRequester = new Mock<IAPIRequester>();

            apiRequester.Setup(ar => ar.Request("https://api.exchangeratesapi.io/latest?base=USD&symbols=EUR,BGN,GBP,USD"))
                .ReturnsAsync("{\"date\": \"2018-12-07\",\"rates\": {\"BGN\": 1.7199894468,\"USD\": 1,},\"base\": \"USD\"}");

            var dateWrapper = new Mock<IDateWrapper>();

            var date = DateTime.UtcNow;

            dateWrapper.Setup(dw => dw.Now()).Returns(date);

            var sut = new CurrencyRateApiService(apiRequester.Object, dateWrapper.Object);

            Type sutType = typeof(CurrencyRateApiService);

            FieldInfo dateTimeField = sutType.GetField("lastUpdate", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            dateTimeField.SetValue(sut, date.Subtract(TimeSpan.FromHours(25)));

            var result = await sut.GetRatesAsync();

            Assert.IsNotNull(result["USD"]);
            Assert.IsNotNull(result["BGN"]);

            Assert.AreEqual(1.7199894468, result["BGN"]);
            Assert.AreEqual(1, result["USD"]);
        }
    }
}
