using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Web.Utilities;
using WebCasino.Web.Utilities.CustomAttributes;

namespace WebCasino.Web.Models.AccountViewModels
{
    public class RegisterViewModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Range(1,4)]
        public int Currency { get; set; }

        [Required]
        [StringLength(10, MinimumLength =3)]
        public string Alias { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateValidator(ErrorMessage ="You must be at least 18 to participate in gambling activities.")]
        public DateTime DateOfBirth { get; set; }

    }
}
