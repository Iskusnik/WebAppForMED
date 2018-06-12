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
    public class PatientsController : Controller
    {
        private ModelMEDContainer db = new ModelMEDContainer();

        // GET: Patients
        public ActionResult Index()
        {
            return View(db.PatientSet.ToList());
        }

        // GET: Patients/Details/5
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
            ViewBag.IllnessList = new SelectList(patient.MedCard.Illness, "Id", "Name");

            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            return View(patient);
        }

        // POST: Patients/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.PatientSet.Find(id);
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



        public PartialViewResult ListView()
        {
            SelectList patients = new SelectList(db.PatientSet, "Id", "FIO");
            ViewBag.Patients = patients;
            return PartialView();
        }




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
                return View("Details", patient);
            }

            ViewBag.IllnessSet = new SelectList(list, "Id", "Name");
            return View(patient);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                return RedirectToAction("Details", patient);
            }
            return View(patient);
        }


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
                return RedirectToAction("Details", patient);
            }
            return View(patient);
        }

    }
}
