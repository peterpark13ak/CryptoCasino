using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Entities;

namespace WebCasino.Web.Areas.Administration.Models
{
    public class UserSettingsViewModel
    {
        public UserSettingsViewModel()
        {

        }
        public UserSettingsViewModel(User user)
        {
            this.Id = user.Id;
            this.Alias = user.Alias;
            this.Email = user.Email;
            this.DateOfBirth = user.DateOfBirth;
            this.Locked = user.Locked;
            this.IsDeleted = user.IsDeleted;
            this.DeletedOn = user.DeletedOn;
            this.CreatedOn = user.CreatedOn;
            this.ModifiedOn = user.ModifiedOn;
        }

        public string Id { get; set; }
        public string Alias { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool Locked { get; set; }

        public bool IsPromoted { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

    }
}
