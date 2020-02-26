using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookingsTrips.Helper;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace BookingsTrips.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required_AR]
        [StringLength(100, ErrorMessage = "{0} لابد على الأقل أن تحتوي على {2} حروف أو رموز.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة السر الجديدة")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة السر الجديدة")]
        [Compare("NewPassword", ErrorMessage = "كلمة السر وتأكيدها غير متطابقين.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required_AR]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة السر الحالية")]
        public string OldPassword { get; set; }

        [Required_AR]
        [StringLength(100, ErrorMessage = "{0} لابد على الأقل أن تحتوي على {2} حروف أو رموز.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة السر الجديدة")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة السر الجديدة")]
        [Compare("NewPassword", ErrorMessage = "كلمة السر وتأكيدها غير متطابقين.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required_AR]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required_AR]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required_AR]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}