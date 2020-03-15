using BookingsTrips.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookingsTrips.Models.ViewModels
{
    public class TripIndexViewModel
    {
        public int Id { get; set; }

        [Display(Name = "الوجهة من")]
        public string StartPoint { get; set; }

        [Display(Name = "الوجهة إلى")]
        public string EndPoint { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        [Display(Name = "التاريخ من")]
        public DateTime FromDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        [Display(Name = "التاريخ إلى")]
        public DateTime ToDate { get; set; }
    }

    public class TripCreateViewModel
    {
        [Required_AR]
        [Display(Name = "الوجهة من")]
        public string StartPoint { get; set; }

        [Required_AR]
        [Display(Name = "الوجهة إلى")]
        public string EndPoint { get; set; }

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
        [RegularExpression(@"^[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "لابد من إدخال صيغة رقمية أو عشرية صحيحة !")]
        [Display(Name = "سعر تكلفة الرحلة")]
        public decimal Cost { get; set; }

        [Required_AR]
        [RegularExpression(@"^[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "لابد من إدخال صيغة رقمية أو عشرية صحيحة !")]
        [Display(Name = "سعر برنامج الرحلة للبالغين")]
        public decimal AdultPrice { get; set; }

        [Required_AR]
        [RegularExpression(@"^[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "لابد من إدخال صيغة رقمية أو عشرية صحيحة !")]
        [Display(Name = "سعر برنامج الرحلة للأطفال أكبر من ست سنوات")]
        public decimal TeenPrice { get; set; }

        [Required_AR]
        [RegularExpression(@"^[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "لابد من إدخال صيغة رقمية أو عشرية صحيحة !")]
        [Display(Name = "سعر برنامج الرحلة للأطفال أصغر من ست سنوات")]
        public decimal ChildPrice { get; set; }

        [Required_AR]
        [RegularExpression(@"^[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "لابد من إدخال صيغة رقمية أو عشرية صحيحة !")]
        [Display(Name = "سعر برنامج الرحلة للأطفال الرضع")]
        public decimal BabyPrice { get; set; }

        [Required_AR]
        [Range(1, int.MaxValue, ErrorMessage = "الطائرة مطلوب !")]
        [Display(Name = "الطائرة")]
        public int FlightId { get; set; }

        [Required_AR]
        [Range(1, int.MaxValue, ErrorMessage = "المركب مطلوب !")]
        [Display(Name = "المركب")]
        public int BoatId { get; set; }
    }

    public class TripEditViewModel
    {
        public int Id { get; set; }

        [Required_AR]
        [Display(Name = "الوجهة من")]
        public string StartPoint { get; set; }

        [Required_AR]
        [Display(Name = "الوجهة إلى")]
        public string EndPoint { get; set; }

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
        [RegularExpression(@"^[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "لابد من إدخال صيغة رقمية أو عشرية صحيحة !")]
        [Display(Name = "سعر تكلفة الرحلة")]
        public decimal Cost { get; set; }

        [Required_AR]
        [RegularExpression(@"^[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "لابد من إدخال صيغة رقمية أو عشرية صحيحة !")]
        [Display(Name = "سعر برنامج الرحلة للبالغين")]
        public decimal AdultPrice { get; set; }

        [Required_AR]
        [RegularExpression(@"^[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "لابد من إدخال صيغة رقمية أو عشرية صحيحة !")]
        [Display(Name = "سعر برنامج الرحلة للأطفال أكبر من ست سنوات")]
        public decimal TeenPrice { get; set; }

        [Required_AR]
        [RegularExpression(@"^[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "لابد من إدخال صيغة رقمية أو عشرية صحيحة !")]
        [Display(Name = "سعر برنامج الرحلة للأطفال أصغر من ست سنوات")]
        public decimal ChildPrice { get; set; }

        [Required_AR]
        [RegularExpression(@"^[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "لابد من إدخال صيغة رقمية أو عشرية صحيحة !")]
        [Display(Name = "سعر برنامج الرحلة للأطفال الرضع")]
        public decimal BabyPrice { get; set; }

        [Required_AR]
        [Range(1, int.MaxValue, ErrorMessage = "الطائرة مطلوب !")]
        [Display(Name = "الطائرة")]
        public int FlightId { get; set; }

        [Required_AR]
        [Range(1, int.MaxValue, ErrorMessage = "المركب مطلوب !")]
        [Display(Name = "المركب")]
        public int BoatId { get; set; }
    }

    public class TripCabinsPriceViewModel
    {
        public int Id { get; set; }
        public int FloorNumber { get; set; }

        [Required_AR]
        [RegularExpression(@"^[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "لابد من إدخال صيغة رقمية أو عشرية صحيحة !")]
        [Display(Name = "سعر الكبائن الفردي")]
        public decimal TripSingleCabinsPrice { get; set; }

        [Required_AR]
        [RegularExpression(@"^[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "لابد من إدخال صيغة رقمية أو عشرية صحيحة !")]
        [Display(Name = "سعر الكبائن الزوجي")]
        public decimal TripDoubleCabinsPrice { get; set; }

        [Required_AR]
        [RegularExpression(@"^[0-9]+(\.[0-9][0-9]?)?", ErrorMessage = "لابد من إدخال صيغة رقمية أو عشرية صحيحة !")]
        [Display(Name = "سعر الكبائن الثلاثي")]
        public decimal TripTripleCabinsPrice { get; set; }

        public int? FloorSingleCabinsCount { get; set; }
        public int? FloorDoubleCabinsCount { get; set; }
        public int? FloorTripleCabinsCount { get; set; }
    }
}