using Domain.Entities;
using Microsoft.AspNet.Identity;
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
    public class ConsultationController : Controller
    {
        // GET: Consultation
        public ActionResult Index()
        {
             if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth");
            HttpClient Client2 = new HttpClient();
            Client2.BaseAddress = new Uri("http://localhost:18080");
            Client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client2.GetAsync("epione-jee-web/api/Consultation/SearchAllConsultations").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<ConsultationVM>>().Result;

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
                string ch = "epione-jee-web/api/Consultation/Delete?idC=" + id;
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

        // GET: Consultation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Consultation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Consultation/Create
        [HttpPost]
        public ActionResult Create(ConsultationVM consultation)
        {
            if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080");
            client.PostAsJsonAsync<ConsultationVM>("epione-jee-web/api/Consultation/ConsultationAdding", consultation).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
               

               
                return RedirectToAction("Index");
           
              
            
        }

        // GET: Consultation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Consultation/Edit/5
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

       

        // POST: Consultation/Delete/5
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

        // GET: Consultation/ConsultationByDoc
       
        public ActionResult ConsultationByDoc()
        {
            if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth");
            HttpClient Client3 = new HttpClient();
            Client3.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            Client3.BaseAddress = new Uri("http://localhost:18080");
            Client3.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client3.GetAsync("epione-jee-web/api/Consultation/SearchForConsultationsByDoctor?id=1").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<ConsultationVM>>().Result;

            }
            else
            {
                ViewBag.result = response.IsSuccessStatusCode;
            }
            return View();
        }

        // GET: Consultation/Delete
        public ActionResult ConsultDelete()
        {
            HttpClient Client2 = new HttpClient();
            Client2.BaseAddress = new Uri("http://localhost:18080");
            Client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client2.GetAsync("epione-jee-web/api/Consultation/SearchAllConsultations").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<ConsultationVM>>().Result;

            }
            else
            {
                ViewBag.result = response.IsSuccessStatusCode;
            }
            return View();
        }

        // GET: Consultation/Display
        public ActionResult DisplayConsultation(int id)
        {
            HttpClient Client1 = new HttpClient();
            Client1.BaseAddress = new Uri("http://localhost:18080");
            Client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client1.GetAsync("epione-jee-web/api/Consultation/getConsultationById?id="+id).Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<ConsultationVM>>().Result;

            }
            else
            {
                ViewBag.result = response.IsSuccessStatusCode;
            }
            return View();
        }

    }
}
