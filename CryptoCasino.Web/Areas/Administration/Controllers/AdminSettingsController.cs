using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Service.Abstract;
using WebCasino.Web.Areas.Administration.Models;

namespace WebCasino.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class AdminSettingsController : Controller
    {
        private readonly IUserService userService;

        public AdminSettingsController(IUserService userService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

      
        public async Task<IActionResult> Index(string id)
        {
            var admin =await this.userService.RetrieveUser(id);

            var model = new UserSettingsViewModel(admin);
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserSettingsViewModel model, string returnUrl = null)
        {
            //ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var updateModel = await this.userService.EditUserAlias(model.Alias, model.Id);

                this.TempData["Updated"] = "Admin info is updated";
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}
