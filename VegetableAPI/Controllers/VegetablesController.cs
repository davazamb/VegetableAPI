using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VegetableAPI.Models;

namespace VegetableAPI.Controllers
{
    public class VegetablesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Vegetables
        public ActionResult Index()
        {
            return View(db.Vegetables.ToList());
        }

        // GET: Vegetables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vegetable vegetable = db.Vegetables.Find(id);
            if (vegetable == null)
            {
                return HttpNotFound();
            }
            return View(vegetable);
        }

        // GET: Vegetables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vegetables/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VegetableId,Description,Price")] Vegetable vegetable)
        {
            if (ModelState.IsValid)
            {
                db.Vegetables.Add(vegetable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vegetable);
        }

        // GET: Vegetables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vegetable vegetable = db.Vegetables.Find(id);
            if (vegetable == null)
            {
                return HttpNotFound();
            }
            return View(vegetable);
        }

        // POST: Vegetables/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VegetableId,Description,Price")] Vegetable vegetable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vegetable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vegetable);
        }

        // GET: Vegetables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vegetable vegetable = db.Vegetables.Find(id);
            if (vegetable == null)
            {
                return HttpNotFound();
            }
            return View(vegetable);
        }

        // POST: Vegetables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vegetable vegetable = db.Vegetables.Find(id);
            db.Vegetables.Remove(vegetable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
