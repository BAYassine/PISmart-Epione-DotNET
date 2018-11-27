using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using SpeechLib;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class DoctolibController : Controller
    {
        // GET: Doctolib
        public ActionResult Index(string doctor, string city, string speciality)
        {

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:18080");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!String.IsNullOrEmpty(speciality))
            {

                HttpResponseMessage response = httpClient.GetAsync("epione-jee-web/api/doctolib?speciality="+speciality+"&location="+city).Result;

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.result = response.Content.ReadAsAsync<IEnumerable<DoctolibVM>>().Result;
                    
                }
                else
                {
                    ViewBag.result = "error";
                }
            }
            else if (!String.IsNullOrEmpty(doctor))
            {
                HttpResponseMessage response = httpClient.GetAsync("epione-jee-web/api/doctolib?name=" + doctor + "&location=" + city).Result;

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.result = response.Content.ReadAsAsync<IEnumerable<DoctolibVM>>().Result;

                }
                else
                {
                    ViewBag.result = "error";
                }
            }

            return View();
        }

        public ActionResult Profile(string path)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:18080");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync("epione-jee-web/api/doctolib?path=" + path).Result;

            if (response.IsSuccessStatusCode)
            {
                SpVoice sp = new SpVoice();
                if (path.Contains("clinique") || path.Contains("hopital") || path.Contains("centre") || path.Contains("cabinet") || path.Contains("etablissement") || path.Contains("maison"))
                {
                    DoctolibOtherVM other = response.Content.ReadAsAsync<DoctolibOtherVM>().Result;
                    var groups = other.lstDoctors.GroupBy(x => x.speciality,i=>i,(key, v) => new GroupedItem { GroupName = key, Items = v });
                    ViewBag.result = other;
                    ViewBag.groups = groups;
                    Task.Delay(2000).ContinueWith(name => {
                        sp.Speak("<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'>Welcome to " + other.speciality + " of <voice xml:lang='fr-FR' gender='female'> " + other.name + " </voice> . Located in <voice xml:lang='fr-FR' gender='female'> " + other.address + " </voice>  </speak>");
                        sp.Speak("<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'>This " + other.speciality + " contain " + groups.Count() + " speciality and "+ other.lstDoctors.Count() +" doctors in total. </speak>");
                        foreach (GroupedItem group in groups)
                        {
                            sp.Speak("<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'>" + group.Items.Count() + " " + group.GroupName + ".</speak>");
                            foreach (DoctolibVM elem in group.Items)
                            {
                                if(group.Items.ToList().IndexOf(elem) == 0 || group.Items.ToList().IndexOf(elem) < group.Items.Count() - 1)
                                    sp.Speak("<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'>" + elem.name + "</speak>");
                                else
                                    sp.Speak("<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'>and " + elem.name + "</speak>");
                            }
                        }
                    });
                }
                else
                {
                    DoctolibDoctorVM doctor = response.Content.ReadAsAsync<DoctolibDoctorVM>().Result;
                    ViewBag.result = doctor;
                    Task.Delay(2000).ContinueWith(name => {
                        sp.Speak("<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'>Welcome to " + doctor.name + " profile  </speak>");
                        Task.Delay(500).ContinueWith(spe => {
                            sp.Speak("<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'>Speciality " + doctor.speciality + " </speak>");
                            Task.Delay(500).ContinueWith(location => {
                                sp.Speak("<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'>Located in <voice xml:lang='fr-FR' gender='female'> " + doctor.address + " </voice></speak>");
                                Task.Delay(500).ContinueWith(desc => {
                                    sp.Speak("<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'>Descripted as " + doctor.description + " </speak>");
                                });
                            });
                        });
                    });
                    
                    
                }
                    
            }
            else
            {
                ViewBag.result = "error";
            }

            

            return View();
        }
    }

    public class GroupedItem
    {
        public string GroupName { set; get; }
        public IEnumerable<DoctolibVM> Items { set; get; }
    }
}