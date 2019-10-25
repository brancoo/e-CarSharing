﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_CarSharing.Models
{
    public class VehicleStation
    {
        public int VehicleStationId { get; set; }

        [Required(ErrorMessage = "You must provide a name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must provide a latitude")]
        [Display(Name = "Latitude")]
        [DataType(DataType.Text)]
        public Double Latitude { get; set; }

        [Required(ErrorMessage = "You must provide a longitude")]
        [Display(Name = "Longitude")]
        [DataType(DataType.Text)]
        public Double Longetide { get; set; } 

        [Required(ErrorMessage = "You must declare where are you from!")]
        [DataType(DataType.Text)]
        public string Country { get; set; }

        [Required(ErrorMessage = "You must say where the station is from")]
        [DataType(DataType.Text)]
        public string City { get; set; }

        public IList<Vehicle> Vehicles {get; set;} //list of vehicles in the station
    }
}