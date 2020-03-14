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
}