using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using OfficeOpenXml;
using Presentation.Models;
using Service;
using System.Net.Http.Headers;

namespace Presentation.Controllers
{
    public class DoctorController : Controller
    {
        static HttpClient client = new HttpClient();

        private static IDoctorService userService = new DoctorService();

        // Author : Yassine
        public async Task<ActionResult> Dashboard()
        {
            if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth", new {returnUrl = HttpContext.Request.Url.AbsolutePath});
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            String response = await client.GetStringAsync("http://localhost:18080/epione-jee-web/api/dashboard");
            JObject dash_data = JObject.Parse(response);
            ViewData["dash_data"] = dash_data;
            return View();
        }

        public async Task<ActionResult> UpdateAppointment(int id, bool state)
        {
            if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth", new {returnUrl = HttpContext.Request.Url.AbsolutePath});
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            await client.PutAsync(
                "http://localhost:18080/epione-jee-web/api/Appointment/changestate/" + id + "?action=" + state, null);
            return RedirectToAction("Dashboard");
        }

        // GET: Account
        public ActionResult Account()
        {
            if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth", new {returnUrl = HttpContext.Request.Url.AbsolutePath});
            Domain.Entities.Doctor doctor = (Domain.Entities.Doctor) Session["user"];
            AccountVM accountVM = new AccountVM
            {
                username = doctor.username,
                email = doctor.email
            };
            return View(accountVM);
        }

        // POST: AccountVMs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Account([Bind(Include = "email,username,password,currentPassword")]
            AccountVM accountVM)
        {
            if (ModelState.IsValid)
            {
                Doctor u = userService.GetById(((User) Session["user"]).Id);
                if (BCrypt.Net.BCrypt.Verify(accountVM.currentPassword, u.password))
                {
                    u.email = accountVM.email;
                    u.username = accountVM.username;
                    Debug.WriteLine("password : '" + accountVM.password + "'");
                    if (accountVM.password != null)
                        u.password = BCrypt.Net.BCrypt.HashPassword(accountVM.password);
                    userService.Update(u);
                    userService.Commit();
                    Session["user"] = u;
                    return RedirectToAction("Account");
                }
                else
                {
                    ViewBag.error = "You entered a wrong password";
                }
            }

            return View(accountVM);
        }

        public void WriteTsv<T>(IEnumerable<T> data, TextWriter output)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Write(prop.DisplayName); // header
                output.Write("\t");
            }

            output.WriteLine();
            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Write(prop.Converter.ConvertToString(
                        prop.GetValue(item)));
                    output.Write("\t");
                }

                output.WriteLine();
            }
        }


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
                uriString = uriString + "?idS=" + model.speciality_id;
            }
            //search by name
            else if (model.latitude == null && model.longitude == null && model.name != null &&
                     model.speciality_id == 0)
            {
                uriString = uriString + "?name=" + model.name;
            }
            //search by location 
            else if (model.latitude != null && model.longitude != null && model.name == null &&
                     model.speciality_id == 0)
            {
                uriString = uriString + "?lon=" + model.longitude + "&lat=" + model.latitude;
            }
            //search by name and speciality
            else if (model.latitude == null && model.longitude == null && model.name != null &&
                     model.speciality_id != 0)
            {
                uriString = uriString + "?idS=" + model.speciality_id + "&name=" + model.name;
                System.Diagnostics.Debug.WriteLine(uriString);
            }
            //search by name and location
            else if (model.latitude != null && model.longitude != null && model.name != null &&
                     model.speciality_id == 0)
            {
                uriString = uriString + "?lon=" + model.longitude + "&lat=" + model.latitude + "&name=" + model.name;
            }
            //search by location and speciality
            else if (model.latitude != null && model.longitude != null && model.name == null &&
                     model.speciality_id != 0)
            {
                uriString = uriString + "?idS=" + model.speciality_id + "&lon=" + model.longitude + "&lat=" +
                            model.latitude + "&name=" + model.name;
            }
            else
            {
                uriString = "http://localhost:18080/epione-jee-web/api/Home/Index";
            }

            // HttpResponseMessage response = await Client.GetAsync("http://localhost:18080/epione-jee-web/api/Doctor?idS=1&lon=&lat=5&name=oumayma");
            HttpResponseMessage response = await Client.GetAsync(uriString);

            System.Diagnostics.Debug.WriteLine("http://localhost:18080/epione-jee-web/api/Doctor?idS=" +
                                               model.speciality_id + "&lon=" + model.longitude + "&lat=" +
                                               model.latitude + "&name=" + model.name);
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

        //// GET: Doctor/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Doctor/Create
        //public ActionResult Create()
        //{
        //    /*if (Session["authtoken"] != null)
        //    {
        //        return Redirect("Login", "Auth");
        //    }*/
        //    HttpClient client = new HttpClient();

        //    return View();
        //}

        //// POST: Doctor/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Doctor/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Doctor/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Doctor/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Doctor/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Doctor
        public ActionResult IndexFB()
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


        public JsonResult GetEvents()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            client.BaseAddress = new Uri("http://localhost:18080");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response =
                client.GetAsync("epione-jee-web/api/Appointment/SearchAllAppointments").Result;
            var events = response.Content.ReadAsAsync<IEnumerable<AppointmentVM>>().Result;
            return new JsonResult {Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        // GET: Doctor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        [HttpPost]
        public JsonResult SaveEvent(AppointmentVM l)
        {
            //int id = ressourceId;
            var status = false;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080");
            client.PostAsJsonAsync<AppointmentVM>("epione-jee-web/api/Appointment/addAPPhh", l)
                .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            //client.PostAsJsonAsync<Appointment>("epione-jee-web/api/Appointment/addAPPhh", l).ContinueWith((p => p.Result.EnsureSuccessStatusCode()));
            status = true;
            return new JsonResult {Data = new {status = status}};
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
    }
}