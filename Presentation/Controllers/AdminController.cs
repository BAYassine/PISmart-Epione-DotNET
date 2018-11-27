using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using Service;

namespace Presentation.Controllers.Admin
{
    public class AdminController : Controller
    {
        static HttpClient client = new HttpClient();
        static IUserService userService = new UserService();
        // Author : Yassine
        public async Task<ActionResult> Dashboard()
        {
            if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth", new { returnUrl = HttpContext.Request.Url.AbsolutePath });
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["authtoken"] + "");
            String response = await client.GetStringAsync("http://localhost:18080/epione-jee-web/api/admin");
            JObject dash_data = JObject.Parse(response);
            ViewData["dash_data"] = dash_data;
            return View();
        }

        public ActionResult Users()
        {
            if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth", new { returnUrl = HttpContext.Request.Url.AbsolutePath });
            ViewBag.users = userService.GetMany().ToList();
            return View();
        }

        public void ExportListUsingEPPlus()
        {
            var data = userService.GetMany().ToList();

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=Users.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

    }
}