using Projekt_ANM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Projekt_ANM.Controllers
{    
    //Zabezpieczenie przed nieautoryzowanym dostępem
    [Authorize]
    public class HomeController : Controller
    { 
        // GET: Załadowanie widoku rezerwacji
        [HttpGet]
        public ActionResult Reservation()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                return View();

            }
            else
            {
                return new RedirectResult(@"~\Account\Login");

            }
        }

        // POST: Funkcja werfikująca daty wejściowe oraz przekazujaca je do controlera aktywnych rezerwacji 
        [HttpPost]
        public ActionResult Reservation(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                if (reservation.Date1 > reservation.Date2)
                {
                    ModelState.AddModelError("Date2", "Data wypożyczenia nie może być większa niż data zwrotu.");

                    return View(reservation);
                }
                else if (reservation.Date1 < DateTime.Now)
                {
                    ModelState.AddModelError("Date2", "Data wypożyczenia nie może być mniejsza lub równa dacie teraz.");

                    return View(reservation);
                }
                else
                {
                    TempData["data"] = reservation;
                    return new RedirectResult(@"~\RentalCars\Index");
                }
            }
            return View(reservation);
        }

    }
}