using Domain.Entities;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace Presentation.Controllers
{
    public class AuthController : Controller
    {
        static HttpClient client = new HttpClient();

        public ActionResult Login(string returnUrl = "~/Home/Index")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string username, string password, string returnUrl)
        {
            var credtls = new Dictionary<string, string>();
            credtls.Add("username", username);
            credtls.Add("password", password);
            var response = await client.PostAsync("http://localhost:18080/epione-jee-web/api/auth", new FormUrlEncodedContent(credtls));
            string body = response.Content.ReadAsStringAsync().Result;
            if (body.Equals("Bad credentials"))
            {
                ViewData["error"] = "User not found or wrong password";
                return View();
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", body);
            response = await client.GetAsync("http://localhost:18080/epione-jee-web/api/users");
            if (response.IsSuccessStatusCode)
            {
                JToken json = JToken.Parse(await response.Content.ReadAsStringAsync());
                if (json["role"].ToString() == "ROLE_PATIENT")
                    Session["user"] = json.ToObject<Patient>();
                else if (json["role"].ToString() == "ROLE_DOCTOR")
                {
                    Session["user"] = json.ToObject<Doctor>();
                }
                else
                {
                    Session["user"] = json.ToObject<User>();
                }
            }
            Session["authtoken"] = body;
            return RedirectToLocal(returnUrl);
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
                if (!response.IsSuccessStatusCode)
                {
                    ViewData["error"] = contents;
                    return View();
                }
                    
                return View("Greeting");
            }

            return View(uservm);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}