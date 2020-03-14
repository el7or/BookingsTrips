using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingsTrips.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Cost { get; set; }
        public decimal AdultPrice { get; set; }
        public decimal TeenPrice { get; set; }
        public decimal ChildPrice { get; set; }
        public decimal BabyPrice { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; }

        public ICollection<Flight> Flights { get; set; }
        public ICollection<Boat> Boats { get; set; }
        public ICollection<TripCabinsPrice> TripCabinsPrices { get; set; }
    }

    public class TripCabinsPrice
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Trip")]
        public int TripId { get; set; }
        public Trip Trip { get; set; }

        [ForeignKey("Floor")]
        public int FloorId { get; set; }
        public Floor Floor { get; set; }

        public decimal TripSingleCabinsPrice { get; set; }
        public decimal TripDoubleCabinsPrice { get; set; }
        public decimal TripTripleCabinsPrice { get; set; }

        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; }
    }
}