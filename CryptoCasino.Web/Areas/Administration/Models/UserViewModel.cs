using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebCasino.Entities;

namespace WebCasino.Web.Areas.Administration.Models
{
	public class UserViewModel
	{
        public UserViewModel()
        {

        }
        public UserViewModel(User user)
        {
            this.Id = user.Id;
            this.CreatedOn = user.CreatedOn;
            this.Email = user.Email;
            this.Alias = user.Alias;
            this.Transactions = user.Transactions.Select(tr => new TransactionViewModel(tr));
            this.Locked = user.Locked;
        }

        public string Id { get; set; }
  
        public DateTime? CreatedOn { get; set; }

        public string Email { get; set; }
        public string Alias { get; set; }

        public bool Locked { get; set; }

		public IEnumerable<TransactionViewModel> Transactions { get; set; }

    }
}