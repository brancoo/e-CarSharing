using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using e_CarSharing.Models;
using Microsoft.AspNet.Identity;

namespace e_CarSharing.Controllers
{
    public class RentalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rentals
        [Authorize(Roles ="User")]
        public ActionResult Index()
        {

            var userID = User.Identity.GetUserId();
            var rentals = db.Rentals.Where(x => x.RegularUserId == userID).Include(r => r.Vehicle).Include(r => r.VehicleStation);
            return View(rentals.ToList());
        }

        // GET: Rentals/Details/5
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
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.VehicleStationId = new SelectList(db.VehicleStations.Where(x => x.Vehicles.Count != 0), "VehicleStationId", "Name");
            ViewBag.VehicleId = new SelectList(db.Vehicles.Where(x => x.BeingUsed == false), "VehicleId", "Name");
            return View();
        }



        // POST: Rentals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Rental rental)
        {
            if (ModelState.IsValid)
            {
                Vehicle aux = db.Vehicles.FirstOrDefault(x => x.VehicleId == rental.VehicleId);
                if (aux.VehicleType != rental.VehicleType)
                {
                    ViewBag.VehicleStationId = new SelectList(db.VehicleStations.Where(x => x.Vehicles.Count != 0), "VehicleStationId", "Name");
                    ViewBag.VehicleId = new SelectList(db.Vehicles.Where(x => x.BeingUsed == false), "VehicleId", "Name");
                    return View(rental);
                }
                VehicleStation aux2 = db.VehicleStations.FirstOrDefault(x => x.VehicleStationId == rental.VehicleStationId);
                foreach (Vehicle v in aux2.Vehicles)
                {
                    if (v.VehicleId == rental.VehicleId)
                    {
                        aux.BeingUsed = true;
                        rental.RegularUserId = User.Identity.GetUserId();
                        rental.RegularUser = db.Users.FirstOrDefault(x => x.Id == rental.RegularUserId);
                        rental.Vehicle = aux;
                        rental.VehicleStationId = rental.VehicleStationId;
                        rental.VehicleStation = aux2;
                        db.Rentals.Add(rental);
                        db.Entry(aux).State = EntityState.Modified;
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

        public JsonResult FillVehicles(int ID)
        {
            //int VehicleStationId;
            //var vehicles = db.Vehicles.Where(c => c.VehicleStationId == VehicleStationId);
            //db.Configuration.ProxyCreationEnabled = false;
            //var vehicles = db.Vehicles.Where(c => c.VehicleStationId == VehicleStationId)
            //                        .Select(x => new SelectListItem { Value = x.VehicleId.ToString()   , Text = x.Name }).ToList();
            //return Json(new SelectList(vehicles, "Value", "Text"));

            //List<Vehicle> aux = db.Vehicles.Where(c => c.VehicleStationId == ID).ToList();
            //return Json(aux, JsonRequestBehavior.AllowGet);

            return Json(new { @success = true });
            //return Json("fodasse", JsonRequestBehavior.AllowGet);
        }

        // GET: Rentals/Edit/5
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
