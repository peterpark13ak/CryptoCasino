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
    public class RetrieveWallet_Should
    {
        [TestMethod]
        public async Task ThrowIfWalletNotFound()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ThrowIfWalletNotFound")
                .Options;

            using (var context = new CasinoContext(contextOptions))
            {

                var walletService = new WalletService(context);
                await Assert.ThrowsExceptionAsync<EntityNotFoundException>
                    (async () => await walletService.RetrieveWallet("invalid-user"));
            }
        }

        [TestMethod]
        public async Task RetrieveWalletIfExists()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                 .UseInMemoryDatabase(databaseName: "RetrieveWalletIfExists")
                 .Options;

            var user = new User() { Id = "valid-user"};
            var wallet = new Wallet() { Id = "wallet-test", User = user, CurrencyId = 1};

            using (var context = new CasinoContext(contextOptions))
            {
                context.Wallets.Add(wallet);
                context.SaveChanges();

                var walletService = new WalletService(context);
                var result = await walletService.RetrieveWallet("valid-user");

                Assert.IsNotNull(result);
                Assert.AreEqual("wallet-test", result.Id);
                Assert.AreEqual(1, result.CurrencyId);
            }
        }
    }
}
