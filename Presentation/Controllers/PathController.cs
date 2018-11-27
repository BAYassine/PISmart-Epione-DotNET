using Domain.Entities;
using Newtonsoft.Json.Linq;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Presentation.Controllers
{
    public class PathController : Controller
    {
        int idp;
        int pathIdForChooseTreat;
        // GET: Path
        public ActionResult Index()
        {
            if (Session["authtoken"] == null)
            return RedirectToAction("Login", "Auth");
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            Client.BaseAddress = new Uri("http://localhost:18080");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("epione-jee-web/api/paths").Result;
           
            if (response.IsSuccessStatusCode)
            {
                
                ViewBag.result =  response.Content.ReadAsAsync<ICollection<PathVM>>().Result;

            }
            else
            {
                ViewBag.result = response.IsSuccessStatusCode;
            }
            return View();
        }

        // GET: Path
        public ActionResult findAllPatient()
        {
            HttpClient Client2 = new HttpClient();
            Client2.BaseAddress = new Uri("http://localhost:18080");
            Client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client2.GetAsync("epione-jee-web/api/paths/getAllPatient").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<PatientVM>>().Result;

            }
            else
            {
                ViewBag.result = response.IsSuccessStatusCode;
            }
            return View();
        }

        // GET: Path/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Path/Create
        public ActionResult Create()
        {
            HttpClient Client2 = new HttpClient();
            Client2.BaseAddress = new Uri("http://localhost:18080");
            Client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client2.GetAsync("epione-jee-web/api/profiles").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<ProfileVM>>().Result;

            }
            else
            {
                ViewBag.result = response.IsSuccessStatusCode;
            }
            return View();
        }

        // POST: Path/Create
        [HttpPost]
        public ActionResult Create(PathVM evm)
        {
                          
            try
            {

                idp = Convert.ToInt32(HttpContext.Request.Params.Get("idSelected"));
                evm.patient = new PatientVM();
                evm.patient.id = getUserId(idp);
                if (Session["authtoken"] == null)
                    return RedirectToAction("Login", "Auth");

                HttpClient Client = new HttpClient();
                Client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
                Client.BaseAddress = new Uri("http://localhost:18080");
               evm.date_path = evm.date_path.Replace("/", "-");

                Client.PostAsJsonAsync<PathVM>("epione-jee-web/api/paths", evm).ContinueWith((PostTask) => PostTask.Result.EnsureSuccessStatusCode());
                //return new JsonResult { Data = evm, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Path/Edit/5
        public ActionResult Edit(int id, String description)
        {
            String js = HttpContext.Request.Params.Get("js");
            if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth");
            ViewBag.Message = description;
            ViewBag.PathId = id;
          
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            Client.BaseAddress = new Uri("http://localhost:18080");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("epione-jee-web/api/treatments/getTreatmentPath?idPath="+id).Result;

            if (response.IsSuccessStatusCode)
            {

                ViewBag.result = response.Content.ReadAsAsync<ICollection<TreatmentVM>>().Result;
                ViewBag.js = js;
            }
            else
            {
                ViewBag.result = response.IsSuccessStatusCode;
            }
            return View();
        }

        // POST: Path/Edit/5
        [HttpPut]
        public ActionResult Edit(PathVM path)
        {

            try
            {

             
                if (Session["authtoken"] == null)
                    return RedirectToAction("Login", "Auth");

                HttpClient Client = new HttpClient();
                Client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
                Client.BaseAddress = new Uri("http://localhost:18080");


                Client.PostAsJsonAsync<PathVM>("epione-jee-web/api/paths", path).ContinueWith((PostTask) => PostTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Path/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Path/Delete/5
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
        public int getUserId(int id)
        {
           
            HttpClient Client = new HttpClient();
           
            Client.BaseAddress = new Uri("http://localhost:18080");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("epione-jee-web/api/profiles/getUser?id="+id).Result;
            if (response.IsSuccessStatusCode)
            {
                id = response.Content.ReadAsAsync<int>().Result;

            }
           
            return id;
        }

        [HttpPost]
        public ActionResult AddTreatment(TreatmentVM treat, int iddd)
        {
            pathIdForChooseTreat = iddd;
            try
            {

                //idp = Convert.ToInt32(HttpContext.Request.Params.Get("idSelected"));
                // evm.patient = new PatientVM();
                // evm.patient.id = getUserId(idp);
                // treat.path = new PathVM();
                // treat.path.id = idp;
                ViewBag.testest = iddd;
                if (Session["authtoken"] == null)
                    return RedirectToAction("Login", "Auth");

                HttpClient Client = new HttpClient();
                Client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
                Client.BaseAddress = new Uri("http://localhost:18080");


                Client.PostAsJsonAsync<TreatmentVM>("epione-jee-web/api/paths/addTreatment?idPath="+iddd, treat).ContinueWith((PostTask) => PostTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            //return new JsonResult { Data = treat,JsonRequestBehavior=JsonRequestBehavior.AllowGet};
        }

        /* public ActionResult Index()
        {
            if (Session["authtoken"] == null)
            return RedirectToAction("Login", "Auth");
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            Client.BaseAddress = new Uri("http://localhost:18080");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("epione-jee-web/api/paths").Result;
           
            if (response.IsSuccessStatusCode)
            {
                
                ViewBag.result =  response.Content.ReadAsAsync<ICollection<PathVM>>().Result;

            }
            else
            {
                ViewBag.result = response.IsSuccessStatusCode;
            }
            return View();
        }*/

        public ActionResult AddTreatment()
        {
          
          
                return View();
            
        }

        public ActionResult chooseTreat()
        {
            
            ViewBag.PathId = 1;
            HttpClient Client2 = new HttpClient();
            Client2.BaseAddress = new Uri("http://localhost:18080");
            Client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client2.GetAsync("epione-jee-web/api/treatments").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<TreatmentVM>>().Result;

            }
            else
            {
                ViewBag.result = response.IsSuccessStatusCode;
            }
            return View();
        }
        [HttpPost]
        public ActionResult chooseTreat(TreatmentVM treat )
        {
            string str1;
            string str = Request.Form["test"];
            str1 = str.Substring(0, 20);
            str = str.Replace(str1, "");
            str = str.Remove(str.Length - 2);
            JavaScriptSerializer j = new JavaScriptSerializer();
           object a = j.Deserialize(str, typeof(object));
           // return new JsonResult { Data = a, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
           /* if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth");*/

            HttpClient Client = new HttpClient();
          /*  Client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");*/
            Client.BaseAddress = new Uri("http://localhost:18080");


            Client.PostAsJsonAsync<object>("epione-jee-web/api/treatments/copyListTreatment?idPath="+ pathIdForChooseTreat, a).ContinueWith((PostTask) => PostTask.Result.EnsureSuccessStatusCode());

            return RedirectToAction("Index");
        }











    }
}
