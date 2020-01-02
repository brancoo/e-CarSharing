﻿using e_CarSharing.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_CarSharing.Controllers
{
    public class StatisticsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Statistics
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {

            List<DataPoint> dataPoints = new List<DataPoint>();

            var rentals = db.Rentals;
            int totalRentals = rentals.Count();

            foreach(var stations in db.VehicleStations)
            {
                int Nrentals = rentals.Where(x => x.VehicleStationId == stations.VehicleStationId).Count();
                dataPoints.Add(new DataPoint(stations.Name, Math.Round((double)(100 * Nrentals)/totalRentals,2)));
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }
    }
}