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
    public class AspNetUserClaimController : Controller
    {
        private VideoGamesEncyclopediaDbEntities db = new VideoGamesEncyclopediaDbEntities();

        // GET: AspNetUserClaim
        public ActionResult Index()
        {
            var aspnetuserclaims = db.aspnetuserclaims.Include(a => a.aspnetuser);
            return View(aspnetuserclaims.ToList());
        }

        // GET: AspNetUserClaim/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aspnetuserclaim aspnetuserclaim = db.aspnetuserclaims.Find(id);
            if (aspnetuserclaim == null)
            {
                return HttpNotFound();
            }
            return View(aspnetuserclaim);
        }

        // GET: AspNetUserClaim/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email");
            return View();
        }

        // POST: AspNetUserClaim/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ClaimType,ClaimValue")] aspnetuserclaim aspnetuserclaim)
        {
            if (ModelState.IsValid)
            {
                db.aspnetuserclaims.Add(aspnetuserclaim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email", aspnetuserclaim.UserId);
            return View(aspnetuserclaim);
        }

        // GET: AspNetUserClaim/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aspnetuserclaim aspnetuserclaim = db.aspnetuserclaims.Find(id);
            if (aspnetuserclaim == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email", aspnetuserclaim.UserId);
            return View(aspnetuserclaim);
        }

        // POST: AspNetUserClaim/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ClaimType,ClaimValue")] aspnetuserclaim aspnetuserclaim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspnetuserclaim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email", aspnetuserclaim.UserId);
            return View(aspnetuserclaim);
        }

        // GET: AspNetUserClaim/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aspnetuserclaim aspnetuserclaim = db.aspnetuserclaims.Find(id);
            if (aspnetuserclaim == null)
            {
                return HttpNotFound();
            }
            return View(aspnetuserclaim);
        }

        // POST: AspNetUserClaim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            aspnetuserclaim aspnetuserclaim = db.aspnetuserclaims.Find(id);
            db.aspnetuserclaims.Remove(aspnetuserclaim);
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
