using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebCasino.DataContext;
using WebCasino.Service;

namespace WebCasino.ServiceTests.WalletServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ThrowIfContextNull()
        {
            Assert.ThrowsException<NullReferenceException>(() => new WalletService(null));
        }

        [TestMethod]
        public void CreateServiceIfContextNotNull()
        {
            var contextMock = new Mock<CasinoContext>();

            var walletService = new WalletService(contextMock.Object);

            Assert.IsNotNull(walletService);
            Assert.IsInstanceOfType(walletService, typeof(WalletService));
        }
    }
}
