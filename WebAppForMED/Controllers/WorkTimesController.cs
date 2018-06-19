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
    public class WorkTimesController : Controller
    {
        private ModelMEDContainer db = new ModelMEDContainer();

        // GET: WorkTimes

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.WorkTimeSet.ToList());
        }

        // GET: WorkTimes/Details/5

        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkTime workTime = db.WorkTimeSet.Find(id);
            if (workTime == null)
            {
                return HttpNotFound();
            }
            return View(workTime);
        }

        // GET: WorkTimes/Create

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkTimes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "Id,StartTime")] WorkTime workTime)
        {
            if (ModelState.IsValid)
            {
                db.WorkTimeSet.Add(workTime);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workTime);
        }

        // GET: WorkTimes/Edit/5

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkTime workTime = db.WorkTimeSet.Find(id);
            if (workTime == null)
            {
                return HttpNotFound();
            }
            return View(workTime);
        }

        // POST: WorkTimes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,StartTime")] WorkTime workTime)
        {
            workTime = db.WorkTimeSet.Find(workTime.Id);
            if (workTime != null)
            {
                db.Entry(workTime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workTime);
        }

        // GET: WorkTimes/Delete/5

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkTime workTime = db.WorkTimeSet.Find(id);
            if (workTime == null)
            {
                return HttpNotFound();
            }
            return View(workTime);
        }

        // POST: WorkTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkTime workTime = db.WorkTimeSet.Find(id);
            db.WorkTimeSet.Remove(workTime);
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
