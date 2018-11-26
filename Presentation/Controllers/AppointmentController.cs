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
using System.Net.Http;
using System.Net.Http.Headers;
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
        // GET: Appointment
        public ActionResult Index()
        {
            return View();
        }

        // GET: Appointment/Details/5
        public ActionResult Details(int id)
        {
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
            System.Diagnostics.Debug.WriteLine("********* state ******* "+result.ElementAt<AppointmentVM>(0).state);
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

            if (response.IsSuccessStatusCode)
            {
              
                System.Diagnostics.Debug.WriteLine("******************okkk:" +response.StatusCode);
                ViewData["canceled"] = "ok";
            }
            else
            {
                ViewData["canceled"] = "erreur";
            }
            return RedirectToAction("AppointmentByPatient");
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
            Model.reason_id = 4;
            System.Diagnostics.Debug.WriteLine("********* From form******* " + Model.date_start);
            //string s = DateTime.ParseExact(Model.date_start, "yyyy-MM-dd HH:mm:ss", null).ToString();
            //System.Diagnostics.Debug.WriteLine("********* conveeert******* " + s);
            // s.Replace("T", " ");
            //s =s.Replace("/","-");
            string date = String.Concat(Model.date_start, ":00");
            a.date_start = date;
        
            string st = "1";
            a.state = st;
            a.doctor = new Doctor();
            System.Diagnostics.Debug.WriteLine("********* Daaaaaate******* " + a.date_start);
            a.doctor.id = Model.doctor_id;
            a.patient = new Patient();
            a.patient.id = 8;
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
                return RedirectToAction("AppointmentByPatient");

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
            //List<SelectListItem> lst = new List<SelectListItem>();
            //foreach(Reason r in  result)
            //{
            //    lst.Add(new SelectListItem()
            //    {
            //        Value = r.id.ToString(),
            //        Text = r.name
            //    });
            //}
            //AppointmentVM Model = new AppointmentVM();
            //Model.reasonList = lst;
            ViewBag.doctorname = name;
            ViewBag.doctorid = doctor_id;
            ViewData["reason_id"] = new SelectList(result, "id", "name");
           System.Diagnostics.Debug.WriteLine("********* docotor name******* " + name);
            
            return View();
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

        // GET: Appointment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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
