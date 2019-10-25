using System.ComponentModel.DataAnnotations;

namespace e_CarSharing.Models
{
    public enum OwnerType
    {
        [Display(Name = "BUSINESS")]
        BUSINESS,

        [Display(Name = "PARTICULAR")]
        PARTICULAR
    }
    public enum VehicleType
    {
        [Display(Name = "BICYCLE")]
        BICYCLE,

        [Display(Name = "BIKE")]
        BIKE,

        [Display(Name = "CAR")]
        CAR,

        [Display(Name = "SCOOTER")] //trotinete
        SCOOTER
    }
}