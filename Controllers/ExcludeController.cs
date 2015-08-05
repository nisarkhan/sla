using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SLAUnavailability.Web.Controllers
{
    public class ExcludeController : Controller
    {
        // GET: Exclude
        public ActionResult Index()
        {
            return View();
        } 

        // GET: Exclude/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Exclude/Create
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

        // GET: Exclude/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Exclude/Edit/5
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

        // GET: Exclude/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Exclude/Delete/5
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
