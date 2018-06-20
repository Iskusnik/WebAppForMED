using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebAppForMED.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace WebAppForMED.Controllers
{
    public class PatientCabinetController : Controller
    {
        private ModelMEDContainer db = new ModelMEDContainer();
        // GET: PatientCabinet
        [Authorize(Roles = "patient")]
        public ActionResult Index()
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                            .GetUserManager<ApplicationUserManager>();

            var user = userManager.FindById(User.Identity.GetUserId());
            Patient patient = db.PatientSet.Find(user.PersonId);
            ViewBag.patient = patient;
            return View(patient);
        }

        [Authorize(Roles = "patient")]
        public ActionResult Visits()
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                            .GetUserManager<ApplicationUserManager>();

            var user = userManager.FindById(User.Identity.GetUserId());
            Patient patient = db.PatientSet.Find(user.PersonId);
            ViewBag.Visits = patient.WorkTime;
            return View(patient);
        }

        [Authorize(Roles = "patient")]
        public ActionResult AddVisit()
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                            .GetUserManager<ApplicationUserManager>();

            var user = userManager.FindById(User.Identity.GetUserId());
            Patient patient = db.PatientSet.Find(user.PersonId);
            if (patient == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (patient == null)
            {
                return HttpNotFound();
            }

            ViewBag.PatientId = patient.Id;
            List<Doctor> docList = db.DoctorSet.Where(d => d.FreeTime.Count != 0).ToList();

            if (docList.Count == 0)
            {
                ViewBag.Message = "Нет свободного времени";
                ViewBag.Visits = patient.WorkTime;
                return View("Visits", patient);
            }

            ViewBag.Doctors = new SelectList(docList, "Id", "FIO");

            foreach (Doctor d in docList)
                ViewData[d.FIO] = new SelectList(d.FreeTime, "Id", "StartTime");
            
            ViewBag.Jobs = new SelectList((from Doctor d in docList select d.Job).Distinct().ToList());
            if (db.FreeTimeSet.Count() == 0)
            {
                ViewBag.Message = "Нет свободного времени";
                return RedirectToAction("Index");
            }
            DoctorFreeTimeViewModel model = new DoctorFreeTimeViewModel();
            model.PatientId = (int)patient.Id;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "patient")]
         public ActionResult AddVisit(DoctorFreeTimeViewModel model)
        {
            Patient patient = db.PatientSet.Find(model.PatientId);
            WorkTime record = new WorkTime();
            FreeTime freeTime = db.FreeTimeSet.Find(model.FreeTimeId);
            Doctor doctor = db.DoctorSet.Find(model.DoctorId);
            if (patient != null)
            if (ModelState.IsValid && model.DoctorId != -1 && model.FreeTimeId != -1)
            {
                record.Doctor = doctor;
                record.StartTime = freeTime.StartTime;
                record.Patient = patient;

                patient.WorkTime.Add(record);

                db.FreeTimeSet.Remove(freeTime);

                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Visits", patient);
            }
            ViewBag.Message = "Выберите врача и время";
            return RedirectToAction("Visits", patient);
        }


        public JsonResult GetDoctors(string Id)
        {
            var Doctors = new SelectList(db.DoctorSet.Where(a => a.Job == Id), "Id", "FIO");
            ViewBag.Doctors = Doctors;
            return Json(Doctors, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTime(int Id)
        {
            var allFreeTime = new SelectList(db.FreeTimeSet.Where(a => a.Doctor.Id == Id).ToList(), "Id", "StartTime");
            return Json(allFreeTime, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "patient")]
        public ActionResult Medcard()
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                               .GetUserManager<ApplicationUserManager>();

            var user = userManager.FindById(User.Identity.GetUserId());
            Patient patient = db.PatientSet.Find(user.PersonId);
            ViewBag.Illnesses = patient.MedCard.Illness;
            ViewBag.DocRecords = patient.MedCard.DocRecord;
            return View(patient);
        }
    }
}