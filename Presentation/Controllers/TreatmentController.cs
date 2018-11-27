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
    public class TreatmentController : Controller
    {
        // GET: Treatment
        public ActionResult Index()
        {
            if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth");
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            Client.BaseAddress = new Uri("http://localhost:18080");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("epione-jee-web/api/treatments").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<ICollection<TreatmentVM>>().Result;

            }
            else
            {
                ViewBag.result = response.IsSuccessStatusCode;
            }
            return View();
        }

        // GET: Treatment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Treatment/Create
        public ActionResult Create()
        {
            if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth");
            return View();
        }

        // POST: Treatment/Create
        [HttpPost]
        public ActionResult Create(TreatmentVM treat)
        {

            try
            {

           
                if (Session["authtoken"] == null)
                    return RedirectToAction("Login", "Auth");

                HttpClient Client = new HttpClient();
                Client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
                Client.BaseAddress = new Uri("http://localhost:18080");


                Client.PostAsJsonAsync<TreatmentVM>("epione-jee-web/api/paths", treat).ContinueWith((PostTask) => PostTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Treatment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Treatment/Edit/5
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

        // GET: Treatment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Treatment/Delete/5
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

    }
}
