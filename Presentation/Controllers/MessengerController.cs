using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class MessengerController : Controller
    {
        // GET: Messenger
        public ActionResult Messenger()
        {
            if (Session["authtoken"] == null)
                return RedirectToAction("Login", "Auth");
            ViewBag.MyString = Session["user"];
            return View();
        }

        // GET: Messenger/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Messenger/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Messenger/Create
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

        // GET: Messenger/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Messenger/Edit/5
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

        // GET: Messenger/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Messenger/Delete/5
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
