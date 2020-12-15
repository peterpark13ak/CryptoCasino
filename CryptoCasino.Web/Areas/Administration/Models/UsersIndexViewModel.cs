using System.Collections.Generic;
using System.Linq;
using WebCasino.Entities;

namespace WebCasino.Web.Areas.Administration.Models
{
	public class UsersIndexViewModel
	{
		public  IEnumerable<UserViewModel> Users { get; set; }
     
	}
}