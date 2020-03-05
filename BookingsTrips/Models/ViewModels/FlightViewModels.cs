using BookingsTrips.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookingsTrips.Models.ViewModels
{
    public class FlightIndexViewModel
    {
        public int Id { get; set; }

        [Display(Name = "الوجهة من")]
        public string FromAirport { get; set; }

        [Display(Name = "الوجهة إلى")]
        public string ToAirport { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        [Display(Name = "التاريخ")]
        public DateTime FromDate { get; set; }

        [Display(Name = "عدد المقاعد")]
        public int Seats { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        [Display(Name = "سعر التذكرة")]
        public decimal Price { get; set; }
    }
    public class FlightCreateViewModel
    {
        [Required_AR]
        [Display(Name = "الوجهة من")]
        public string FromAirport { get; set; }

        [Required_AR]
        [Display(Name = "الوجهة إلى")]
        public string ToAirport { get; set; }

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
        [Display(Name = "عدد مقاعد الطائرة")]
        public int Seats { get; set; }

        [Required_AR]
        [RegularExpression(@"^[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "لابد من إدخال صيغة رقمية أو عشرية صحيحة !")]
        [Display(Name = "سعر تكلفة التذكرة")]
        public decimal Cost { get; set; }

        [Required_AR]
        [RegularExpression(@"^[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "لابد من أن إدخال صيغة رقمية أو عشرية صحيحة !")]
        [Display(Name = "سعر بيع التذكرة")]
        public decimal Price { get; set; }
    }
    public class FlightDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "الوجهة من:")]
        public string FromAirport { get; set; }

        [Display(Name = "الوجهة إلى:")]
        public string ToAirport { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        [Display(Name = "من تاريخ:")]
        public DateTime FromDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        [Display(Name = "إلى تاريخ:")]
        public DateTime ToDate { get; set; }

        [Display(Name = "عدد مقاعد الطائرة:")]
        public int Seats { get; set; }

        [Display(Name = "عدد المقاعد المتاحة:")]
        public int SeatsAvailable { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        [Display(Name = "سعر تكلفة التذكرة:")]
        public decimal Cost { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        [Display(Name = "سعر بيع التذكرة:")]
        public decimal Price { get; set; }

        [Display(Name = "إنشاء بواسطة:")]
        public string CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        [Display(Name = "إنشاء بتاريخ:")]
        public DateTime CreatedOn { get; set; }
    }
    public class FlightEditViewModel
    {
        public int Id { get; set; }

        [Required_AR]
        [Display(Name = "الوجهة من")]
        public string FromAirport { get; set; }

        [Required_AR]
        [Display(Name = "الوجهة إلى")]
        public string ToAirport { get; set; }

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
        [Display(Name = "عدد مقاعد الطائرة")]
        public int Seats { get; set; }

        [Required_AR]
        [RegularExpression(@"^[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "لابد من إدخال صيغة رقمية أو عشرية صحيحة !")]
        [Display(Name = "سعر تكلفة التذكرة")]
        public decimal Cost { get; set; }

        [Required_AR]
        [RegularExpression(@"^[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "لابد من أن إدخال صيغة رقمية أو عشرية صحيحة !")]
        [Display(Name = "سعر بيع التذكرة")]
        public decimal Price { get; set; }
    }
}