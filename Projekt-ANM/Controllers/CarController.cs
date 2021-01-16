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
    //Zabezpieczenie przed nieautoryzowanym dostępem dostępne tylko dla Administratora
    [Authorize(Roles = "Administrator")]
    public class CarController : Controller
    {
        //Inicjalizacja bazy
        private ANMContext db = new ANMContext();

        // GET: Załadowanie widoku listy samochodów
        public ActionResult Index()
        {
            return View(db.Cars.ToList());
        }

        // GET: Załadowanie widoku dodaj samochód
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funkcja dodawania samochodu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CarName,CarRegistration,VIN,ProductionYear")] Car car)
        {

            if (ModelState.IsValid)
            {    
                foreach (var item in db.Cars)
                {
                    if (item.VIN == car.VIN)
                    {
                        ModelState.AddModelError("ProductionYear", "Istnieje już taki samochód.");

                        return View("Create");
                    }

                }
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car);
        }

        // GET: Załadowanie widoku edycji samochodu (id = numer samochodu na liscie)
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Funkcja edycji samochodu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CarName,CarRegistration,VIN,ProductionYear")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: Załadowanie widoku usuwania samochodu(id = numer samochodu na liscie)
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Funkcja usuwania samochodu
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            Current current = db.Current.Find(id);
            db.Cars.Remove(car);
            db.Current.Remove(current);
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
