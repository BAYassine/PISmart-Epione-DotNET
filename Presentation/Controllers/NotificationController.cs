using Domain.Entities;
using Presentation.Models;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class NotificationController : Controller
    {
        INotificationService ins;

        public NotificationController()
        {
            ins = new NotificationService();
        }

        // GET: Notification
        public ActionResult Index(string searchString)
        {
            var listnotification = ins.GetMany();

            if (!String.IsNullOrEmpty(searchString))
            {
                listnotification = ins.GetMany(m => m.content.Contains(searchString));
            }
            var films = new List<NotificationMV>();
            foreach (Notificationapp f in listnotification)
            {
                films.Add(new NotificationMV()
                {
                    content = f.content,
                    confirmation = f.confirmation,
                    new_Appointement_Date=f.new_Appointement_Date,
                    notified_at=f.notified_at,
                    patientnotif_id=f.patientnotif_id   
                });
            }
            return View(films);
        }

        //// POST: Notification/Delete/5
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

       // GET: Notification/Patient
        public async Task<ActionResult> GetNotifications()
        {
            System.Diagnostics.Debug.WriteLine("****Get notiif****** ");
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:18080/epione-jee-web/api/notification/show").Result;
            var result = response.Content.ReadAsAsync<IEnumerable<NotificationVM>>().Result;
            System.Diagnostics.Debug.WriteLine("****Get notiif count****** :"+result.Count());
            ViewBag.notification = result;

            return View();
        }

        //// GET: Notification/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Notification/Create
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

        //// GET: Notification/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Notification/Edit/5
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

        //// GET: Notification/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Notification/Delete/5
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
    }
}
