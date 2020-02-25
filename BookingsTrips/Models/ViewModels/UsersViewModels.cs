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

        [Display(Name = "اسم المستخدم")]
        public string FullName { get; set; }

        [Display(Name = "رقم التليفون")]
        public string Phone { get; set; }

        [Display(Name = "القسم")]
        public string RoleName { get; set; }
    }
}