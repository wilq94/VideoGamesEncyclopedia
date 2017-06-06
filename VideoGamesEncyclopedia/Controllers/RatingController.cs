using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using VideoGamesEncyclopedia.Models;

namespace VideoGamesEncyclopedia.Controllers
{
    public class RatingController : Controller
    {
        private VideoGamesEncyclopediaDbEntities db = new VideoGamesEncyclopediaDbEntities();

        // GET: Rating
        public ActionResult Index()
        {
            var ratings = db.ratings.Include(r => r.aspnetuser).Include(r => r.product);
            return View(ratings.ToList());
        }

        // GET: Rating/Details/5
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

        // GET: Rating/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email");
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name");
            return View();
        }

        // POST: Rating/Create
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

            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email", rating.UserId);
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", rating.ProductId);
            return View(rating);
        }

        // GET: Rating/Edit/5
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
            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email", rating.UserId);
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", rating.ProductId);
            return View(rating);
        }

        // POST: Rating/Edit/5
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
            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email", rating.UserId);
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", rating.ProductId);
            return View(rating);
        }

        // GET: Rating/Delete/5
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

        // POST: Rating/Delete/5
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
