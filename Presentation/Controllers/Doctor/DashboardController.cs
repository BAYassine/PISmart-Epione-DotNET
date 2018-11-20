using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class DashboardController : Controller
    {
        static HttpClient client = new HttpClient();
        // Author : Yassine
        public async Task<ActionResult> Index()
        {
            System.Diagnostics.Debug.WriteLine(HttpContext.Request.Url.AbsolutePath);
            if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth", new { returnUrl = HttpContext.Request.Url.AbsolutePath });
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            String response = await client.GetStringAsync("http://localhost:18080/epione-jee-web/api/dashboard");
            JObject dash_data = JObject.Parse(response);
            ViewData["dash_data"] = dash_data;
            return View();
        }
    }
}