using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using VegetableAPI.Classes;
using VegetableAPI.Models;

namespace VegetableAPI.Controllers.API
{
    public class VegetablesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Vegetables
        public IQueryable<Vegetable> GetVegetables()
        {
            return db.Vegetables;
        }

        // GET: api/Vegetables/5
        [ResponseType(typeof(Vegetable))]
        public IHttpActionResult GetVegetable(int id)
        {
            Vegetable vegetable = db.Vegetables.Find(id);
            if (vegetable == null)
            {
                return NotFound();
            }

            return Ok(vegetable);
        }

        // PUT: api/Vegetables/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVegetable(int id, VegetableRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.VegetableId)
            {
                return BadRequest();
            }

            if (request.ImageArray != null && request.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(request.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0}.jpg", guid);
                var folder = "~/Content/Images";
                var fullPath = string.Format("{0}/{1}", folder, file);
                var response = Fileshelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    request.Image = fullPath;
                }
            }

            var vegetable = ToVegetable(request);   
            db.Entry(vegetable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VegetableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Vegetables
        [ResponseType(typeof(Vegetable))]
        public IHttpActionResult PostVegetable(VegetableRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (request.ImageArray != null && request.ImageArray.Length>0)
            {
                var stream = new MemoryStream(request.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0}.jpg", guid);
                var folder = "~/Content/Images";
                var fullPath = string.Format("{0}/{1}", folder, file);
                var response = Fileshelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    request.Image = fullPath;
                }
            }

            var vegetable = ToVegetable(request);
            db.Vegetables.Add(vegetable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vegetable.VegetableId }, vegetable);
        }

        private Vegetable ToVegetable(VegetableRequest request)
        {
            return new Vegetable
            {
                Description = request.Description,
                VegetableId = request.VegetableId,
                IsActive = request.IsActive,
                Image = request.Image,
                LastPurchase = request.LastPurchase,
                Observation = request.Observation,
                Price = request.Price,

            };
        }

        // DELETE: api/Vegetables/5
        [ResponseType(typeof(Vegetable))]
        public IHttpActionResult DeleteVegetable(int id)
        {
            Vegetable vegetable = db.Vegetables.Find(id);
            if (vegetable == null)
            {
                return NotFound();
            }

            db.Vegetables.Remove(vegetable);
            db.SaveChanges();

            return Ok(vegetable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VegetableExists(int id)
        {
            return db.Vegetables.Count(e => e.VegetableId == id) > 0;
        }
    }
}