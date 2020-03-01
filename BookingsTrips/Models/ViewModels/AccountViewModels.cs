using BookingsTrips.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingsTrips.Models
{

    public class LoginViewModel
    {
        [Required_AR]
        [Display(Name = "البريد الإلكتروني")]
        [EmailAddress]
        public string Email { get; set; }

        [Required_AR]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة السر")]
        public string Password { get; set; }

        [Display(Name = "تذكرني؟")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required_AR]
        [EmailAddress]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }

        [Required_AR]
        [StringLength(100, ErrorMessage = "{0} لابد على الأقل أن تحتوي على {2} حروف أو رموز.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة السر")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة السر")]
        [Compare("Password", ErrorMessage = "كلمة السر وتأكيدها غير متطابقين.")]
        public string ConfirmPassword { get; set; }

        [Required_AR]
        [Display(Name = "الإسم")]
        public string FullName { get; set; }

        [Required_AR]
        [MaxLength(12, ErrorMessage = "لابد من إدخال رقم تليفون صحيح.")]
        [MinLength(10, ErrorMessage = "لابد من إدخال رقم تليفون صحيح.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لابد من إدخال رقم تليفون صحيح.")]
        [Display(Name = "رقم التليفون")]
        [Phone]
        public string Phone { get; set; }

        [Required_AR]
        [Display(Name = "القسم")]
        public string RoleName { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; } = DateTime.Now;
    }

    public class UsersListViewModel
    {
        public string Id { get; set; }

        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }

        [Display(Name = "الإسم")]
        public string FullName { get; set; }

        [Display(Name = "رقم التليفون")]
        public string Phone { get; set; }

        [Display(Name = "القسم")]
        public string RoleName { get; set; }
    }
    public class UserEditViewModel
    {
        public string Id { get; set; }

        [Required_AR]
        [EmailAddress]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }

        [Required_AR]
        [Display(Name = "الإسم")]
        public string FullName { get; set; }

        [Required_AR]
        [MaxLength(12, ErrorMessage = "لابد من إدخال رقم تليفون صحيح.")]
        [MinLength(10, ErrorMessage = "لابد من إدخال رقم تليفون صحيح.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لابد من إدخال رقم تليفون صحيح.")]
        [Display(Name = "رقم التليفون")]
        [Phone]
        public string Phone { get; set; }

        [Required_AR]
        [Display(Name = "القسم")]
        public string RoleName { get; set; }

        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; } = DateTime.Now;
    }

    public class ResetPasswordViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} لابد على الأقل أن تحتوي على {2} حروف أو رموز.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة السر الجديدة")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة السر الجديدة")]
        [Compare("Password", ErrorMessage = "كلمة السر وتأكيدها غير متطابقين.")]
        public string ConfirmPassword { get; set; }
    }

    /*
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }
    }
*/
}
