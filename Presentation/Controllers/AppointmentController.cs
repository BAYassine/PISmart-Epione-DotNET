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
    public class AppointmentController : Controller
    {
        // GET: Appointment
        public ActionResult Index()
        {
            if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth");
            HttpClient Client2 = new HttpClient();
            Client2.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            Client2.BaseAddress = new Uri("http://localhost:18080");
            Client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client2.GetAsync("epione-jee-web/api/Appointment/SearchAllAppointments").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<AppointmentVM>>().Result;

            }
            else
            {
                ViewBag.result = response.IsSuccessStatusCode;
            }
            return View();
        }

        public ActionResult CalendarRessource(int id)
        {

            AppointmentVM r = new AppointmentVM();
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:18080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("epione-jee-web/api/Appointment?id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                try
                {

                    r = response.Content.ReadAsAsync<AppointmentVM>().Result;

                }
                catch (NullReferenceException) { }
            }
            return View(r);

        }
        // GET: Appointment
        public ActionResult AppDelete()
        {
            if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth");
            HttpClient Client2 = new HttpClient();
            Client2.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            Client2.BaseAddress = new Uri("http://localhost:18080");
            Client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client2.GetAsync("epione-jee-web/api/Appointment/SearchAllAppointments").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<AppointmentVM>>().Result;

            }
            else
            {
                ViewBag.result = response.IsSuccessStatusCode;
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                /* if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth");*/
                HttpClient Client = new HttpClient();
                /*Client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");*/
                Client.BaseAddress = new Uri("http://localhost:18080");
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string ch = "epione-jee-web/api/Appointment/Delete?id=" + id;
                HttpResponseMessage response = Client.DeleteAsync(ch).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.result = response.IsSuccessStatusCode;
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Appointment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        

        // POST: Appointment/Create
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

        // GET: Appointment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Appointment/Edit/5
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

       

        // POST: Appointment/Delete/5
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
