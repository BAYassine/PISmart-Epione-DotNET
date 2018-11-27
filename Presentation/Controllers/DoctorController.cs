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
                return RedirectToAction("Login", "Auth", new { returnUrl = HttpContext.Request.Url.AbsolutePath });
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            String response = await client.GetStringAsync("http://localhost:18080/epione-jee-web/api/dashboard");
            JObject dash_data = JObject.Parse(response);
            ViewData["dash_data"] = dash_data;
            return View();
        }

        public async Task<ActionResult> UpdateAppointment(int id, bool state)
        {

            if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth", new { returnUrl = HttpContext.Request.Url.AbsolutePath });
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            await client.PutAsync("http://localhost:18080/epione-jee-web/api/Appointment/changestate/" + id+ "?action="+state, null);
            return RedirectToAction("Dashboard");
        }

        // GET: Account
        public ActionResult Account()
        {
            if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth", new { returnUrl = HttpContext.Request.Url.AbsolutePath });
            Domain.Entities.Doctor doctor = (Domain.Entities.Doctor)Session["user"];
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
        public ActionResult Account([Bind(Include = "email,username,password,currentPassword")] AccountVM accountVM)
        {
            if (ModelState.IsValid)
            {
                Doctor u = userService.GetById(((User)Session["user"]).Id);
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

    }

}