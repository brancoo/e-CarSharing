using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace e_CarSharing.Models
{
    public class Owner
    {
        public int OwnerId { get; set; }

        [Required(ErrorMessage = "You must provide a name!")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must declare what type of owner are you!")]
        public OwnerType OwnerType {get; set;} //type of vehicle's owner (BUSINESS OR PARTICULAR)

        [Required(ErrorMessage = "You must provide a city!")]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [Required(ErrorMessage = "You must provide a address!")]
        [DataType(DataType.MultilineText), MaxLength(120)]
        public string Address { get; set; }

        public IList<Vehicle> Vehicles {get; set;} //list of vehicles owned
    }
}