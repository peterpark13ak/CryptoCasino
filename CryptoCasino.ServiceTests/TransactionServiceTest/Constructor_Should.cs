using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebCasino.DataContext;
using WebCasino.Service;
using WebCasino.Service.Abstract;
using WebCasino.Service.Exceptions;

namespace WebCasino.ServiceTests.TransactionServiceTest
{
	[TestClass]
	public class Constructor_Should
	{
		[TestMethod]
		public void ThrowEntityNotFoundException_WhenNullParameterIsPassed()
		{
			var currencyServiceMock = new Mock<ICurrencyRateApiService>();

			Assert.ThrowsException<EntityNotFoundException>(() => new TransactionService(null, currencyServiceMock.Object));
		}

		[TestMethod]
		public void CreateInstance_WhenCorrectParametersArePassed()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "CreateInstance_WhenCorrectParametersArePassed")
				.Options;

			var currencyServiceMock = new Mock<ICurrencyRateApiService>();

			using (var context = new CasinoContext(contextOptions))
			{
				var service = new TransactionService(context, currencyServiceMock.Object);

				Assert.IsInstanceOfType(service, typeof(TransactionService));
			}
		}
	}
}