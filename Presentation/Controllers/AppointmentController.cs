using Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Presentation.Models;
using Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Presentation.Controllers
{
    public class AppointmentController : Controller
    {
        IAppointmentService serv = new AppointmentService();
        int i = 8;

        // GET: Appointment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
       public ActionResult PreviousAppointment()
        {
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:18080/epione-jee-web/api/Appointment").Result;
            var result = response.Content.ReadAsAsync<IEnumerable<AppointmentVM>>().Result;
            DateTime today = DateTime.Today;
            List<AppointmentVM> l1 = new List<AppointmentVM>();
            foreach(AppointmentVM a in result){
                DateTime ds = Convert.ToDateTime(a.date_start);
                if (DateTime.Compare(ds,today) < 0)
                {
                    l1.Add(a);
                }
            }
            
                ViewBag.previousAppointment = l1;
                return View();
        }
        public ActionResult UpcomingAppointment()
        {
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:18080/epione-jee-web/api/Appointment").Result;
            var result = response.Content.ReadAsAsync<IEnumerable<AppointmentVM>>().Result;
            DateTime today = DateTime.Today;
            ViewBag.date = today;
            List<AppointmentVM> l2 = new List<AppointmentVM>();
            foreach (AppointmentVM a in result)
            {
                DateTime ds = Convert.ToDateTime(a.date_start);
                if (DateTime.Compare(ds, today) > 0)
                {
                    l2.Add(a);
                }
            }

            ViewBag.upcomingAppointments = l2;
            return View();
        }
        // GET: Appointment/Patient
        public async Task<ActionResult> AppointmentByPatient()
        {
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            Client.DefaultRequestHeaders. Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:18080/epione-jee-web/api/Appointment").Result;
            var result =response.Content.ReadAsAsync<IEnumerable<AppointmentVM>>().Result;
            ViewBag.appointment = result;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CancelAppointment(int id)
        {
            HttpClient Client = new HttpClient();
            System.Diagnostics.Debug.WriteLine("********* canceled**********");

            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Put,"http://localhost:18080/epione-jee-web/api/Appointment?idA="+id);
            request.Content = new StringContent("{}",  Encoding.UTF8, "application/json");
            var response = await Client.SendAsync(request);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            System.Diagnostics.Debug.WriteLine("******************okkk:" + request.Content);
            int ca = 0;
            var canceledAp = serv.GetMany(c => c.patient.Id == i);
            foreach(Appointment app in canceledAp)
            {
                if (app.state==0){ ca++;}
            }
            if(ca>=2)
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress("oumayma.habouri@gmail.com")); //replace with valid value
                message.Subject = "Canceled Appointments";
                message.Body = string.Format("You have canceled more than 2 appointment please answer this form ...");
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("AppointmentByPatient");
                }
            }
            if (response.IsSuccessStatusCode)
            {
              
                System.Diagnostics.Debug.WriteLine("******************okkk:" +response.StatusCode);
                ViewData["canceled"] = "ok";
            }
            else
            {
                ViewData["canceled"] = "erreur";
            }
            return RedirectToAction("AppointmentByPatient", new { c = ViewBag.canceledApp });
        }
        // GET: Appointment/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(AppointmentVM Model)
        {

            HttpClient Client = new HttpClient();

            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            //User currentUser = (User)Session["authtoken"];
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));   
            AppointmentVM a = new AppointmentVM();
            string date = String.Concat(Model.date_start, ":00");
            a.date_start = date;
            a.state = "1";
            a.doctor = new Doctor();
            System.Diagnostics.Debug.WriteLine("********* Daaaaaate******* " + a.date_start);
            a.doctor.id = Model.doctor_id;
            a.patient = new Patient();
            a.patient.Id = 8;
            a.patient_id = 8;
            a.message = Model.message;
            a.reason = new Reason() { id = Model.reason_id };
            a.reason_id = Model.reason_id;
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:18080/epione-jee-web/api/Appointment");
            var json = JsonConvert.SerializeObject(a);

            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.SendAsync(request);
            System.Diagnostics.Debug.WriteLine("********* Reasonnnn******* " + a.reason_id);
            System.Diagnostics.Debug.WriteLine("********* Messagggee******* " + a.message);
            System.Diagnostics.Debug.WriteLine("********* docotor iiid******* " + a.doctor_id);

            if (response.IsSuccessStatusCode)
            {
               
                ViewBag.success = "Added with success !";
              
                var message = new MailMessage();
                message.To.Add(new MailAddress("oumayma.habouri@gmail.com")); //replace with valid value
                message.Subject = "Appointment confirmation";
                message.Body = string.Format("Your appointment on "+Model.date_start+" has been confirmed");
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("AppointmentByPatient");
                }

            }
            else
            {
                ViewBag.result = "Not added ! ";
                return View();
            }


           
        }

        // GET: Appointment/Reasons
        [HttpGet]
        public ActionResult Create(int doctor_id,string name)
        {
            HttpClient Client = new HttpClient();

            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:18080/epione-jee-web/api/Reason/SearchReasonsByDoctor?idDoctor="+ doctor_id).Result;
            var result = response.Content.ReadAsAsync<List<Reason>>().Result;
            ViewBag.doctorname = name;
            ViewBag.doctorid = doctor_id;
            ViewData["reason_id"] = new SelectList(result, "id", "name");
           System.Diagnostics.Debug.WriteLine("********* docotor name******* " + name);
            
            return View();
        }

        // GET: Appointment/Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            HttpClient Client = new HttpClient();

            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = Client.GetAsync("http://localhost:18080/epione-jee-web/api/Appointment?id="+id).Result;
            AppointmentVM app = response.Content.ReadAsAsync<AppointmentVM>().Result;
       
            System.Diagnostics.Debug.WriteLine("********* update app : app doc******* " + app.doctor.name);
            HttpResponseMessage response2= Client.GetAsync("http://localhost:18080/epione-jee-web/api/Reason/SearchReasonsByDoctor?idDoctor=" + app.doctor.id).Result;
            var reasons = response2.Content.ReadAsAsync<List<Reason>>().Result;
            ViewBag.doctorname1 = app.doctor.name;
            ViewData["reason"] = new SelectList(reasons, "id", "name");
            return View(app);
        }

        // POST: Appointment/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(AppointmentVM app)
        {
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Put,"http://localhost:18080/epione-jee-web/api/Appointment");
            request.Content = new StringContent("{}", Encoding.UTF8, "application/json");
            app.reason = new Reason() { id = app.reason_id };
            string date = String.Concat(app.date_start, ":00");
            app.date_start = date;
            app.state = "1";
            var json = JsonConvert.SerializeObject(app);

            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var response =await Client.SendAsync(request);
         
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            System.Diagnostics.Debug.WriteLine("****************** app updated i*****");

            if (response.IsSuccessStatusCode)
            {

                System.Diagnostics.Debug.WriteLine("******************okkk:" + response.StatusCode);
                return RedirectToAction("AppointmentByPatient");
            }
            else
            {
                ViewData["updated"] = "erreur";
                return View();
            }
            
        }

        //// GET: Appointment/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Appointment/Delete/5
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
    }
}
