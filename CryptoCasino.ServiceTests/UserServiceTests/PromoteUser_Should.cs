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
    public class PromoteUser_Should
    {
        [TestMethod]
        public async Task ThrowIfUserNotFound()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ThrowIfUserNotFound")
                .Options;


            using(var context = new CasinoContext(contextOptions))
            {
                var userService = new UserService(context);
                await Assert.ThrowsExceptionAsync<EntityNotFoundException>
                    (async () => await userService.PromoteUser("invalid-user"));
            }
        }

        [TestMethod]
        public async Task ThrowIfRoleNotFound()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ThrowIfRoleNotFound")
                .Options;

            var user = new User() { Id = "valid-user" };
            using (var context = new CasinoContext(contextOptions))
            {
                context.Users.Add(user);
                context.SaveChanges();

                var userService = new UserService(context);
                await Assert.ThrowsExceptionAsync<EntityNotFoundException>
                    (async () => await userService.PromoteUser("valid-user"));
            }
        }

        [TestMethod]
        public async Task CorrectlyAddRoleWithValidId()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "CorrectlyAddRoleWithValidId")
                .Options;

            var user = new User() { Id = "valid-user" };
            var role = new IdentityUserRole<string>() { UserId = "valid-user", RoleId = "2" };
            using (var context = new CasinoContext(contextOptions))
            {
                context.Users.Add(user);
                context.UserRoles.Add(role);
                context.SaveChanges();

                var userService = new UserService(context);
                var result = await userService.PromoteUser("valid-user");

                Assert.IsNotNull(context.UserRoles.FirstOrDefault(ur => ur.UserId == result.Id && ur.RoleId == "1"));
            }
        }

        [TestMethod]
        public async Task CorrectlyRemoveOldRoleWithValidId()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "CorrectlyRemoveOldRoleWithValidId")
                .Options;

            var user = new User() { Id = "valid-user" };
            var role = new IdentityUserRole<string>() { UserId = "valid-user", RoleId = "2" };
            using (var context = new CasinoContext(contextOptions))
            {
                context.Users.Add(user);
                context.UserRoles.Add(role);
                context.SaveChanges();

                var userService = new UserService(context);
                var result = await userService.PromoteUser("valid-user");

                Assert.IsNull(context.UserRoles.FirstOrDefault(ur => ur.UserId == result.Id && ur.RoleId == "2"));
                Assert.AreEqual(1, context.UserRoles.Count());
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
                (async () => await userService.PromoteUser(id));
        }
    }
}
