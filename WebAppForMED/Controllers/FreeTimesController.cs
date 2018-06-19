using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppForMED.Models;

namespace WebAppForMED.Controllers
{
    public class FreeTimesController : Controller
    {
        private ModelMEDContainer db = new ModelMEDContainer();

        // GET: FreeTimes
        public ActionResult Index()
        {
            return View(db.FreeTimeSet.ToList());
        }

        // GET: FreeTimes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FreeTime freeTime = db.FreeTimeSet.Find(id);
            if (freeTime == null)
            {
                return HttpNotFound();
            }
            return View(freeTime);
        }

        // GET: FreeTimes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FreeTimes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StartTime")] FreeTime freeTime)
        {
            if (ModelState.IsValid)
            {
                db.FreeTimeSet.Add(freeTime);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(freeTime);
        }

        // GET: FreeTimes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FreeTime freeTime = db.FreeTimeSet.Find(id);
            if (freeTime == null)
            {
                return HttpNotFound();
            }
            return View(freeTime);
        }

        // POST: FreeTimes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartTime")] FreeTime freeTime)
        {
            freeTime = db.FreeTimeSet.Find(freeTime.Id);
            if (freeTime != null)
            {
                db.Entry(freeTime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(freeTime);
        }

        // GET: FreeTimes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FreeTime freeTime = db.FreeTimeSet.Find(id);
            if (freeTime == null)
            {
                return HttpNotFound();
            }
            return View(freeTime);
        }

        // POST: FreeTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FreeTime freeTime = db.FreeTimeSet.Find(id);
            db.FreeTimeSet.Remove(freeTime);
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
