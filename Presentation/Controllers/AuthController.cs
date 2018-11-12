using Domain.Entities;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class AuthController : Controller
    {
        static HttpClient client = new HttpClient();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string username, string password)
        {
            var credtls = new Dictionary<string, string>();
            credtls.Add("username", username);
            credtls.Add("password", password);
            var response = await client.PostAsync("http://localhost:18080/epione-jee-web/api/auth", new FormUrlEncodedContent(credtls));
            response.EnsureSuccessStatusCode();
            Session["authtoken"] = response.Content.ReadAsStringAsync().Result;
            //System.Diagnostics.Debug.WriteLine("token : " + Session["authtoken"]);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Remove("authtoken");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> register([Bind(Include = "username,email,password,confirmPassword,role")] UserVM uservm)
        {
            if (ModelState.IsValid)
            {
                var u = new 
                {
                    username = uservm.username,
                    email = uservm.email,
                    password = uservm.password,
                    role = uservm.role.ToString()
                };
                HttpResponseMessage response = await client.PostAsJsonAsync("http://localhost:18080/epione-jee-web/api/users/register", u);
                var contents = response.Content.ReadAsStringAsync().Result;
                response.EnsureSuccessStatusCode();
                return View("Greeting");
            }

            return View(uservm);
        }

    }
}