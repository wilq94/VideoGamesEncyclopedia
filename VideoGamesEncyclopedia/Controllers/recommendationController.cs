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
    public class recommendationController : Controller
    {
        private VideoGamesEncyclopediaDbEntities db = new VideoGamesEncyclopediaDbEntities();

        // GET: recommendation
        public ActionResult Index()
        {
            var recommendations = db.recommendations.Include(r => r.product).Include(r => r.user);
            return View(recommendations.ToList());
        }

        // GET: recommendation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recommendation recommendation = db.recommendations.Find(id);
            if (recommendation == null)
            {
                return HttpNotFound();
            }
            return View(recommendation);
        }

        // GET: recommendation/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name");
            ViewBag.UserId = new SelectList(db.users, "Id", "Nickname");
            return View();
        }

        // POST: recommendation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ProductId,RecommendationRating,WasPresented,GenerationDate")] recommendation recommendation)
        {
            if (ModelState.IsValid)
            {
                db.recommendations.Add(recommendation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", recommendation.ProductId);
            ViewBag.UserId = new SelectList(db.users, "Id", "Nickname", recommendation.UserId);
            return View(recommendation);
        }

        // GET: recommendation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recommendation recommendation = db.recommendations.Find(id);
            if (recommendation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", recommendation.ProductId);
            ViewBag.UserId = new SelectList(db.users, "Id", "Nickname", recommendation.UserId);
            return View(recommendation);
        }

        // POST: recommendation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ProductId,RecommendationRating,WasPresented,GenerationDate")] recommendation recommendation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recommendation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", recommendation.ProductId);
            ViewBag.UserId = new SelectList(db.users, "Id", "Nickname", recommendation.UserId);
            return View(recommendation);
        }

        // GET: recommendation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recommendation recommendation = db.recommendations.Find(id);
            if (recommendation == null)
            {
                return HttpNotFound();
            }
            return View(recommendation);
        }

        // POST: recommendation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            recommendation recommendation = db.recommendations.Find(id);
            db.recommendations.Remove(recommendation);
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
