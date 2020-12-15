using Microsoft.AspNetCore.Identity;
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
    public class LockUser_Should
    {
        [TestMethod]
        public async Task ThrowIfUserNotFound()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ThrowIfUserNotFound")
                .Options;

            var user = new User() { Id = "invalid-user", Locked = false };

            using (var context = new CasinoContext(contextOptions))
            {
                context.Users.Add(user);
                context.SaveChanges();

                var userService = new UserService(context);

                await Assert.ThrowsExceptionAsync<EntityNotFoundException>
                    (async () => await userService.LockUser("test-user-id"));
            }
        }
        [TestMethod]
        public async Task ThrowIfUserIsAdmin()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
    .UseInMemoryDatabase(databaseName: "ThrowIfUserIsAdmin")
    .Options;

            var user = new User() { Id = "invalid-user", Locked = false };
            var role = new IdentityUserRole<string>() { RoleId = "1", UserId = "invalid-user" };

            using (var context = new CasinoContext(contextOptions))
            {
                context.Users.Add(user);
                context.UserRoles.Add(role);
                context.SaveChanges();

                var userService = new UserService(context);

                await Assert.ThrowsExceptionAsync<InvalidAdministratorOperationException>
                    (async () => await userService.LockUser("invalid-user"));
            }
        }

        [TestMethod]
        public async Task CorrectlyLockWithValidParameters()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "CorrectlyLockWithValidParameters")
                .Options;

            var user = new User() { Id = "test-user-id", Locked = false };
            var role = new IdentityUserRole<string>() { RoleId = "2", UserId = "test-user-id" };

            using (var context = new CasinoContext(contextOptions))
            {
                context.Users.Add(user);
                context.UserRoles.Add(role);
                context.SaveChanges();

                var userService = new UserService(context);
                var result = await userService.LockUser("test-user-id");

                Assert.IsTrue(result.Locked);
                Assert.IsNotNull(context.Users.FirstOrDefault(us => us.Locked && us.Id == "test-user-id"));
            }
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        public async Task ThrowIfAliasInvalid(string id)
        {
            var context = new Mock<CasinoContext>();

            var userService = new UserService(context.Object);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>
                (async () => await userService.LockUser(id));
        }
    }
}
