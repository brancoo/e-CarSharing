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
        public DateTime DeliveryExpectedDate { get; set; } // expected vehicle's time delivery

        [ForeignKey("RegularUser")]
        //[Required]
        public string RegularUserId { get; set; }
        public ApplicationUser RegularUser { get; set; } // the regularUser that will use the car

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        [ForeignKey("VehicleStation")]
        public int VehicleStationId { get; set; }
        public VehicleStation VehicleStation { get; set; } //where the RegularUser have to leave the car (the destination)

        public int DeliveryId { get; set; }
        public virtual Delivery Delivery { get; set; }

        [Required(ErrorMessage = "You must declare what type of vehicle you own!")]
        public VehicleType VehicleType { get; set; }            //NAO REMOVER -> FALAR COM O ALEX PRIMEIRO

        public Rental()
        {
            this.RentalDate = DateTime.Now;
            this.DeliveryExpectedDate = DateTime.Now;
        }
    }
}