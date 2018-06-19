using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppForMED.Controllers
{
    public class PatientCabinetController : Controller
    {
        // GET: PatientCabinet
        [Authorize(Roles = "patient")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "patient")]
        public ActionResult Visits()
        {
            return View();
        }

        [Authorize(Roles = "patient")]
        public ActionResult Medcard()
        {
            return View();
        }
    }
}