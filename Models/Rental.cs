using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_CarSharing.Models
{
    public class Rental
    {
        public int RentalId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RentalDate { get; set; }  // date of when the rental started

        [Required(ErrorMessage = "You must say when you want to deliver the car!")]
        [DataType(DataType.DateTime)]
        public DateTime RentalDeliveryDate { get; set; } // expected vehicle's time delivery

        [ForeignKey("RegularUser")]
        [Required]
        public string RegularUserId { get; set; }
        public ApplicationUser RegularUser { get; set; } // the regularUser that will use the car

        [ForeignKey("Vehicle")]
        [Required]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        [ForeignKey("VehicleStation")]
        [Required]
        public int VehicleStationId { get; set; }
        public VehicleStation VehicleStation { get; set; } //where the RegularUser have to leave the car (the destination)

        [ForeignKey("Delivery")]
        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }

        public Rental()
        {
            this.RentalDate = DateTime.Now;
            this.Vehicle.BeingUsed = true;
        }
    }
}