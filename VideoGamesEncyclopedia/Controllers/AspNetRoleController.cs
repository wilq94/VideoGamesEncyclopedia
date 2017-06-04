﻿using System;
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
    public class AspNetRoleController : Controller
    {
        private VideoGamesEncyclopediaDbEntities db = new VideoGamesEncyclopediaDbEntities();

        // GET: AspNetRole
        public ActionResult Index()
        {
            return View(db.aspnetroles.ToList());
        }

        // GET: AspNetRole/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aspnetrole aspnetrole = db.aspnetroles.Find(id);
            if (aspnetrole == null)
            {
                return HttpNotFound();
            }
            return View(aspnetrole);
        }

        // GET: AspNetRole/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AspNetRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] aspnetrole aspnetrole)
        {
            if (ModelState.IsValid)
            {
                db.aspnetroles.Add(aspnetrole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspnetrole);
        }

        // GET: AspNetRole/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aspnetrole aspnetrole = db.aspnetroles.Find(id);
            if (aspnetrole == null)
            {
                return HttpNotFound();
            }
            return View(aspnetrole);
        }

        // POST: AspNetRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] aspnetrole aspnetrole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspnetrole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspnetrole);
        }

        // GET: AspNetRole/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aspnetrole aspnetrole = db.aspnetroles.Find(id);
            if (aspnetrole == null)
            {
                return HttpNotFound();
            }
            return View(aspnetrole);
        }

        // POST: AspNetRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            aspnetrole aspnetrole = db.aspnetroles.Find(id);
            db.aspnetroles.Remove(aspnetrole);
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
