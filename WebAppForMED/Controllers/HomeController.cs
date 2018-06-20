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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                               .GetUserManager<ApplicationUserManager>();

            var user = userManager.FindById(User.Identity.GetUserId());
            if (user == null)
                return View();

            string[] roles = userManager.GetRoles(user.Id).ToArray();
            if (roles.Count() == 0)
                return View();

            if (roles.Contains("admin"))
                return View("Admin");

            if (roles[0] == "doctor")
                return RedirectToAction("Index","DoctorCabinet");

            if (roles[0] == "patient")
                return RedirectToAction("Index", "PatientCabinet");
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Admin()
        {
            return View();
        }

    
    }
}