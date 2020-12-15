using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;

namespace WebCasino.ServiceTests.UserServiceTests
{
    [TestClass]
    public class GetAllUsers_Should
    {
        [TestMethod]
        public async Task ReturnListOfAllUsersThatAreNotDeleted()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ReturnListOfAllUsersThatAreNotDeleted")
                .Options;

            var validUserOne = new User() { Id = "validUserOne", IsDeleted = false };
            var validUserTwo = new User() { Id = "validUserTwo", IsDeleted = false };
            var invalidUser = new User() { Id = "invalidUser", IsDeleted = true };

            using(var context = new CasinoContext(contextOptions))
            {
                context.Users.Add(validUserOne);
                context.Users.Add(validUserTwo);
                context.Users.Add(invalidUser);

                context.SaveChanges();

                var userService = new UserService(context);

                var result = await userService.GetAllUsers();

                Assert.IsTrue(result.Count() == 2);
                Assert.IsTrue(result.FirstOrDefault(us => us.Id == validUserOne.Id) != null);
                Assert.IsTrue(result.FirstOrDefault(us => us.Id == validUserTwo.Id) != null);
                Assert.IsFalse(result.FirstOrDefault(us => us.Id == invalidUser.Id) != null);
            }

        }
    }
}
