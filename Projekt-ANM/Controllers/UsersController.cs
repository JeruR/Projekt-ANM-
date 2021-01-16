using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Projekt_ANM.DAL;
using Projekt_ANM.Models;

namespace Projekt_ANM.Controllers
{
    //Zabezpieczenie przed nieautoryzowanym dostępem  dostępne tylko dla Administratora
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private ANMContext db = new ANMContext();

        // GET: Załadowanie widoku użytkowników
        public ActionResult Index()
        {
        
                return View(db.Users.ToList());
        }

     
        // GET: Załadowanie widoku nowego użytkownika
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funkcja dodająca nowego użytkownika do bazy
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Login,Name,Surname")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(users);
        }
    
        //Funkcja zmiany hasła dla administratora parametrem jest id użytkownika z bazy
        public ActionResult ChangePassword(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            TempData["passedit"] = id;
            return new RedirectResult(@"~\Manage\ChangePassword");
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
