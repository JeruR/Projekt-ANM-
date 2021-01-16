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
    //Zabezpieczenie przed nieautoryzowanym dostępem
    [Authorize]
    public class RentalCarsController : Controller
    {
        private ANMContext db = new ANMContext();
        private static Reservation _reservation;
        // GET: Załadowanie widoku samochodów do wypożyczenia
        public ActionResult Index()
        {
            //Weryfikacja które samochody są aktualnie dostępne do zarezerwowania
            if (TempData["data"] != null)
            {
                _reservation = (Reservation)TempData["data"];
                db.RentalCars.RemoveRange(db.RentalCars.Where(x => x.ID == x.ID));
                List<Current> notRented = new List<Current>();
                foreach (var ren in db.Current.Where(m => m.ReturnDate >= DateTime.Now).ToList())
                {
                    if ((_reservation.Date1 >= ren.RentalDate  && _reservation.Date1 <= ren.ReturnDate) || (_reservation.Date2 >= ren.RentalDate && _reservation.Date2 <= ren.ReturnDate) || (_reservation.Date1 <= ren.RentalDate && _reservation.Date2 >= ren.ReturnDate))
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
        //Funkcja przypisuję wypożyczenie samochodu do użytkownika
        public ActionResult Rent(int? id)
        {
            RentalCar rentalCar = db.RentalCars.Find(id);
            db.Current.Add(new Current { ID = rentalCar.ID, Car = rentalCar.CarName, CarRegistration = rentalCar.CarRegistration, RentalDate = _reservation.Date1, ReturnDate=_reservation.Date2});
            db.RentalCars.Remove(rentalCar);
            db.SaveChanges();
            return new RedirectResult(@"~\Current\Index");
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
