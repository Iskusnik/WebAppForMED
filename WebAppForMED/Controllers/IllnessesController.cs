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
    public class IllnessesController : Controller
    {
        private ModelMEDContainer db = new ModelMEDContainer();

        // GET: Illnesses
        public ActionResult Index()
        {
            return View(db.IllnessSet.ToList());
        }

        // GET: Illnesses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Illness illness = db.IllnessSet.Find(id);
            if (illness == null)
            {
                return HttpNotFound();
            }
            return View(illness);
        }

        // GET: Illnesses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Illnesses/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Illness illness)
        {
            if (ModelState.IsValid)
            {
                db.IllnessSet.Add(illness);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(illness);
        }

        // GET: Illnesses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Illness illness = db.IllnessSet.Find(id);
            if (illness == null)
            {
                return HttpNotFound();
            }
            return View(illness);
        }

        // POST: Illnesses/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Illness illness)
        {
            if (ModelState.IsValid)
            {
                db.Entry(illness).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(illness);
        }

        // GET: Illnesses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Illness illness = db.IllnessSet.Find(id);
            if (illness == null)
            {
                return HttpNotFound();
            }
            return View(illness);
        }

        // POST: Illnesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Illness illness = db.IllnessSet.Find(id);
            db.IllnessSet.Remove(illness);
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
