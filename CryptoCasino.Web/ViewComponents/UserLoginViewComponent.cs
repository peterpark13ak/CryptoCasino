using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Entities;
using WebCasino.Service.Abstract;
using WebCasino.Web.Models.ViewComponentModels.UserLoginModels;
using WebCasino.Web.Utilities.Wrappers;

namespace WebCasino.Web.ViewComponents
{
    public class UserLoginViewComponent : ViewComponent
    {
        private readonly SignInManager<User> signInManager;
        private readonly IUserWrapper userManager;
        private readonly IUserService userService;

        public UserLoginViewComponent(SignInManager<User> signInManager, IUserWrapper userManager, IUserService userService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (signInManager.IsSignedIn(HttpContext.User))
            {
                var userId = this.userManager.GetUserId(HttpContext.User);
                var user = await userService.RetrieveUser(userId);

                return View("LoggedIn", new UserLogginViewModel(user));


            }
            else
            {
                return View();
            }
        }
    }
}
