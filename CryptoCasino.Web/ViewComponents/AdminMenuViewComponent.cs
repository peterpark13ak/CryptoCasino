using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebCasino.Entities;
using WebCasino.Service.Abstract;
using WebCasino.Web.Models.ViewComponentModels.UserLoginModels;
using WebCasino.Web.Utilities.Wrappers;

namespace WebCasino.Web.ViewComponents
{
	public class AdminMenuViewComponent : ViewComponent
	{
		private readonly SignInManager<User> signInManager;
		private readonly IUserWrapper userManager;
		private readonly IUserService userService;

		public AdminMenuViewComponent(SignInManager<User> signInManager, IUserWrapper userManager, IUserService userService)
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

				return View("AdminMenu", new UserLogginViewModel(user));
			}
			else
			{
				return View();
			}
		}
	}
}