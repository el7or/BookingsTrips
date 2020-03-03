using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookingsTrips.Models
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        public string FromAirport { get; set; }
        public string ToAirport { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Seats { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; }
    }
}