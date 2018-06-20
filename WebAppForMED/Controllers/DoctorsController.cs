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
    public class DoctorsController : Controller
    {
        private ModelMEDContainer db = new ModelMEDContainer();

        // GET: Doctors
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.DoctorSet.ToList());
        }

        // GET: Doctors/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.DoctorSet.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // GET: Doctors/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "Id,FIO,Nation,BirthDate,BirthPlace,LivePlace,Pol,Job,Insurance,DocType,DocNum")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.DoctorSet.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doctor);
        }

        // GET: Doctors/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.DoctorSet.Find(id);
            ViewBag.FreeTimeList = new SelectList(doctor.FreeTime, "Id", "StartTime");
            ViewBag.WorkTimeList = new SelectList(doctor.WorkTime, "Id", "StartTime");
            foreach (WorkTime wt in doctor.WorkTime)
                ViewData[wt.StartTime.ToString()] = wt.Patient.FIO;
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,FIO,Nation,BirthDate,BirthPlace,LivePlace,Pol,Job,Insurance,DocType,DocNum")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.DoctorSet.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doctor doctor = db.DoctorSet.Find(id);
            db.DoctorSet.Remove(doctor);
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

        [Authorize(Roles = "admin")]
        public PartialViewResult ListView()
        {
            SelectList doctors = new SelectList(db.DoctorSet, "Id", "FIO");
            ViewBag.Doctors = doctors;
            return PartialView();
        }


        [AcceptVerbs("Get", "Post")]
        public JsonResult CheckDocNum(string DocNum, string DocType, int Id = 0)
        {
            List<Doctor> resD = (from d in db.DoctorSet where d.DocType == DocType && d.DocNum == DocNum select d).ToList();
            List<Patient> resP = (from d in db.PatientSet where d.DocType == DocType && d.DocNum == DocNum select d).ToList();
            var result = (resD.Count == 0 && resP.Count == 0);

            if (Id != 0)
            {
                resD.Remove(db.DoctorSet.Find(Id));
                result = (resD.Count == 0 && resP.Count == 0);
            }

            if (result)
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "admin")]
        public ActionResult AddFreeTime(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.DoctorSet.Find(id);

            ViewBag.doctorId = id;
            if (doctor == null)
            {
                return HttpNotFound();
            }

            
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddFreeTime(int doctorId, [Bind(Include = "StartTime")]FreeTime freeTime)
        {
            Doctor doctor = db.DoctorSet.Find(doctorId);
            freeTime.Doctor = doctor;

            
            if (ModelState.IsValid)
            {
                freeTime = db.FreeTimeSet.Add(freeTime);
                doctor.FreeTime.Add(freeTime);
                db.SaveChanges();
                ViewBag.FreeTimeList = new SelectList(doctor.FreeTime, "Id", "StartTime");
                ViewBag.WorkTimeList = new SelectList(doctor.WorkTime, "Id", "StartTime");
                foreach (WorkTime wt in doctor.WorkTime)
                    ViewData[wt.StartTime.ToString()] = wt.Patient.FIO;

                return RedirectToAction("Edit", doctor);
            }
            ViewBag.doctorId = doctorId;
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteFreeTime(int doctorId, int id)
        {
            Doctor doctor = db.DoctorSet.Find(doctorId);
            FreeTime record = db.FreeTimeSet.Find(id);

            if (ModelState.IsValid)
            {
                doctor.FreeTime.Remove(record);
                db.FreeTimeSet.Remove(record);
                db.SaveChanges();


                ViewBag.FreeTimeList = new SelectList(doctor.FreeTime, "Id", "StartTime");
                ViewBag.WorkTimeList = new SelectList(doctor.WorkTime, "Id", "StartTime");
                foreach (WorkTime wt in doctor.WorkTime)
                    ViewData[wt.StartTime.ToString()] = wt.Patient.FIO;
                return RedirectToAction("Edit", doctor);
            }
            return View(doctor);
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteWorkTime(int doctorId, int id)
        {
            Doctor doctor = db.DoctorSet.Find(doctorId);
            WorkTime record = db.WorkTimeSet.Find(id);

            if (ModelState.IsValid)
            {
                doctor.WorkTime.Remove(record);
                db.WorkTimeSet.Remove(record);
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();


                ViewBag.FreeTimeList = new SelectList(doctor.FreeTime, "Id", "Name");
                ViewBag.WorkTimeList = new SelectList(doctor.WorkTime, "Id", "StartTime");
                foreach (WorkTime wt in doctor.WorkTime)
                    ViewData[wt.StartTime.ToString()] = wt.Patient.FIO;
                return RedirectToAction("Edit", doctor);
            }
            return View(doctor);
        }
        /* public virtual ActionResult Export()
         {
             var tasks = repository.GetAllTasks();

             Response.AddHeader("Content-Disposition", "attachment; filename=Tasks.xls");
             Response.ContentType = "application/ms-excel";

             return PartialView("Export", tasks);
         }*/
    }
}
