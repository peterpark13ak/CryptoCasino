using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;
using WebCasino.Service.Exceptions;

namespace WebCasino.ServiceTests.WalletServiceTests
{
    [TestClass]
    public class CreateWallet_Should
    {
        [TestMethod]
        public async Task ThrowIfWalletAlreadyExists()
        {

            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ThrowIfWalletAlreadyExists")
                .Options;

            var user = new User() { Id = "test-user-id", IsDeleted = false };
            var wallet = new Wallet() { Id = "test-wallet", User = user };

            using (var context = new CasinoContext(contextOptions))
            {
                context.Wallets.Add(wallet);
                context.SaveChanges();

                var walletService = new WalletService(context);
                await Assert.ThrowsExceptionAsync<EntityAlreadyExistsException>
                    (async () => await walletService.CreateWallet("test-user-id", 1));
            }

        }

        [TestMethod]
        public async Task ThrowIfUserNotFound()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ThrowIfUserNotFound")
                .Options;

            using (var context = new CasinoContext(contextOptions))
            {

                var walletService = new WalletService(context);
                await Assert.ThrowsExceptionAsync<EntityNotFoundException>
                    (async () => await walletService.CreateWallet("test-user-id", 1));
            }
        }

        [TestMethod]
        public async Task ThrowIfCurrencyNotFound()
        {

            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ThrowIfCurrencyNotFound")
                .Options;

            var user = new User() { Id = "test-user-id", IsDeleted = false };

            using (var context = new CasinoContext(contextOptions))
            {
                context.Users.Add(user);
                context.SaveChanges();

                var walletService = new WalletService(context);

                await Assert.ThrowsExceptionAsync<EntityNotFoundException>
                    (async () => await walletService.CreateWallet("test-user-id", 1));
            }
        }

        [TestMethod]
        public async Task CreateWalletIfNoneFound()
        {

            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "CreateWalletIfNoneFound")
                .Options;

            var user = new User() { Id = "test-user-id", IsDeleted = false };

            var currency = new Currency() { Id = 1, Name = "Test-currency" };

            using (var context = new CasinoContext(contextOptions))
            {
                context.Users.Add(user);
                context.Currencies.Add(currency);
                context.SaveChanges();

                var walletService = new WalletService(context);

                var result = await walletService.CreateWallet("test-user-id", 1);

                Assert.IsNotNull(result);
                Assert.AreEqual("test-user-id", result.UserId);
                Assert.AreEqual(1, result.CurrencyId);
            }

        }
    }
}
