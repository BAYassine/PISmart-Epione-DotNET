

using iTextSharp.text;
using iTextSharp.text.pdf;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
           /* if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth");*/
            HttpClient Client = new HttpClient();
           /* Client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");*/
            Client.BaseAddress = new Uri("http://localhost:18080");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("epione-jee-web/api/reports").Result;

            if (response.IsSuccessStatusCode)
            {

                ViewBag.result = response.Content.ReadAsAsync<ICollection<Object>>().Result;

            }
            else
            {
                ViewBag.result = response.IsSuccessStatusCode;
            }
            return View();
        }

        // GET: Report/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Report/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Report/Create
        [HttpPost]
        public ActionResult Create(ReportVM report)
        {
            try
            {
               /* if (Session["authtoken"] == null)
                    return RedirectToAction("Login", "Auth");
                    */
                HttpClient Client = new HttpClient();
               /* Client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");*/
                Client.BaseAddress = new Uri("http://localhost:18080");
                report.date_rep = report.date_rep.Replace("/", "-");

                Client.PostAsJsonAsync<ReportVM>("epione-jee-web/api/reports", report).ContinueWith((PostTask) => PostTask.Result.EnsureSuccessStatusCode());
                //return new JsonResult { Data = report, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return RedirectToAction("Index");
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Report/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Report/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Report/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Report/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Report/Generate/5
      
    }
}
