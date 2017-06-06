using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using VideoGamesEncyclopedia.Models;

namespace VideoGamesEncyclopedia.Controllers
{
    public class TicketController : Controller
    {
        private VideoGamesEncyclopediaDbEntities db = new VideoGamesEncyclopediaDbEntities();

        // GET: Ticket
        public ActionResult Index()
        {
            var tickets = db.tickets.Include(t => t.aspnetuser).Include(t => t.aspnetuser1);
            return View(tickets.ToList());
        }

        // GET: Ticket/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticket ticket = db.tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Ticket/Create
        public ActionResult Create()
        {
            ViewBag.ReportedBy = new SelectList(db.aspnetusers, "Id", "Email");
            ViewBag.SolvedBy = new SelectList(db.aspnetusers, "Id", "Email");
            return View();
        }

        // POST: Ticket/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ReportedBy,SolvedBy,Description,IsFinished")] ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReportedBy = new SelectList(db.aspnetusers, "Id", "Email", ticket.ReportedBy);
            ViewBag.SolvedBy = new SelectList(db.aspnetusers, "Id", "Email", ticket.SolvedBy);
            return View(ticket);
        }

        // GET: Ticket/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticket ticket = db.tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReportedBy = new SelectList(db.aspnetusers, "Id", "Email", ticket.ReportedBy);
            ViewBag.SolvedBy = new SelectList(db.aspnetusers, "Id", "Email", ticket.SolvedBy);
            return View(ticket);
        }

        // POST: Ticket/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ReportedBy,SolvedBy,Description,IsFinished")] ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReportedBy = new SelectList(db.aspnetusers, "Id", "Email", ticket.ReportedBy);
            ViewBag.SolvedBy = new SelectList(db.aspnetusers, "Id", "Email", ticket.SolvedBy);
            return View(ticket);
        }

        // GET: Ticket/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticket ticket = db.tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Ticket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ticket ticket = db.tickets.Find(id);
            db.tickets.Remove(ticket);
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
