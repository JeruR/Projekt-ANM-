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
    public class CurrentController : Controller
    {
        private ANMContext db = new ANMContext();

        // GET: Current
        public ActionResult Index()
        {
            return View(db.Current.ToList());
        }
      
        // GET: Current/Details/5
        public ActionResult Details(int? id)
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

        // GET: Current/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Current/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Car,CarRegistration,RentalDate,ReturnDate")] Current current)
        {
            if (ModelState.IsValid)
            {
                db.Current.Add(current);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(current);
        }

        // GET: Current/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Current/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Car,CarRegistration,RentalDate,ReturnDate")] Current current)
        {
            if (ModelState.IsValid)
            {
                db.Entry(current).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(current);
        }

        // GET: Current/Delete/5
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
