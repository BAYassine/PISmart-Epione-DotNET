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
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }
      

        // GET: Patient/Details/5
        public ActionResult Details(int id)
        {
        //    HttpClient Client = new HttpClient();
        //    IEnumerable<PatientVM> list;
        //    System.Diagnostics.Debug.WriteLine("******************specialit :" + model.speciality_id);
           

        //    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    HttpResponseMessage response = await Client.GetAsync("http://localhost:18080/epione-jee-web/api/Doctor?idS=" + model.speciality_id + "&lon=" + model.longitude + "&lat=" + model.latitude + "&name=" + model.name);
        //    // HttpResponseMessage response = await Client.GetAsync("http://localhost:18080/epione-jee-web/api/Doctor?idS=1&lon=5&lat=5&name=oumayma");
        //    System.Diagnostics.Debug.WriteLine("http://localhost:18080/epione-jee-web/api/Doctor?idS=" + model.speciality_id + "&lon=" + model.longitude + "&lat=" + model.latitude + "&name=" + model.name);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        list = await response.Content.ReadAsAsync<IEnumerable<PatientVM>>();
        //        System.Diagnostics.Debug.WriteLine("******************DOCTOR LIST LENGTH :" + list.Count());
        //        ViewData["appoi"] = list;
        //    }
        //    else
        //    {
        //        ViewBag.result = "erreur";
        //    }
            return View();
        }

        // GET: Patient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Patient/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Patient/Edit/5
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

        // GET: Patient/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Patient/Delete/5
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

        public new ActionResult Profile()
        {
            return View();
        }
    }

}
