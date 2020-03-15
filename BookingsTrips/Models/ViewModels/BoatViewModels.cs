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

        [Required_AR]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لابد من إدخال رقم صحيح !")]
        [Display(Name = "عدد المستخدمين المسموح لهم بالحجز")]
        public int UsersCount { get; set; }
    }
    public class BoatEditViewModel
    {
        public int Id { get; set; }

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

        [Required_AR]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لابد من إدخال رقم صحيح !")]
        [Display(Name = "عدد المستخدمين المسموح لهم بالحجز")]
        public int UsersCount { get; set; }
    }
    public class FloorCabinsCountViewModel
    {
        public int Id { get; set; }

        public int FloorNumber { get; set; }

        [Required_AR]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لابد من إدخال رقم صحيح !")]
        [Display(Name = "عدد الكبائن الفردي")]
        public int? FloorSingleCabinsCount { get; set; }

        [Required_AR]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لابد من إدخال رقم صحيح !")]
        [Display(Name = "عدد الكبائن الزوجي")]
        public int? FloorDoubleCabinsCount { get; set; }

        [Required_AR]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لابد من إدخال رقم صحيح !")]
        [Display(Name = "عدد الكبائن الثلاثي")]
        public int? FloorTripleCabinsCount { get; set; }
    }
    public class UserCabinsCountViewModel
    {
        public int Id { get; set; }

        public int FloorNumber { get; set; }

        [Display(Name = "الكبائن الفردي")]
        public int? FloorSingleCabinsCount { get; set; }
        public int? FloorSingleCabinsAssignedCount { get; set; }

        [Display(Name = "الكبائن الزوجي")]
        public int? FloorDoubleCabinsCount { get; set; }
        public int? FloorDoubleCabinsAssignedCount { get; set; }

        [Display(Name = "الكبائن الثلاثي")]
        public int? FloorTripleCabinsCount { get; set; }
        public int? FloorTripleCabinsAssignedCount { get; set; }

        public List<FloorCabinsUser> FloorCabinsUsers { get; set; }
    }

    public class FloorCabinsUser
    {
        public int Id { get; set; }

        [Required_AR]
        public string UserId { get; set; }

        [Required_AR]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لابد من إدخال رقم صحيح !")]
        [Display(Name = "الكبائن الفردي")]
        public int UserSingleCabinsCount { get; set; }

        [Required_AR]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لابد من إدخال رقم صحيح !")]
        [Display(Name = "الكبائن الزوجي")]
        public int UserDoubleCabinsCount { get; set; }

        [Required_AR]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لابد من إدخال رقم صحيح !")]
        [Display(Name = "الكبائن الثلاثي")]
        public int UserTripleCabinsCount { get; set; }
    }

    public class BoatDetailsViewModel
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        [Display(Name = "من تاريخ:")]
        public DateTime FromDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        [Display(Name = "إلى تاريخ:")]
        public DateTime ToDate { get; set; }

        [Display(Name = "عدد الأدوار:")]
        public int FloorsCount { get; set; }

        [Display(Name = "عدد المستخدمين المسموح لهم بالحجز:")]
        public int UsersCount { get; set; }

        [Display(Name = "إنشاء بواسطة:")]
        public string CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        [Display(Name = "إنشاء بتاريخ:")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "آخر تعديل بواسطة:")]
        public string EditedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        [Display(Name = "آخر تعديل بتاريخ:")]
        public DateTime EditedOn { get; set; }

        public List<BoatDetailsFloorCabinsCount> BoatDetailsFloorCabinsCounts { get; set; }
    }
    public class BoatDetailsFloorCabinsCount
    {
        public int FloorNumber { get; set; }

        [Display(Name = "عدد الكبائن الفردي:")]
        public int? FloorSingleCabinsCount { get; set; }

        [Display(Name = "عدد الكبائن الزوجي:")]
        public int? FloorDoubleCabinsCount { get; set; }

        [Display(Name = "عدد الكبائن الثلاثي:")]
        public int? FloorTripleCabinsCount { get; set; }

        public List<BoatDetailsUserCabinsCount> BoatDetailsUserCabinsCounts { get; set; }
    }
    public class BoatDetailsUserCabinsCount
    {
        [Display(Name = "إسم المستخدم:")]
        public string UserFullName { get; set; }

        [Display(Name = "الكبائن الفردي المخصصة:")]
        public int UserSingleCabinsCount { get; set; }

        [Display(Name = "الكبائن الزوجي المخصصة:")]
        public int UserDoubleCabinsCount { get; set; }

        [Display(Name = "الكبائن الثلاثي المخصصة:")]
        public int UserTripleCabinsCount { get; set; }
    }
}