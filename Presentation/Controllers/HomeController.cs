
using Presentation.Models;
using Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        ISpecialityService serv = new SpecialityService();
        public ActionResult Index()
        {
       
            var specialites = serv.GetMany();
     
            ViewBag.specialites = new SelectList(specialites, "id", "name");


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}