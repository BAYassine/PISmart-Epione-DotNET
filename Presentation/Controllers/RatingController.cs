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
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
                //Sending request to find web api REST service resource doList using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("epione-jee-web/api/rating/patient/"+ Session["username"]);

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
        public async Task<ActionResult> Create()
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
                //Sending request to find web api REST service resource doList using HttpClient  
                HttpResponseMessage Res =  client.GetAsync("epione-jee-web/api/Appointment").Result;

                //Checking the response is successful or not which is sent using HttpClient  
               
                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsAsync<IEnumerable<AppointmentVM>>().Result;
                    System.Diagnostics.Debug.WriteLine("****apppp****"+ Response.ElementAt<AppointmentVM>(0).id);
                    //Deserializing the response recieved from web api and storing into the users list  
                    List<SelectListItem> myList = new List<SelectListItem>();
                    foreach (var item in Response)
                    {
                        SelectListItem s = new SelectListItem() { Text = item.message , Value =(item.id).ToString() };
                        myList.Add(s);
                    }

                    ViewBag.list = myList;
             
                //returning the employee list to view
                return View();
            }
        }

        // POST: Rating/Create
        [HttpPost]
        public async Task<ActionResult> Create(RatingWithAppVM rvm)
        {
            using (var client = new HttpClient())
            {
                CreatRatingVM rvm2 = new CreatRatingVM() { rate = rvm.rate, comment = rvm.comment };
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
                //Sending request to find web api REST service resource doList using HttpClient  
                HttpResponseMessage Res = await client.PostAsJsonAsync<CreatRatingVM>("epione-jee-web/api/rating/add/"+int.Parse(rvm.id_appointment), rvm2);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the users list  

                }
                //returning the employee list to view
                return RedirectToAction("Index");
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
        public async Task<ActionResult> Delete(int id)
        {
            if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.DeleteAsync("epione-jee-web/api/rating/" + id);
            
                if (Res.IsSuccessStatusCode)
                {
                    var stockResponse = Res.Content.ReadAsStringAsync().Result;
                    //String j = JsonConvert.DeserializeObject<String>(stockResponse);

                }
                return RedirectToAction("Index");
            }
        }
    }
        

      
}
    

