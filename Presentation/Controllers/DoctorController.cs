using Domain.Entities;

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
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index()
        {
            HttpClient Client1 = new HttpClient();
            Client1.BaseAddress = new Uri("http://localhost:18080");
            Client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client1.GetAsync("epione-jee-web/api/Doctor").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<DoctorVM>>().Result;

            }
            else
            {
                ViewBag.result = response.IsSuccessStatusCode;
            }
            return View();
        }


        // GET: Doctor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Doctor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctor/Create
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

        // GET: Doctor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Doctor/Edit/5
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



        public JsonResult GetEvents()
        {
            
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            client.BaseAddress = new Uri("http://localhost:18080");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("epione-jee-web/api/Appointment/SearchAllAppointments").Result;
            var events = response.Content.ReadAsAsync<IEnumerable<AppointmentVM>>().Result;
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



        [HttpPost]
        public JsonResult SaveEvent(AppointmentVM l)
        {


            //int id = ressourceId;
            var status = false;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080");
            client.PostAsJsonAsync<AppointmentVM>("epione-jee-web/api/Appointment/addAPPhh", l).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            //client.PostAsJsonAsync<Appointment>("epione-jee-web/api/Appointment/addAPPhh", l).ContinueWith((p => p.Result.EnsureSuccessStatusCode()));
            status = true;


            return new JsonResult { Data = new { status = status } };
        }

       


        public ActionResult AppointementDoctor()
        {


            return View();
        }



        // GET: Doctor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        public ActionResult UpdateProfil()
        {
            HttpClient Client1 = new HttpClient();
            Client1.BaseAddress = new Uri("http://localhost:18080");
            Client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client1.GetAsync("epione-jee-web/api/Doctor").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<DoctorVM>>().Result;

            }
            else
            {
                ViewBag.result = response.IsSuccessStatusCode;
            }
            return View();
        }
        // POST: Doctor/Delete/5
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
