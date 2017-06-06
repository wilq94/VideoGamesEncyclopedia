using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using VideoGamesEncyclopedia.Models;

namespace VideoGamesEncyclopedia.Controllers
{
    public class productController : Controller
    {
        private VideoGamesEncyclopediaDbEntities db = new VideoGamesEncyclopediaDbEntities();

        // GET: product
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;

            IQueryable<product> products = db.products;

            if (!string.IsNullOrEmpty(searchString))
            {
                products = db.products.Where(g => g.Name.ToString().Contains(searchString) || g.Description.Contains(searchString));
            }
            if (sortOrder == "rating")
            {
                products = products.OrderByDescending(x => x.Rating);
            }

            return View(products.ToList());
        }

        // GET: product/Details/5
        public ActionResult Gamecard(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult AddToWishlist(int id)
        {
            using (VideoGamesEncyclopediaDbEntities database = new VideoGamesEncyclopediaDbEntities())
            {
                var wishlistedProduct = database.wishedproducts.FirstOrDefault(wp => wp.ProductId == id);
                if(wishlistedProduct == null)
                {
                    int lastId = -1;
                    var recordWithLastId = database.wishedproducts.OrderByDescending(x => x.Id).FirstOrDefault();
                    if(recordWithLastId != null)
                    {
                        lastId = recordWithLastId.Id;
                    }
                    var newWishlistedProduct = new wishedproduct(lastId + 1, User.Identity.GetUserId(), id);
                    database.wishedproducts.Add(newWishlistedProduct);
                    database.SaveChanges();
                }
                
            }

            return RedirectToAction("Gamecard/" + id);
        }

        public ActionResult AddToIgnoredList(int id)
        {
            using (VideoGamesEncyclopediaDbEntities database = new VideoGamesEncyclopediaDbEntities())
            {
                //zrobic jakies warunki, zeby nie bylo na liscie zyczen, w ocenionych i ignorowanych jednoczesnie
                var ignoredProduct = database.ignoredproducts.FirstOrDefault(wp => wp.ProductId == id);
                if (ignoredProduct == null)
                {
                    int lastId = -1;
                    var recordWithLastId = database.ignoredproducts.OrderByDescending(x => x.Id).FirstOrDefault();
                    if (recordWithLastId != null)
                    {
                        lastId = recordWithLastId.Id;
                    }
                    var newIgnoredProduct = new ignoredproduct(lastId + 1, User.Identity.GetUserId(), id);
                    database.ignoredproducts.Add(newIgnoredProduct);
                    database.SaveChanges();
                }

            }


            return RedirectToAction("Gamecard/" + id);
        }

        // GET: product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CoverPath,PremiereDate,CreatedBy,Description,Rating")] product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CoverPath,PremiereDate,CreatedBy,Description,Rating")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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
