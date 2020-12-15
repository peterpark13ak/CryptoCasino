using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;
using WebCasino.Service.Exceptions;

namespace WebCasino.ServiceTests.UserServiceTests
{
    [TestClass]
    public class RetrieveUser_Should
    {
        [TestMethod]
        public async Task ThrowIfUserNotFound()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ThrowIfUserNotFound")
                .Options;


            using (var context = new CasinoContext(contextOptions))
            {
                var userService = new UserService(context);
                await Assert.ThrowsExceptionAsync<EntityNotFoundException>
                    (async () => await userService.PromoteUser("invalid-user"));
            }
        }

        [TestMethod]
        public async Task ReturnCorrectUser()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ReturnCorrectUser")
                .Options;

            var user = new User() { Id = "valid-user", IsDeleted = false, Alias = "Test" };
            using (var context = new CasinoContext(contextOptions))
            {
                context.Users.Add(user);
                context.SaveChanges();
                var userService = new UserService(context);

                var result = await userService.RetrieveUser("valid-user");

                Assert.IsNotNull(result);
                Assert.AreEqual("Test", result.Alias);
            }
        }
    }
}
