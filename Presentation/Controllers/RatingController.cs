using Newtonsoft.Json;
using Presentation.Models;
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
    public class RatingController : Controller
    {
        string Baseurl = "http://localhost:18080/";
        // GET: Rating
        public async Task<ActionResult> Index()
        {
            List<RatingVM> ratings = new List<RatingVM>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource doList using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("epione-jee-web/api/rating/get");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ratingResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the users list  
                    ratings = JsonConvert.DeserializeObject<List<RatingVM>>(ratingResponse);

                }
                //returning the employee list to view
                Console.WriteLine(ratings);
                return View(ratings);
            }
        }

        // GET: Rating/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rating/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rating/Create
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

        // GET: Rating/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Rating/Edit/5
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

        // GET: Rating/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Rating/Delete/5
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
