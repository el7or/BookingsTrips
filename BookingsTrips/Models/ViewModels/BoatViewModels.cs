using BookingsTrips.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookingsTrips.Models.ViewModels
{
    public class BoatIndexViewModel
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        [Display(Name = "من تاريخ")]
        public DateTime FromDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        [Display(Name = "إلى تاريخ")]
        public DateTime ToDate { get; set; }

        [Display(Name = "عدد الأدوار")]
        public int FloorsCount { get; set; } = 0;

        [Display(Name = "إجمالي عدد الكبائن")]
        public int? CabinsCount { get; set; } = 0;
    }
    public class BoatCreateViewModel
    {
        [Required_AR]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        [Display(Name = "من تاريخ")]
        public DateTime FromDate { get; set; }

        [Required_AR]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        [Display(Name = "إلى تاريخ")]
        public DateTime ToDate { get; set; }

        [Required_AR]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لابد من إدخال رقم صحيح !")]
        [Display(Name = "عدد الأدوار")]
        public int FloorsCount { get; set; }
    }
}