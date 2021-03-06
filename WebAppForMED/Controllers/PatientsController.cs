﻿using System;
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
    public class PatientsController : Controller
    {
        private ModelMEDContainer db = new ModelMEDContainer();
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

        // GET: Patients
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.PatientSet.ToList());
        }

        // GET: Patients/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.PatientSet.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }

            

            return View(patient);
        }

        // GET: Patients/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "Id,FIO,Nation,BirthDate,BirthPlace,LivePlace,Pol,OMS,Blood,DocType,DocNum")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.PatientSet.Add(patient);
                patient.MedCard = new MedCard();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.PatientSet.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.IllnessList = new SelectList(patient.MedCard.Illness, "Id", "Name");
            ViewBag.RecordsList = new SelectList(patient.MedCard.DocRecord, "Id", "Diagnos");

            SelectList list = new SelectList(patient.WorkTime, "Id", "StartTime");
            ViewBag.VisitsList = list;
            foreach (var item in patient.WorkTime)
            {
                ViewData[item.StartTime.ToString()] = item.Doctor.FIO;
                ViewData[item.Doctor.FIO] = item.Doctor.Job;
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,FIO,Nation,BirthDate,BirthPlace,LivePlace,Pol,OMS,Blood,DocType,DocNum")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.PatientSet.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.PatientSet.Find(id);
            if (patient.MedCard != null)
            {
                db.DocRecordSet.RemoveRange(patient.MedCard.DocRecord);
                patient.MedCard.Illness.Clear();
                db.MedCardSet.Remove(patient.MedCard);
            }
            db.PatientSet.Remove(patient);
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
            SelectList patients = new SelectList(db.PatientSet, "Id", "FIO");
            ViewBag.Patients = patients;
            return PartialView();
        }




        [Authorize(Roles = "admin")]
        public ActionResult AddIllness(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.PatientSet.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.patientId = id;

            //var Illnesses = (from ill in db.IllnessSet where !patient.MedCard.Illness.Contains(ill) select ill).ToList();
            //if (Illnesses.Count() == 0)
            //    return View("Details");

            //(from ill in db.IllnessSet where !patient.MedCard.Illness.Contains(ill) select ill).ToList()
            List<Illness> list = db.IllnessSet.ToList();
            List<Illness> patientList = patient.MedCard.Illness.ToList();

            for (int i = 0; i < list.Count; i++)
                for (int j = 0; j < patient.MedCard.Illness.Count; j++)
                    if (list[i].Name == patientList[j].Name)
                        list.RemoveAt(i);

            if (list.Count == 0)
            {
                ViewBag.Message = "Нечего добавить";
                ViewBag.IllnessList = new SelectList(patient.MedCard.Illness, "Id", "Name");
                ViewBag.RecordsList = new SelectList(patient.MedCard.DocRecord, "Id", "Diagnos");
                
                SelectList listT = new SelectList(patient.WorkTime, "Id", "StartTime");
                ViewBag.VisitsList = listT;
                foreach (var item in patient.WorkTime)
                {
                    ViewData[item.StartTime.ToString()] = item.Doctor.FIO;
                    ViewData[item.Doctor.FIO] = item.Doctor.Job;
                }
                return View("Edit", patient);
            }

            ViewBag.IllnessSet = new SelectList(list, "Id", "Name");
            return View(patient);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult AddIllness(int patientId, int id)
        {
            Patient patient = db.PatientSet.Find(patientId);
            Illness illness = db.IllnessSet.Find(id);
            
            if (ModelState.IsValid)
            {
                patient.MedCard.Illness.Add(illness);
                illness.MedCard.Add(patient.MedCard);

                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", patient);
            }
            return View(patient);
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteIllness(int patientId, int id)
        {
            Patient patient = db.PatientSet.Find(patientId);
            Illness illness = db.IllnessSet.Find(id);

            if (ModelState.IsValid)
            {
                patient.MedCard.Illness.Remove(illness);
                illness.MedCard.Remove(patient.MedCard);

                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", patient);
            }
            return View(patient);
        }




        [Authorize(Roles = "admin")]
        public ActionResult AddRecord(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.PatientSet.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.patientId = id;
            ViewBag.DoctorList = new SelectList(db.DoctorSet, "Id", "FIO");

            if (db.DoctorSet.Count() == 0)
            {
                ViewBag.Message = "Нет врачей, чтобы сделать запись";
                ViewBag.IllnessList = new SelectList(patient.MedCard.Illness, "Id", "Name");
                ViewBag.RecordsList = new SelectList(patient.MedCard.DocRecord, "Id", "Diagnos");
                ViewBag.VisitsList = new SelectList(patient.WorkTime, "Id", "StartTime");


                SelectList list = new SelectList(patient.WorkTime, "Id", "StartTime");
                ViewBag.VisitsList = list;
                foreach (var item in patient.WorkTime)
                {
                    ViewData[item.StartTime.ToString()] = item.Doctor.FIO;
                    ViewData[item.Doctor.FIO] = item.Doctor.Job;
                }
                return View("Edit", patient);
            }


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult AddRecord(int patientId, int doctorId, [Bind(Include = "Id, Diagnos, RecordDate")]DocRecord record)
        {
            Patient patient = db.PatientSet.Find(patientId);
            Doctor doctor = db.DoctorSet.Find(doctorId);

            record.Doctor = doctor;

            if (ModelState.IsValid)
            {
                patient.MedCard.DocRecord.Add(record);

                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", patient);
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteRecord(int patientId, int id)
        {
            Patient patient = db.PatientSet.Find(patientId);
            DocRecord record = db.DocRecordSet.Find(id);

            if (ModelState.IsValid)
            {
                db.DocRecordSet.Remove(record);

                db.SaveChanges();
                return RedirectToAction("Edit", patient);
            }
            return View(patient);
        }



        

        [Authorize(Roles = "admin")]
        public ActionResult DeleteVisit(int patientId, int id)
        {
            Patient patient = db.PatientSet.Find(patientId);
            WorkTime record = db.WorkTimeSet.Find(id);

            if (ModelState.IsValid)
            {
                patient.WorkTime.Remove(record);
                db.WorkTimeSet.Remove(record);
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", patient);
            }
            return View(patient);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AddVisit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.PatientSet.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }

            ViewBag.PatientId = id;

            foreach (Doctor d in db.DoctorSet)
                ViewData[d.FIO] = new SelectList(d.FreeTime, "Id", "StartTime");
           
            List<Doctor> docList = db.DoctorSet.Where(d => d.FreeTime.Count != 0).ToList();

            ViewBag.Doctors = new SelectList(docList, "Id", "FIO");
            ViewBag.Jobs = new SelectList((from Doctor d in docList select d.Job).Distinct().ToList());

            if (db.FreeTimeSet.Count() == 0)
            {
                ViewBag.Message = "Нет свободного времени";
                ViewBag.IllnessList = new SelectList(patient.MedCard.Illness, "Id", "Name");
                ViewBag.RecordsList = new SelectList(patient.MedCard.DocRecord, "Id", "Diagnos");

                SelectList list = new SelectList(patient.WorkTime, "Id", "StartTime");
                ViewBag.VisitsList = list;
                foreach (var item in patient.WorkTime)
                {
                    ViewData[item.StartTime.ToString()] = item.Doctor.FIO;
                    ViewData[item.Doctor.FIO] = item.Doctor.Job;
                }
                return View("Edit", patient);
            }
            DoctorFreeTimeViewModel model = new DoctorFreeTimeViewModel();
            model.PatientId = (int)id;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
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
                return RedirectToAction("Edit", patient);
            }
            ViewBag.Message = "Выберите врача и время";
            return RedirectToAction("Edit", patient);
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

        
    }
}
