using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_CarSharing.Models
{
    public class Delivery
    {
        [DataType(DataType.DateTime)]
        public DateTime DeliveryDate { get; set; } //the actual date that the vehicle was delivered

        [Key, ForeignKey("Rental")]
        [Required]
        public int RentalId { get; set; }
        public Rental Rental { get; set; }

        public Delivery()
        {
            this.DeliveryDate = DateTime.Now;
            this.Rental.Vehicle.BeingUsed = false;
            this.Rental.Vehicle.VehicleStation = this.Rental.VehicleStation;
            this.Rental.VehicleStationId = this.Rental.VehicleStationId;
        }
    }
}