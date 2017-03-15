using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VegetableAPI.Classes;   
using VegetableAPI.Models;

namespace VegetableAPI.Controllers
{
    public class VegetablesController : Controller
    {
        private DataContext db; 

        public VegetablesController()
        {
            db = new DataContext();
        }

        // GET: Vegetables
        public ActionResult Index()
        {
            return View(db.Vegetables.OrderBy(v => v.Description).ToList());
        }

        // GET: Vegetables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vegetable = db.Vegetables.Find(id);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VegetableView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = Fileshelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var vegetable = ToVegetable(view);
                vegetable.Image = pic;
                db.Vegetables.Add(vegetable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        private Vegetable ToVegetable(VegetableView view)
        {
            return new Vegetable
            {
                Description = view.Description,
                VegetableId = view.VegetableId,
                Image = view.Image,
                IsActive = view.IsActive,
                LastPurchase = view.LastPurchase,
                Observation = view.Observation,
                Price = view.Price,
            };
        }

        // GET: Vegetables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vegetable = db.Vegetables.Find(id);
            if (vegetable == null)
            {
                return HttpNotFound();
            }

            return View(ToView(vegetable));
        }

        private VegetableView ToView(Vegetable vegetable)
        {
            return new VegetableView
            {
                Description = vegetable.Description,
                VegetableId = vegetable.VegetableId,
                Image = vegetable.Image,
                IsActive = vegetable.IsActive,
                LastPurchase = vegetable.LastPurchase,
                Observation = vegetable.Observation,
                Price = vegetable.Price,
            };
        }

        // POST: Vegetables/Edit/5 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VegetableView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.Image;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = Fileshelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var vegetable = ToVegetable(view);
                vegetable.Image = pic; 

                db.Entry(vegetable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(view);
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
