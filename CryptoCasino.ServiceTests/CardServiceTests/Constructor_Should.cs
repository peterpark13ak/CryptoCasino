using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCasino.DataContext;
using WebCasino.Service;
using WebCasino.Service.Exceptions;

namespace WebCasino.ServiceTests.CardServiceTests
{
	[TestClass]
	public class Constructor_Should
	{
		[TestMethod]
		public void ThrowEntityNotFoundException_WhenNullParameterIsPassed()
		{
			Assert.ThrowsException<EntityNotFoundException>(() => new CardService(null));
		}

		[TestMethod]
		public void CreateInstance_WhenCorrectParametersArePassed()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "CreateInstance_WhenCorrectParametersArePassed")
				.Options;

			using (var context = new CasinoContext(contextOptions))
			{
				var service = new CardService(context);

				Assert.IsInstanceOfType(service, typeof(CardService));
			}

			
		}
	}
}
