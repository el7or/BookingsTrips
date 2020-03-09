using BookingsTrips.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookingsTrips.Models.ViewModels
{
    public class CustomerIndexViewModel
    {
        public int Id { get; set; }

        [Display(Name = "الإسم")]
        public string Name { get; set; }

        [Display(Name = "رقم التليفون")]
        public string Phone { get; set; }

        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }

        [Display(Name = "آخر اتصالات")]
        public ICollection<LastCall> LastCalls { get; set; }
    }

    public class LastCall
    {
        public string Note { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }
    }

    public class CustomerCreateViewModel
    {
        [Required_AR]
        [StringLength(100, ErrorMessage = "{0} لابد على الأقل أن يحتوي على {2} حروف أو رموز.", MinimumLength = 7)]
        [Display(Name = "الإسم")]
        public string Name { get; set; }

        [Required_AR]
        [MaxLength(12, ErrorMessage = "لابد من إدخال رقم تليفون صحيح.")]
        [MinLength(10, ErrorMessage = "لابد من إدخال رقم تليفون صحيح.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لابد من إدخال رقم تليفون صحيح.")]
        [Display(Name = "رقم التليفون")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "الرجاء إدخال بريد إلكتروني صحيح !")]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }

        [Required_AR]
        [Display(Name = "سبب الإتصال")]
        public string CallNote { get; set; }
    }

    public class CustomerEditViewModel
    {
        public int Id { get; set; }

        [Required_AR]
        [StringLength(100, ErrorMessage = "{0} لابد على الأقل أن يحتوي على {2} حروف أو رموز.", MinimumLength = 7)]
        [Display(Name = "الإسم")]
        public string Name { get; set; }

        [Required_AR]
        [MaxLength(12, ErrorMessage = "لابد من إدخال رقم تليفون صحيح.")]
        [MinLength(10, ErrorMessage = "لابد من إدخال رقم تليفون صحيح.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لابد من إدخال رقم تليفون صحيح.")]
        [Display(Name = "رقم التليفون")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "الرجاء إدخال بريد إلكتروني صحيح !")]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }
    }

    public class CustomerCreateCallViewModel
    {
        public int CustomerId { get; set; }

        [Display(Name = "الإسم")]
        public string Name { get; set; }

        [Display(Name = "رقم التليفون")]
        public string Phone { get; set; }

        [Required_AR]
        [Display(Name = "سبب الإتصال")]
        public string CallNote { get; set; }
    }

    public class CustomerCallListViewModel
    {
        [Display(Name = "مستقبل الإتصال")]
        public string CreatedByName { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [Display(Name = "تاريخ الاتصال")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "سبب الإتصال")]
        public string CallNote { get; set; }
    }
}