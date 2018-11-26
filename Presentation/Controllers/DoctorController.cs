using Domain.Entities;
using Presentation.Models;
using Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace Presentation.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        public async Task<ActionResult> Index()
        {
            HttpClient Client = new HttpClient();
            IEnumerable<DoctorVM> doctors;
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await Client.GetAsync("http://localhost:18080/epione-jee-web/api/Doctor");
            if (response.IsSuccessStatusCode)
            {
                doctors = await response.Content.ReadAsAsync<IEnumerable<DoctorVM>>();
                System.Diagnostics.Debug.WriteLine("doctors :" + doctors.Count());
                ViewData["doctors"] = doctors;
            }
            else
            {
                ViewBag.result = "erreur";
            }
            return View();
        }
        // GET: Doctor/SearchDoctor
        public async Task<ActionResult> SearchDoctor(DoctorVM model)
        {
            HttpClient Client = new HttpClient();
            IEnumerable<DoctorVM> list;
            System.Diagnostics.Debug.WriteLine("******************specialit :" + model.speciality_id);
            System.Diagnostics.Debug.WriteLine("******************lon :" + model.longitude);
            System.Diagnostics.Debug.WriteLine("******************lat :" + model.latitude);
            System.Diagnostics.Debug.WriteLine("******************name :" + model.name);
            string uriString = "http://localhost:18080/epione-jee-web/api/Doctor";
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //search by speciality
            if (model.latitude == null && model.longitude == null && model.name == null && model.speciality_id != 0)
            {
                uriString = uriString +"?idS=" + model.speciality_id;
            }
            //search by name
            else if (model.latitude == null && model.longitude == null && model.name != null && model.speciality_id == 0)
            {
                uriString = uriString+ "?name=" + model.name;
            }
            //search by location 
            else if (model.latitude != null && model.longitude != null && model.name == null && model.speciality_id == 0)
            {
                uriString = uriString +"?lon="+model.longitude+"&lat="+model.latitude;
               
            }
            //search by name and speciality
            else if (model.latitude == null && model.longitude == null && model.name != null && model.speciality_id != 0)
            {
                uriString = uriString + "?idS=" + model.speciality_id + "&name=" + model.name;
                System.Diagnostics.Debug.WriteLine(uriString);
            }
            //search by name and location
            else if (model.latitude != null && model.longitude != null && model.name != null && model.speciality_id == 0)
            {
                uriString = uriString + "?lon=" + model.longitude + "&lat=" + model.latitude+"&name="+model.name;
            }
            //search by location and speciality
            else if (model.latitude != null && model.longitude != null && model.name == null && model.speciality_id != 0)
            {
                uriString = uriString + "?idS=" + model.speciality_id + "&lon=" + model.longitude + "&lat=" + model.latitude + "&name=" + model.name;
            }
            else
            {
                uriString = "http://localhost:18080/epione-jee-web/api/Home/Index";

            }
            // HttpResponseMessage response = await Client.GetAsync("http://localhost:18080/epione-jee-web/api/Doctor?idS=1&lon=&lat=5&name=oumayma");
            HttpResponseMessage response = await Client.GetAsync(uriString);

            System.Diagnostics.Debug.WriteLine("http://localhost:18080/epione-jee-web/api/Doctor?idS=" + model.speciality_id + "&lon=" + model.longitude + "&lat=" + model.latitude + "&name=" + model.name);
            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadAsAsync<IEnumerable<DoctorVM>>();
            System.Diagnostics.Debug.WriteLine("******************DOCTOR LIST LENGTH :" + list.Count());
                ViewData["doctorsList"] = list;
            }
            else
            {
                ViewBag.result = "erreur";
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
            /*if (Session["authtoken"] != null)
            {
                return Redirect("Login", "Auth");
            }*/
            HttpClient client = new HttpClient();
            
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

        // GET: Doctor/Delete/5
        public ActionResult Delete(int id)
        {
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
