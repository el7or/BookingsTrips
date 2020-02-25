using BookingsTrips.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BookingsTrips.Models.ViewModels
{
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
        [Display(Name = "رقم التليفون")]
        [Phone]
        public string Phone { get; set; }

        [Required_AR]
        [Display(Name = "القسم")]
        public string RoleName { get; set; }
    }
}