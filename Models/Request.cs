using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_CarSharing.Models
{
    public class Request
    {
        public int RequestId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RequestDate { get; set; }

        [Required(ErrorMessage = "You must declare the expected time of the ride!")]
        [DataType(DataType.Duration)]
        public int ExpectedTime { get; set; } // expected time of use

        [ForeignKey("RegularUser")]
        public int RegularUserId { get; set; }
        public RegularUser RegularUser { get; set; } // the regularUser that will use the car

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        [Required]
        [ForeignKey("VehicleStation")]
        public int VehicleStationId { get; set; }
        public VehicleStation VehicleStation { get; set; } //where the RegularUser have to leave the car (the destination)
    }
}