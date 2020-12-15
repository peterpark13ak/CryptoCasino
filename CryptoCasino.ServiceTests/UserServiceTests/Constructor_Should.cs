using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebCasino.DataContext;
using WebCasino.Service;

namespace WebCasino.ServiceTests.UserServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ThrowIfContextNull()
        {
            Assert.ThrowsException<NullReferenceException>(() => new UserService(null));
        }

        [TestMethod]
        public void CreateServiceIfContextNotNull()
        {
            var contextMock = new Mock<CasinoContext>();

            var userService = new UserService(contextMock.Object);

            Assert.IsNotNull(userService);
            Assert.IsInstanceOfType(userService, typeof(UserService));
        }
    }
}
