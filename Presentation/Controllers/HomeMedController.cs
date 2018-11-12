using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class HomeMedController : Controller
    {
        // GET: HomeMed
        public ActionResult Index()
        {
            return View();
        }
    }
}