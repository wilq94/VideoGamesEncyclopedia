using Microsoft.AspNet.Identity;
using System;
using System.Collections;
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
    public class RecommendationController : Controller
    {
        private VideoGamesEncyclopediaDbEntities db = new VideoGamesEncyclopediaDbEntities();

        // GET: Recommendation
        //jak sie zdazy to poprawic, zeby wychodzil poza 10 kategorii i bral pod uwage liste zyczen i ignorowane (tez np. po 10 losowych)
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                //przypadek gdzie juz czeka wygenerowane
                var recommendations = db.recommendations.Include(r => r.aspnetuser).Include(r => r.product).Where(u => u.UserId == userId).Where(p => p.WasPresented == false).FirstOrDefault();
                if(recommendations != null)
                {
                    //wykorzystaj stare
                    product product = db.products.Find(recommendations.ProductId);
                    return View(product);
                }
                else
                {
                    //generowanie nowego ze sprawdzaniem czy widzial juz to wczesniej
                    //rekomendacja dziala tak, ze sprawdza do 10 najlepiej ocenianych gier uzytkownika (jesli niefiltrowane)
                    var userRates = db.ratings.Where(u => u.UserId == userId).OrderByDescending(r => r.Rate).Take(10);
                    if(userRates != null)
                    {
                        int[] categoryIds = new int[db.categories.Count()];

                        foreach (var item in userRates)
                        {
                            //bierze ich kategorie, zlicza ile razy wystapila kategoria i 
                            var productCategories = db.productcategories.Where(p => p.ProductId == item.ProductId);
                            foreach(var category in productCategories)
                            {
                                categoryIds[category.CategoryId.Value]++;
                            }
                        }
                        //bierze po 1 losowej grze (nie widzianej przez gracza) az nie dojdzie do wyboru 10 gier pasujacej do kategorii
                        List <int> productIds = new List<int>();
                        while (productIds.Count() < 10)
                        {
                            int maxIndex = 1;
                            for(int i=0; i<categoryIds.Count(); i++)
                            {
                                if(categoryIds[i] > maxIndex)
                                {
                                    maxIndex = i;
                                }
                            }
                            var rand = new Random();
                            var randedId = rand.Next(db.productcategories.Where(c => c.CategoryId == maxIndex).Count());
                            var productcategories = db.productcategories.Where(c => c.CategoryId == maxIndex).OrderBy(p => p.Id).Skip(randedId).Take(1).FirstOrDefault();
                            productIds.Add(productcategories.ProductId.Value);
                            categoryIds[maxIndex] = 0;
                        }

                        int rate = 10;

                        var lastRecommendationId = db.recommendations.OrderByDescending(x => x.Id).First().Id;
                        int newRecommendationId = lastRecommendationId + 1;

                        foreach (var productId in productIds)
                        {
                            
                            
                            db.recommendations.Add(new recommendation(newRecommendationId, User.Identity.GetUserId(), productId, rate, false, DateTime.Now));
                            newRecommendationId++;
                            rate--;
                        }
                        db.SaveChanges();
                        product product = db.products.Find(productIds.Take(1).FirstOrDefault());
                        return View(product);
                    }
                }
            }
            else
            {
                //tutaj losowe tytuły
                var rand = new Random();
                var randedId = rand.Next(db.products.Count());
                var product = db.products.OrderBy(p => p.Id).Skip(randedId).Take(1).FirstOrDefault();
                return View(product);
            }
            return View("Error");
        }

        public ActionResult FindAnother()
        {
            if (User.Identity.IsAuthenticated)
            { 
                var userId = User.Identity.GetUserId();

                var firstRecommendation = db.recommendations.Where(u => u.UserId == userId).Where(wp => wp.WasPresented == false).FirstOrDefault();

                firstRecommendation.WasPresented = true;

                db.Entry(firstRecommendation).State = EntityState.Modified;

                db.SaveChanges();
            }

            return RedirectToAction("Index","Recommendation");
        }

        // GET: Recommendation/Details/5
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

        // GET: Recommendation/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email");
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name");
            return View();
        }

        // POST: Recommendation/Create
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

            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email", recommendation.UserId);
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", recommendation.ProductId);
            return View(recommendation);
        }

        // GET: Recommendation/Edit/5
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
            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email", recommendation.UserId);
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", recommendation.ProductId);
            return View(recommendation);
        }

        // POST: Recommendation/Edit/5
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
            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email", recommendation.UserId);
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", recommendation.ProductId);
            return View(recommendation);
        }

        // GET: Recommendation/Delete/5
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

        // POST: Recommendation/Delete/5
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
