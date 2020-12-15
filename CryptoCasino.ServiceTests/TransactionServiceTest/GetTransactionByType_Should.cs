using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;
using WebCasino.Service.Abstract;

namespace WebCasino.ServiceTests.TransactionServiceTest
{
	[TestClass]
	public class GetTransactionByType_Should
	{
		[TestMethod]
		public async Task ThrowArgumentNullException_WhenTransactionTypeNameIsNull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenTransactionTypeNameIsNull")
				.Options;

			var currencyServiceMock = new Mock<ICurrencyRateApiService>();

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context, currencyServiceMock.Object);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() => transactionService.GetTransactionByType(null)
				);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_WhenTransactionTypeNameLengthIsLessThenThree()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentOutOfRangeException_WhenTransactionTypeNameLengthIsLessThenThree")
				.Options;

			var currencyServiceMock = new Mock<ICurrencyRateApiService>();

			var transactionType = "12";

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context, currencyServiceMock.Object);

				await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(
					() => transactionService.GetTransactionByType(transactionType)
				);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_WhenTransactionTypeNameBiggerThenTen()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentOutOfRangeException_WhenTransactionTypeNameBiggerThenTen")
				.Options;

			var currencyServiceMock = new Mock<ICurrencyRateApiService>();

			var transactionType = new string('-', 21);

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context, currencyServiceMock.Object);

				await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(
					() => transactionService.GetTransactionByType(transactionType)
				);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentNullException_WhenTransactionTypeTypeCountAreEqualZero()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
			.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenTransactionTypeTypeCountAreEqualZero")
			.Options;

			var currencyServiceMock = new Mock<ICurrencyRateApiService>();

			var transactionType = "NoSuchTransaction";

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context, currencyServiceMock.Object);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() => transactionService.GetTransactionByType(transactionType)
				);
			}
		}

		[TestMethod]
		public async Task ReturnTransactionByType()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
			.UseInMemoryDatabase(databaseName: "ReturnTransactionByType")
			.Options;

			var currencyServiceMock = new Mock<ICurrencyRateApiService>();

			string userId = "id";
			double originalAmount = 1;
			var newBankCard = new BankCard()
			{
				Id = "id1",
			};

			string description = "1234567890";
			var transactionTypeTest = new TransactionType() { Id = 1, Name = "Win" };
			var newTransaction = new Transaction()
			{
				UserId = userId,
				OriginalAmount = originalAmount,
				Description = description,
				TransactionType = transactionTypeTest,
				Card = newBankCard
			};

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context, currencyServiceMock.Object);
				await context.Transactions.AddAsync(newTransaction);
				context.BankCards.Add(newBankCard);
				context.SaveChanges();

				var transActionTypeCount = await transactionService
					.GetTransactionByType("Win")
					.ToAsyncEnumerable()
					.Count();

				Assert.AreEqual(1, transActionTypeCount);
			}
		}
	}
}