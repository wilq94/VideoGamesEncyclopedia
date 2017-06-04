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
    public class IgnoredProductController : Controller
    {
        private VideoGamesEncyclopediaDbEntities db = new VideoGamesEncyclopediaDbEntities();

        // GET: IgnoredProduct
        public ActionResult Index()
        {
            var ignoredproducts = db.ignoredproducts.Include(i => i.aspnetuser).Include(i => i.product);
            return View(ignoredproducts.ToList());
        }

        // GET: IgnoredProduct/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ignoredproduct ignoredproduct = db.ignoredproducts.Find(id);
            if (ignoredproduct == null)
            {
                return HttpNotFound();
            }
            return View(ignoredproduct);
        }

        // GET: IgnoredProduct/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email");
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name");
            return View();
        }

        // POST: IgnoredProduct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ProductId")] ignoredproduct ignoredproduct)
        {
            if (ModelState.IsValid)
            {
                db.ignoredproducts.Add(ignoredproduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email", ignoredproduct.UserId);
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", ignoredproduct.ProductId);
            return View(ignoredproduct);
        }

        // GET: IgnoredProduct/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ignoredproduct ignoredproduct = db.ignoredproducts.Find(id);
            if (ignoredproduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email", ignoredproduct.UserId);
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", ignoredproduct.ProductId);
            return View(ignoredproduct);
        }

        // POST: IgnoredProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ProductId")] ignoredproduct ignoredproduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ignoredproduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email", ignoredproduct.UserId);
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", ignoredproduct.ProductId);
            return View(ignoredproduct);
        }

        // GET: IgnoredProduct/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ignoredproduct ignoredproduct = db.ignoredproducts.Find(id);
            if (ignoredproduct == null)
            {
                return HttpNotFound();
            }
            return View(ignoredproduct);
        }

        // POST: IgnoredProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ignoredproduct ignoredproduct = db.ignoredproducts.Find(id);
            db.ignoredproducts.Remove(ignoredproduct);
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
