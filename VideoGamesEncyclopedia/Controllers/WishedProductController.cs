using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using VideoGamesEncyclopedia.Models;

namespace VideoGamesEncyclopedia.Controllers
{
    public class WishedProductController : Controller
    {
        private VideoGamesEncyclopediaDbEntities db = new VideoGamesEncyclopediaDbEntities();

        // GET: WishedProduct
        public ActionResult Index()
        {
            var wishedproducts = db.wishedproducts.Include(w => w.aspnetuser).Include(w => w.product);
            return View(wishedproducts.ToList());
        }

        // GET: WishedProduct/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wishedproduct wishedproduct = db.wishedproducts.Find(id);
            if (wishedproduct == null)
            {
                return HttpNotFound();
            }
            return View(wishedproduct);
        }

        // GET: WishedProduct/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email");
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name");
            return View();
        }

        // POST: WishedProduct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ProductId")] wishedproduct wishedproduct)
        {
            if (ModelState.IsValid)
            {
                db.wishedproducts.Add(wishedproduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email", wishedproduct.UserId);
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", wishedproduct.ProductId);
            return View(wishedproduct);
        }

        // GET: WishedProduct/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wishedproduct wishedproduct = db.wishedproducts.Find(id);
            if (wishedproduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email", wishedproduct.UserId);
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", wishedproduct.ProductId);
            return View(wishedproduct);
        }

        // POST: WishedProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ProductId")] wishedproduct wishedproduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wishedproduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.aspnetusers, "Id", "Email", wishedproduct.UserId);
            ViewBag.ProductId = new SelectList(db.products, "Id", "Name", wishedproduct.ProductId);
            return View(wishedproduct);
        }

        // GET: WishedProduct/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wishedproduct wishedproduct = db.wishedproducts.Find(id);
            if (wishedproduct == null)
            {
                return HttpNotFound();
            }
            return View(wishedproduct);
        }

        // POST: WishedProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            wishedproduct wishedproduct = db.wishedproducts.Find(id);
            db.wishedproducts.Remove(wishedproduct);
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
