using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SuperCarro;

namespace SuperCarro.Controllers
{
    public class VehiclesController : Controller
    {
        private INTEC_SuperCarEntities db = new INTEC_SuperCarEntities();

        // GET: Vehicles
        public ActionResult Index()
        {
            var vehicle = db.Vehicle.Include(v => v.Category).Include(v => v.Mark).Include(v => v.Model).Include(v => v.Supplier).OrderByDescending(x=> x.CreatedDate);
            return View(vehicle.ToList());
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicle.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name");
            ViewBag.MarkId = new SelectList(db.Mark, "Id", "Name");
            ViewBag.ModelId = new SelectList(db.Model, "Id", "Name");
            ViewBag.SupplierId = new SelectList(db.Supplier, "Id", "Name");
            return View(new Vehicle());
        }

        // POST: Vehicles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MarkId,Description,Color,CategoryId,Year,Price,FuelType,Ofert,SupplierId,MinStock,MaxStock,Puertas,Picture,Enabled")] Vehicle vehicle, HttpPostedFileBase file )
        {
            if (ModelState.IsValid)
            {
                vehicle.Id = Guid.NewGuid().ToString();
                vehicle.CreatedDate = DateTime.Now;

                if(file != null)
                {
                    string pictureUrl = System.IO.Path.GetFileName(file.FileName);
                    string pathUrl = System.IO.Path.Combine(Server.MapPath("/Public/Vehicles"), pictureUrl);

                    file.SaveAs(pathUrl);

                    vehicle.Picture = pictureUrl;
                }

                db.Vehicle.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", vehicle.CategoryId);
            ViewBag.MarkId = new SelectList(db.Mark, "Id", "Name", vehicle.MarkId);
            ViewBag.ModelId = new SelectList(db.Model, "Id", "Name", vehicle.ModelId);
            ViewBag.SupplierId = new SelectList(db.Supplier, "Id", "Name", vehicle.SupplierId);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicle.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", vehicle.CategoryId);
            ViewBag.MarkId = new SelectList(db.Mark, "Id", "Name", vehicle.MarkId);
            ViewBag.ModelId = new SelectList(db.Model, "Id", "Name", vehicle.ModelId);
            ViewBag.SupplierId = new SelectList(db.Supplier, "Id", "Name", vehicle.SupplierId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MarkId,ModelId,Description,Color,CategoryId,Year,Price,FuelType,Ofert,SupplierId,MinStock,MaxStock,Puertas,Picture,Enabled")] Vehicle vehicle, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                vehicle.Id = Guid.NewGuid().ToString();
                vehicle.CreatedDate = DateTime.Now;

                if (file != null)
                {
                    string pictureUrl = System.IO.Path.GetFileName(file.FileName);
                    string pathUrl = System.IO.Path.Combine(Server.MapPath("/Public/Vehicles"), pictureUrl);

                    file.SaveAs(pathUrl);

                    vehicle.Picture = pictureUrl;
                }

                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", vehicle.CategoryId);
            ViewBag.MarkId = new SelectList(db.Mark, "Id", "Name", vehicle.MarkId);
            ViewBag.ModelId = new SelectList(db.Model, "Id", "Name", vehicle.ModelId);
            ViewBag.SupplierId = new SelectList(db.Supplier, "Id", "Name", vehicle.SupplierId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicle.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Vehicle vehicle = db.Vehicle.Find(id);
            db.Vehicle.Remove(vehicle);
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
