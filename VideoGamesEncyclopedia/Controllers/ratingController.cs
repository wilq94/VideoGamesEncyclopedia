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
    public class ratingController : Controller
    {
        private VideoGamesEncyclopediaDbEntities db = new VideoGamesEncyclopediaDbEntities();

        // GET: rating
        public ActionResult Index()
        {
            var ratings = db.ratings.Include(r => r.product).Include(r => r.user);
            return View(ratings.ToList());
        }

        // GET: rating/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rating rating = db.ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // GET: rating/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name");
            ViewBag.UserId = new SelectList(db.users, "Id", "Nickname");
            return View();
        }

        // POST: rating/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ProductId,Rate")] rating rating)
        {
            if (ModelState.IsValid)
            {
                db.ratings.Add(rating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", rating.ProductId);
            ViewBag.UserId = new SelectList(db.users, "Id", "Nickname", rating.UserId);
            return View(rating);
        }

        // GET: rating/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rating rating = db.ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", rating.ProductId);
            ViewBag.UserId = new SelectList(db.users, "Id", "Nickname", rating.UserId);
            return View(rating);
        }

        // POST: rating/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ProductId,Rate")] rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", rating.ProductId);
            ViewBag.UserId = new SelectList(db.users, "Id", "Nickname", rating.UserId);
            return View(rating);
        }

        // GET: rating/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rating rating = db.ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: rating/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            rating rating = db.ratings.Find(id);
            db.ratings.Remove(rating);
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
