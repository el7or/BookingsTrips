using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingsTrips.Models
{
    public class Boat
    {
        [Key]
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int FloorsCount { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; }

        public ICollection<Floor> Floors { get; set; }
    }
    public class Floor
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Boat")]
        public int BoatId { get; set; }
        public Boat Boat { get; set; }

        public int FloorNumber { get; set; }
        public int? FloorCabinsCount { get; set; }
        public int? FloorSingleCabinsCount { get; set; }
        public int? FloorDoubleCabinsCount { get; set; }
        public int? FloorTripleCabinsCount { get; set; }

        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; }

        public ICollection<UserCabinsCount> UserCabinsCounts { get; set; }
    }
    public class UserCabinsCount
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Floor")]
        public int FloorId { get; set; }
        public Floor Floor { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int UserSingleCabinsCount { get; set; }
        public int UserDoubleCabinsCount { get; set; }
        public int UserTripleCabinsCount { get; set; }

        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; }
    }
}