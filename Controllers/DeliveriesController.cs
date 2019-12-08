using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using e_CarSharing.Models;
using Microsoft.AspNet.Identity;

namespace e_CarSharing.Controllers
{
    public class DeliveriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult DeliveryVehicle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Rental rental = db.Rentals.Include(r => r.Vehicle).Include(r => r.VehicleStation).FirstOrDefault(x => x.RentalId == id);

            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        [HttpPost, ActionName("DeliveryVehicle")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeliveryVehicleConfirmation(int id)
        {
            Rental rental = db.Rentals.Include(x=>x.RegularUser).Include(x=>x.Vehicle).Include(x=>x.VehicleStation).Include(x=>x.DeliveryVehicleStation).FirstOrDefault(x=>x.RentalId == id);

            if (rental == null)
            {
                return HttpNotFound();
            }

            rental.Vehicle.BeingUsed = false;
            Vehicle vehicle = db.Vehicles.Find(rental.VehicleId);
            vehicle.BeingUsed = false;

            double RentedDays = Math.Abs((DateTime.Now - rental.RentalDate).Days);
            
            Delivery delivery = new Delivery() { RentalId = id, Rental = rental };
            
            switch (rental.VehicleType)
            {
                case (VehicleType.BICYCLE):
                    delivery.RentalCost = (decimal) (RentedDays * 3);
                    break;
                case (VehicleType.BIKE):
                    delivery.RentalCost = (decimal)(RentedDays * 5.5);
                    break; ;
                case (VehicleType.CAR):
                    delivery.RentalCost = (decimal)(RentedDays * 10);
                    break; ;
                case (VehicleType.SCOOTER):
                    delivery.RentalCost = (decimal)(RentedDays * 3.5);
                    break; ;
            }

            if (RentedDays == 0) //if the vehicle was delivered on the same day that was rented
            {
                delivery.RentalCost = 7.5M;
            }

            if (DateTime.Now > rental.DeliveryExpectedDate) //if the User delivered the vehicle after the expected date of delivery
            {
                int totalDaysDelayed = (DateTime.Now - rental.DeliveryExpectedDate).Days;

                delivery.RentalCost = (decimal)(totalDaysDelayed * 7.5) + (DateTime.Now - rental.RentalDate).Days + delivery.RentalCost;  //7.5€ of fee
            }

            if (rental.VehicleStation != rental.DeliveryVehicleStation)
            {
                var VehicleStationList = db.VehicleStations.Include(x => x.Vehicles);
                foreach (var station in VehicleStationList)
                {
                    if (station.VehicleStationId == rental.VehicleStationId)
                    {
                        station.Vehicles.Remove(rental.Vehicle);
                        db.Entry(station).State = EntityState.Modified;
                    }
                    if (station.VehicleStationId == rental.DeliveryVehicleStationId)
                    {
                        station.Vehicles.Add(rental.Vehicle);
                        vehicle.VehicleStationId = rental.VehicleStationId;
                        vehicle.VehicleStation = station;
                        db.Entry(station).State = EntityState.Modified;
                    }
                }
            }

            rental.Delivery = delivery;
            rental.DeliveryId = delivery.DeliveryId;
            

            db.Deliveries.Add(delivery);
            db.Entry(vehicle).State = EntityState.Modified;
            db.Entry(rental).State = EntityState.Modified;

            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        // GET: Deliveries
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            var deliveries = db.Deliveries.Include(d => d.Rental).Where(x => x.Rental.RegularUserId == user);
            return View(deliveries.ToList());
        }

        // GET: Deliveries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery delivery = db.Deliveries.Find(id);
            if (delivery == null)
            {
                return HttpNotFound();
            }
            return View(delivery);
        }

        // GET: Deliveries/Create
        public ActionResult Create()
        {
            ViewBag.RentalId = new SelectList(db.Rentals, "RentalId", "RegularUserId");
            return View();
        }

        // POST: Deliveries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RentalId,DeliveryDate")] Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                db.Deliveries.Add(delivery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RentalId = new SelectList(db.Rentals, "RentalId", "RegularUserId", delivery.RentalId);
            return View(delivery);
        }

        // GET: Deliveries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery delivery = db.Deliveries.Find(id);
            if (delivery == null)
            {
                return HttpNotFound();
            }
            ViewBag.RentalId = new SelectList(db.Rentals, "RentalId", "RegularUserId", delivery.RentalId);
            return View(delivery);
        }

        // POST: Deliveries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentalId,DeliveryDate")] Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(delivery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RentalId = new SelectList(db.Rentals, "RentalId", "RegularUserId", delivery.RentalId);
            return View(delivery);
        }

        // GET: Deliveries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery delivery = db.Deliveries.Find(id);
            if (delivery == null)
            {
                return HttpNotFound();
            }
            return View(delivery);
        }

        // POST: Deliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Delivery delivery = db.Deliveries.Find(id);
            db.Deliveries.Remove(delivery);
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
