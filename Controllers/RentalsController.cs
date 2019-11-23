using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using e_CarSharing.Models;
using Microsoft.AspNet.Identity;

namespace e_CarSharing.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class RentalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Rentals
        [Authorize(Roles = "User")]
        public ActionResult Index()
        {

            var userID = User.Identity.GetUserId();
            var rentals = db.Rentals.Include(x=>x.Vehicle).Include(x=>x.VehicleStation).Where(x=>x.RegularUserId == userID);

            return View(rentals.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ListAllRentals()
        {
            var rentals = db.Rentals.Include(x => x.Vehicle).Include(x => x.VehicleStation).OrderBy(x=>x.RentalId);
            return View(rentals.ToList());
        }


        // GET: Rentals/Details/5
        [Authorize(Roles = "User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        //GET: Rentals/Create
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            ViewBag.VehicleStationId = new SelectList(db.VehicleStations.Where(x => x.Vehicles.Count != 0), "VehicleStationId", "Name");
            ViewBag.VehicleId = new SelectList(db.Vehicles.Where(x => x.BeingUsed == false), "VehicleId", "Name");
            return View();
        }



        // POST: Rentals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Rental rental)   //async Task<ActionResult>
        {
            if (ModelState.IsValid)
            {
                Vehicle vehicle = db.Vehicles.Include(x=>x.Owner).Include(x=>x.VehicleStation).FirstOrDefault(x => x.VehicleId == rental.VehicleId);
                if (vehicle.VehicleType != rental.VehicleType)
                {
                    ViewBag.VehicleStationId = new SelectList(db.VehicleStations.Where(x => x.Vehicles.Count != 0), "VehicleStationId", "Name");
                    ViewBag.VehicleId = new SelectList(db.Vehicles.Where(x => x.BeingUsed == false), "VehicleId", "Name");
                    return View(rental);
                }
                VehicleStation vehicleStation = db.VehicleStations.Include(x=>x.Vehicles).FirstOrDefault(x => x.VehicleStationId == rental.VehicleStationId);
                foreach (Vehicle v in vehicleStation.Vehicles)
                {
                    if (v.VehicleId == rental.VehicleId)
                    {
                        vehicle.BeingUsed = true;
                        string userID = User.Identity.GetUserId();
                        var regularUser = db.Users.Find(userID);
                        rental.RegularUserId = userID;
                        rental.RegularUser = regularUser;
                        rental.VehicleStation = vehicleStation;
                        rental.Vehicle = vehicle;

                        db.Rentals.Add(rental);
                        db.Entry(vehicle).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
                ViewBag.VehicleStationId = new SelectList(db.VehicleStations.Where(x => x.Vehicles.Count != 0), "VehicleStationId", "Name");
                ViewBag.VehicleId = new SelectList(db.Vehicles.Where(x => x.BeingUsed == false), "VehicleId", "Name");
                return View(rental);
            }

            ViewBag.VehicleStationId = new SelectList(db.VehicleStations.Where(x => x.Vehicles.Count != 0), "VehicleStationId", "Name");
            ViewBag.VehicleId = new SelectList(db.Vehicles.Where(x => x.BeingUsed == false), "VehicleId", "Name");

            return View(rental);
        }


        // GET: Rentals/Edit/5
        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            ViewBag.RentalId = new SelectList(db.Deliveries, "RentalId", "RentalId", rental.RentalId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "Name", rental.VehicleId);
            ViewBag.VehicleStationId = new SelectList(db.VehicleStations, "VehicleStationId", "Name", rental.VehicleStationId);
            return View(rental);
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentalId,RentalDate,RentalDeliveryDate,RegularUserId,VehicleId,VehicleStationId,DeliveryId")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rental).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RentalId = new SelectList(db.Deliveries, "RentalId", "RentalId", rental.RentalId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "Name", rental.VehicleId);
            ViewBag.VehicleStationId = new SelectList(db.VehicleStations, "VehicleStationId", "Name", rental.VehicleStationId);
            return View(rental);
        }

        // GET: Rentals/Delete/5
        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // POST: Rentals/Delete/5
        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rental rental = db.Rentals.Find(id);
            db.Rentals.Remove(rental);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
