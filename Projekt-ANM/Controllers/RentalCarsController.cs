using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projekt_ANM.DAL;
using Projekt_ANM.Models;

namespace Projekt_ANM.Controllers
{
    [Authorize]
    public class RentalCarsController : Controller
    {
        private ANMContext db = new ANMContext();
        private static Reservation _reservation;
        // GET: RentalCars
        public ActionResult Index()
        {
            if (TempData["data"] != null)
            {
                _reservation = (Reservation)TempData["data"];
                // return View(db.Current.ToList().Where(m => m.RentalDate <= b.Date1 && m.ReturnDate >= b.Date2));
                db.RentalCars.RemoveRange(db.RentalCars.Where(x => x.ID == x.ID));
                List<Current> notRented = new List<Current>();
                foreach (var ren in db.Current.Where(m => m.ReturnDate >= DateTime.Now).ToList())
                {
                   // if (ren.RentalDate <= _reservation.Date1 && ren.ReturnDate >= _reservation.Date2)
                    if (ren.RentalDate <= _reservation.Date1 && ren.ReturnDate <= _reservation.Date1)
                    {
                        notRented.Add(ren);
                    }
                }
                foreach (var item in db.Cars)
                {
                    var a = notRented.Find(x => x.CarRegistration == item.CarRegistration);
                    if (a == null)
                    {
                        db.RentalCars.Add(new RentalCar { ID= item.ID ,CarName = item.CarName, CarRegistration = item.CarRegistration, VIN = item.VIN, ProductionYear = item.ProductionYear });

                    } 
                }
                db.SaveChanges();

                return View(db.RentalCars.ToList());
            }
            else
            {
                return new RedirectResult(@"~\Current\Index");
            }
        }
        public ActionResult Rent(int? id)
        {
            RentalCar rentalCar = db.RentalCars.Find(id);
            db.Current.Add(new Current { ID = rentalCar.ID, Car = rentalCar.CarName, CarRegistration = rentalCar.CarRegistration, RentalDate = _reservation.Date1, ReturnDate=_reservation.Date2});
            db.RentalCars.Remove(rentalCar);
            db.SaveChanges();
            return new RedirectResult(@"~\Current\Index");
        }


        // GET: RentalCars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalCar rentalCar = db.RentalCars.Find(id);
            if (rentalCar == null)
            {
                return HttpNotFound();
            }
            return View(rentalCar);
        }

        // POST: RentalCars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CarName,CarRegistration,VIN,ProductionYear")] RentalCar rentalCar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentalCar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rentalCar);
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
