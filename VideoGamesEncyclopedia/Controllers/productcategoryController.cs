using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoGamesEncyclopedia.Models;

namespace VideoGamesEncyclopedia.Controllers
{
    public class productcategoryController : Controller
    {
        private VideoGamesEncyclopediaDbEntities db = new VideoGamesEncyclopediaDbEntities();

        // GET: productcategory
        public ActionResult Index()
        {
            var productcategories = db.productcategories.Include(p => p.category).Include(p => p.product);
            return View(productcategories.ToList());
        }

        // GET: productcategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productcategory productcategory = db.productcategories.Find(id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            return View(productcategory);
        }

        // GET: productcategory/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.categories, "Id", "Name");
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name");
            return View();
        }

        // POST: productcategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,CategoryId")] productcategory productcategory)
        {
            if (ModelState.IsValid)
            {
                db.productcategories.Add(productcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.categories, "Id", "Name", productcategory.CategoryId);
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", productcategory.ProductId);
            return View(productcategory);
        }

        // GET: productcategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productcategory productcategory = db.productcategories.Find(id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.categories, "Id", "Name", productcategory.CategoryId);
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", productcategory.ProductId);
            return View(productcategory);
        }

        // POST: productcategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,CategoryId")] productcategory productcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.categories, "Id", "Name", productcategory.CategoryId);
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", productcategory.ProductId);
            return View(productcategory);
        }

        // GET: productcategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productcategory productcategory = db.productcategories.Find(id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            return View(productcategory);
        }

        // POST: productcategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            productcategory productcategory = db.productcategories.Find(id);
            db.productcategories.Remove(productcategory);
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
