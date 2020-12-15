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
    public class UnlockUser_Should
    {
        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        public async Task ThrowIfAliasInvalid(string id)
        {
            var context = new Mock<CasinoContext>();

            var userService = new UserService(context.Object);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>
                (async () => await userService.UnLockUser(id));
        }

        [TestMethod]
        public async Task ThrowToUnlockIfUserNotFound()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ThrowToUnlockIfUserNotFound")
                .Options;


            using (var context = new CasinoContext(contextOptions))
            {
                var userService = new UserService(context);
                await Assert.ThrowsExceptionAsync<EntityNotFoundException>
                    (async () => await userService.UnLockUser("invalid-user"));
            }
        }

        [TestMethod]
        public async Task ThrowIfRoleNotFoundToUnlockUser()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ThrowIfRoleNotFoundToUnlockUser")
                .Options;

            var user = new User() { Id = "valid-user" };
            using (var context = new CasinoContext(contextOptions))
            {
                context.Users.Add(user);
                context.SaveChanges();

                var userService = new UserService(context);
                await Assert.ThrowsExceptionAsync<EntityNotFoundException>
                    (async () => await userService.UnLockUser("valid-user"));
            }
        }

        [TestMethod]
        public async Task CorrectlyUnLockWithValidParameters()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "CorrectlyUnLockWithValidParameters")
                .Options;

            var user = new User() { Id = "test-user-id", Locked = true };
            var role = new IdentityUserRole<string>() { RoleId = "2", UserId = "test-user-id" };

            using (var context = new CasinoContext(contextOptions))
            {
                context.Users.Add(user);
                context.UserRoles.Add(role);
                context.SaveChanges();

                var userService = new UserService(context);
                var result = await userService.UnLockUser("test-user-id");

                Assert.IsFalse(result.Locked);
                Assert.IsNotNull(context.Users.FirstOrDefault(us => !us.Locked && us.Id == "test-user-id"));
            }
        }
    }
}
