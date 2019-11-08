using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using e_CarSharing.Models;
using Microsoft.AspNet.Identity;

namespace e_CarSharing.Controllers
{
    public class VehiclesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vehicles
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            var vehicles = db.Vehicles.Where(x => x.OwnerId == userID).Include(v => v.Owner).Include(v => v.VehicleStation);
            return View(vehicles.ToList());
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles/Create
        [Authorize(Roles = "Owner")]
        public ActionResult Create()
        {
            ViewBag.VehicleStationId = new SelectList(db.VehicleStations, "VehicleStationId", "Name");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Owner")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VehicleId,Name,VehicleType,OwnerId,VehicleStationId,BeingUsed")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                vehicle.BeingUsed = false;
                vehicle.OwnerId = User.Identity.GetUserId();
                vehicle.Owner = db.Users.FirstOrDefault(x => x.Id == vehicle.OwnerId);
                VehicleStation posto = db.VehicleStations.FirstOrDefault(x => x.VehicleStationId == vehicle.VehicleStationId);  //vai buscar o posto onde queremos adicionar o veiculo
                posto.Vehicles.Add(vehicle);    //adicionamos o veiculo que foi criado a lista de veiculos do posto
                db.Entry(posto).State = EntityState.Modified;   //atualiza os dados do posto
                db.Vehicles.Add(vehicle);

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewBag.VehicleStationId = new SelectList(db.VehicleStations, "VehicleStationId", "Name", vehicle.VehicleStationId);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.VehicleStationId = new SelectList(db.VehicleStations, "VehicleStationId", "Name", vehicle.VehicleStationId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VehicleId,Name,OwnerId,VehicleType,VehicleStationId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                vehicle.OwnerId = User.Identity.GetUserId();
                db.Entry(vehicle).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerId = new SelectList(db.Owner, "OwnerId", "Name", vehicle.OwnerId);
            ViewBag.VehicleStationId = new SelectList(db.VehicleStations, "VehicleStationId", "Name", vehicle.VehicleStationId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
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
