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
    public class productscreenshotController : Controller
    {
        private VideoGamesEncyclopediaDbEntities db = new VideoGamesEncyclopediaDbEntities();

        // GET: productscreenshot
        public ActionResult Index()
        {
            var productscreenshots = db.productscreenshots.Include(p => p.product);
            return View(productscreenshots.ToList());
        }

        // GET: productscreenshot/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productscreenshot productscreenshot = db.productscreenshots.Find(id);
            if (productscreenshot == null)
            {
                return HttpNotFound();
            }
            return View(productscreenshot);
        }

        // GET: productscreenshot/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name");
            return View();
        }

        // POST: productscreenshot/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,ScreenshotPath")] productscreenshot productscreenshot)
        {
            if (ModelState.IsValid)
            {
                db.productscreenshots.Add(productscreenshot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", productscreenshot.ProductId);
            return View(productscreenshot);
        }

        // GET: productscreenshot/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productscreenshot productscreenshot = db.productscreenshots.Find(id);
            if (productscreenshot == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", productscreenshot.ProductId);
            return View(productscreenshot);
        }

        // POST: productscreenshot/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,ScreenshotPath")] productscreenshot productscreenshot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productscreenshot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", productscreenshot.ProductId);
            return View(productscreenshot);
        }

        // GET: productscreenshot/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productscreenshot productscreenshot = db.productscreenshots.Find(id);
            if (productscreenshot == null)
            {
                return HttpNotFound();
            }
            return View(productscreenshot);
        }

        // POST: productscreenshot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            productscreenshot productscreenshot = db.productscreenshots.Find(id);
            db.productscreenshots.Remove(productscreenshot);
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
