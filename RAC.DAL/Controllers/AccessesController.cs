using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RAC.DAL.Controllers
{
    public class AccessesController : Controller
    {
        // GET: Accesses
        public ActionResult Index()
        {
            return View();
        }

        // GET: Accesses/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Accesses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accesses/Create
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

        // GET: Accesses/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Accesses/Edit/5
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

        // GET: Accesses/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Accesses/Delete/5
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
