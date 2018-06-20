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
    public class DoctorCabinetController : Controller
    {
        private ModelMEDContainer db = new ModelMEDContainer();
        // GET: DoctorCabinet
        [Authorize(Roles = "doctor")]
        public ActionResult Index()
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                            .GetUserManager<ApplicationUserManager>();

            var user = userManager.FindById(User.Identity.GetUserId());
            Doctor doctor = db.DoctorSet.Find(user.PersonId);
            ViewBag.patient = doctor;
            return View(doctor);
        }


        [Authorize(Roles = "doctor")]
        public ActionResult Visits()
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                               .GetUserManager<ApplicationUserManager>();

            var user = userManager.FindById(User.Identity.GetUserId());
            Doctor doctor = db.DoctorSet.Find(user.PersonId);
            ViewBag.patient = doctor;
            ViewBag.Visits = doctor.WorkTime;
            return View();
        }

        [Authorize(Roles = "doctor")]
        public ActionResult AddRecord(int patientId)
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                               .GetUserManager<ApplicationUserManager>();

            var user = userManager.FindById(User.Identity.GetUserId());
            Doctor doctor = db.DoctorSet.Find(user.PersonId);
            ViewBag.doctorId = doctor.Id;
            ViewBag.patientId = patientId;
            return View();
        }

        [Authorize(Roles = "doctor")]
        [HttpPost]
        public ActionResult AddRecord(int patientId, int doctorId, string Diagnos)
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                               .GetUserManager<ApplicationUserManager>();

            var user = userManager.FindById(User.Identity.GetUserId());
            Doctor doctor = db.DoctorSet.Find(user.PersonId);
            Patient patient = db.PatientSet.Find(patientId);
            DocRecord record = new DocRecord();
            record.Diagnos = Diagnos;
            record.RecordDate = DateTime.Today;
            record.Doctor = doctor;
            record.MedCard = patient.MedCard;

            patient.MedCard.DocRecord.Add(record);
            db.SaveChanges();

            return View("Index");
        }
        [Authorize(Roles = "doctor")]
        public ActionResult Records()
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                               .GetUserManager<ApplicationUserManager>();

            var user = userManager.FindById(User.Identity.GetUserId());
            Doctor doctor = db.DoctorSet.Find(user.PersonId);
            ViewBag.doctor = doctor;
            ViewBag.Patients = db.PatientSet.ToList();
            return View();
        }
        [Authorize(Roles = "doctor")]
        public ActionResult ListRecords()
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                                  .GetUserManager<ApplicationUserManager>();

            var user = userManager.FindById(User.Identity.GetUserId());
            Doctor doctor = db.DoctorSet.Find(user.PersonId);
            ViewBag.doctor = doctor;
            return View(doctor.DocRecord);
        }
    }
}