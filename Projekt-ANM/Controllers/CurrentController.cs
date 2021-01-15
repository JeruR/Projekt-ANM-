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
    public class CurrentController : Controller
    {
        private ANMContext db = new ANMContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Current.ToList().Where(m => m.ReturnDate >= DateTime.Now).OrderBy(x => x.RentalDate));

        }
             
        [HttpGet]
        public ActionResult Histories()
        {
            return View(db.Current.Where(m => m.ReturnDate <= DateTime.Now).ToList().OrderBy(x => x.RentalDate));
        }
 

        // GET: Current/Delete/5
        [Authorize(Roles = "Administrator")]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Current current = db.Current.Find(id);
            if (current == null)
            {
                return HttpNotFound();
            }
            return View(current);
        }

        // POST: Current/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Current current = db.Current.Find(id);
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
