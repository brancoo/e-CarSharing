using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_CarSharing.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "You must provide a name!")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must declare what type of vehicle you own!")]
        public VehicleType VehicleType { get; set; }

        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }

        [ForeignKey("VehicleStation")]
        public int VehicleStationId { get; set; }
        public VehicleStation VehicleStation { get; set; }
    }
}