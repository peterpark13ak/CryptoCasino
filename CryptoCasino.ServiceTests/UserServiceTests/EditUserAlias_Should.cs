using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;
using WebCasino.Service.Exceptions;

namespace WebCasino.ServiceTests.UserServiceTests
{
    [TestClass]
    public class EditUserAlias_Should
    {
        [TestMethod]
        public async Task ThrowIfUserNotFound()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "EditUserAliasShouldThrowIfUserNotFound")
                .Options;

            using (var context = new CasinoContext(contextOptions))
            {
                var userService = new UserService(context);
                await Assert.ThrowsExceptionAsync<EntityNotFoundException>
                    (async () => await userService.EditUserAlias("test-user-alias", "test-user-id"));
            }
        }

        [TestMethod]
        public async Task ThrowIfFoundUserIsDeleted()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "EditUserAliasShouldThrowIfFoundUserIsDeleted")
                .Options;

            var user = new User() { Id = "test-user-id", IsDeleted = true };

            using (var context = new CasinoContext(contextOptions))
            {
                context.Users.Add(user);
                context.SaveChanges();

                var userService = new UserService(context);
                await Assert.ThrowsExceptionAsync<EntityNotFoundException>
                    (async () => await userService.EditUserAlias("test-user-alias", "test-user-id"));
            }
        }
        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        public async Task ThrowIfNewAliasInvalid(string alias)
        {
            var context = new Mock<CasinoContext>();

            var userService = new UserService(context.Object);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>
                (async () => await userService.EditUserAlias(alias, "test-user-id"));
        }

        [TestMethod]
        public async Task CorrectlyEditAlias()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "EditUserShouldCorrectlyEditAlias")
                .Options;

            var user = new User() { Id = "test-user-id", IsDeleted = false, Alias = "before" };

            using (var context = new CasinoContext(contextOptions))
            {
                context.Users.Add(user);
                context.SaveChanges();

                var userService = new UserService(context);
                var result = await userService.EditUserAlias("after", "test-user-id");

                Assert.AreEqual("after", result.Alias);
                Assert.IsTrue(context.Users.Where(u => u.Alias == "after").Count() == 1);
            }
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        public async Task ThrowIfIdIsNull(string id)
        {
            var context = new Mock<CasinoContext>();

            var userService = new UserService(context.Object);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>
                (async () => await userService.EditUserAlias("test-alias", id));
        }
    }
}
