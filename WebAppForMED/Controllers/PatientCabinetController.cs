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
        public ActionResult Index()
        {
            return View();
        }
    }
}