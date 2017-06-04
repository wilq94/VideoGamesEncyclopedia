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
    public class AspNetUserLoginController : Controller
    {
        private VideoGamesEncyclopediaDbEntities db = new VideoGamesEncyclopediaDbEntities();

        // GET: AspNetUserLogin
        public ActionResult Index()
        {
            var aspnetuserlogins = db.aspnetuserlogins.Include(a => a.aspnetuser);
            return View(aspnetuserlogins.ToList());
        }

        // GET: AspNetUserLogin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aspnetuserlogin aspnetuserlogin = db.aspnetuserlogins.Find(id);
            if (aspnetuserlogin == null)
            {
                return HttpNotFound();
            }
            return View(aspnetuserlogin);
        }

        // GET: AspNetUserLogin/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email");
            return View();
        }

        // POST: AspNetUserLogin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoginProvider,ProviderKey,UserId")] aspnetuserlogin aspnetuserlogin)
        {
            if (ModelState.IsValid)
            {
                db.aspnetuserlogins.Add(aspnetuserlogin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email", aspnetuserlogin.UserId);
            return View(aspnetuserlogin);
        }

        // GET: AspNetUserLogin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aspnetuserlogin aspnetuserlogin = db.aspnetuserlogins.Find(id);
            if (aspnetuserlogin == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email", aspnetuserlogin.UserId);
            return View(aspnetuserlogin);
        }

        // POST: AspNetUserLogin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoginProvider,ProviderKey,UserId")] aspnetuserlogin aspnetuserlogin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspnetuserlogin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email", aspnetuserlogin.UserId);
            return View(aspnetuserlogin);
        }

        // GET: AspNetUserLogin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aspnetuserlogin aspnetuserlogin = db.aspnetuserlogins.Find(id);
            if (aspnetuserlogin == null)
            {
                return HttpNotFound();
            }
            return View(aspnetuserlogin);
        }

        // POST: AspNetUserLogin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            aspnetuserlogin aspnetuserlogin = db.aspnetuserlogins.Find(id);
            db.aspnetuserlogins.Remove(aspnetuserlogin);
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
