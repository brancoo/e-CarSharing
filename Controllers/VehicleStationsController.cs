using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using e_CarSharing.Models;

namespace e_CarSharing.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehicleStationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VehicleStations
        public ActionResult Index()
        {
            return View(db.VehicleStations.ToList());
        }

        // GET: VehicleStations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleStation vehicleStation = db.VehicleStations.Find(id);
            if (vehicleStation == null)
            {
                return HttpNotFound();
            }
            return View(vehicleStation);
        }

        // GET: VehicleStations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleStations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehicleStationId,Name,Latitude,Longetide,City")] VehicleStation vehicleStation)
        {
            if (ModelState.IsValid)
            {
                db.VehicleStations.Add(vehicleStation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicleStation);
        }

        // GET: VehicleStations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleStation vehicleStation = db.VehicleStations.Find(id);
            if (vehicleStation == null)
            {
                return HttpNotFound();
            }
            return View(vehicleStation);
        }

        // POST: VehicleStations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehicleStationId,Name,Latitude,Longetide,City")] VehicleStation vehicleStation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleStation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicleStation);
        }

        // GET: VehicleStations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleStation vehicleStation = db.VehicleStations.Find(id);
            if (vehicleStation == null)
            {
                return HttpNotFound();
            }
            return View(vehicleStation);
        }

        // POST: VehicleStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleStation vehicleStation = db.VehicleStations.Find(id);
            db.VehicleStations.Remove(vehicleStation);
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
