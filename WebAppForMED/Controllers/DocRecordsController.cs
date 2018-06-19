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
    public class DocRecordsController : Controller
    {
        private ModelMEDContainer db = new ModelMEDContainer();

        // GET: DocRecords
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.DocRecordSet.ToList());
        }

        // GET: DocRecords/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocRecord docRecord = db.DocRecordSet.Find(id);
            if (docRecord == null)
            {
                return HttpNotFound();
            }
            return View(docRecord);
        }

        // GET: DocRecords/Create

        // GET: DocRecords/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocRecord docRecord = db.DocRecordSet.Find(id);
            if (docRecord == null)
            {
                return HttpNotFound();
            }
            return View(docRecord);
        }

        // POST: DocRecords/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Diagnos,RecordDate")] DocRecord docRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(docRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(docRecord);
        }

        // GET: DocRecords/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocRecord docRecord = db.DocRecordSet.Find(id);
            if (docRecord == null)
            {
                return HttpNotFound();
            }
            return View(docRecord);
        }

        // POST: DocRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            DocRecord docRecord = db.DocRecordSet.Find(id);
            db.DocRecordSet.Remove(docRecord);
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
