using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Service.Abstract;
using WebCasino.Web.Areas.Administration.Models;

namespace WebCasino.Web.Areas.Administration.Controllers
{
	[Area("Administration")]
	public class UserAdministrationController : Controller
	{
		private readonly IUserService userService;
        private readonly ITransactionService transactionService;

        public UserAdministrationController(IUserService service, ITransactionService transactionService)
		{
			this.userService = service ?? throw new System.ArgumentNullException(nameof(service));
            this.transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        }

		public async Task<IActionResult> Index()
		{
			
			var users = await this.userService.GetAllUsers();
            var userModels = users.Select(us => new UserViewModel(us));
            var model = new UsersIndexViewModel()
            {
                Users = userModels
            };
            return View(model);
		}

        [HttpGet]
        public async Task<IActionResult> Details(UserViewModel model)
        {
            var user = await this.userService.RetrieveUser(model.Id);
            var userTransactions = await this.transactionService.GetUserTransactions(user.Id);
            user.Transactions = userTransactions;
            model.Transactions = user.Transactions.Select(tr => new TransactionViewModel(tr));
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserAccountSettings(UserSettingsViewModel model, string returnUrl = null)
        {
            //ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var updateModel = await this.userService.EditUserAlias(model.Alias, model.Id);

                this.TempData["Updated"] = "User info is updated";
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LockUser(UserSettingsViewModel model)
        {

            await this.userService.LockUser(model.Id);

            this.TempData["Locked"] = "You Lock this user";

            return RedirectToAction("UserAccountSettings", "UserAdministration", model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TableLockUser(UsersIndexViewModel model, string id)
        {

            await this.userService.LockUser(id);

            this.TempData["Locked"] = "You Lock this user";

            return RedirectToAction("Index", "UserAdministration", model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnLockUser(UserSettingsViewModel model)
        {

            await this.userService.UnLockUser(model.Id);

            this.TempData["UnLocked"] = "You Unlock this user";

            return RedirectToAction("UserAccountSettings", "UserAdministration", model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TableUnLockUser(UserSettingsViewModel model, string id)
        {

            await this.userService.UnLockUser(id);

            this.TempData["UnLocked"] = "You Unlock this user";

            return RedirectToAction("Index", "UserAdministration", model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PromoteUser(UserSettingsViewModel model)
        {

            await this.userService.PromoteUser(model.Id);

            this.TempData["Promoted"] = "You promote to admin this user";

            return RedirectToAction("UserAccountSettings", "UserAdministration", model);
        }

        public async Task<IActionResult> UserAccountSettings(string id)
        {
            var userModel =await userService.RetrieveUser(id);
            var model = new UserSettingsViewModel(userModel);
            
            return  this.View(model);
        }

    }
}