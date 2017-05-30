using System;
using System.Linq;
using System.Web.Mvc;
using VideoGamesEncyclopedia.Models;
using MySql.AspNet.Identity;
using Microsoft.AspNet.Identity;

namespace VideoGamesEncyclopedia.Controllers
{
    public class AdminPanelController : Controller
    {
        private VideoGamesEncyclopediaDbEntities db = new VideoGamesEncyclopediaDbEntities();

        // GET: AdminPanel
        public ActionResult Index()
        {
            if (isAdmin() || isPublisher())
            {
                return View();
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult AddGame()
        {
            if (isAdmin() || isPublisher())
            {
                return View();
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult Games()
        {
            if (isAdmin() || isPublisher())
            {
                return View(db.products.ToList());
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult Users()
        {
            if (isAdmin())
            {
                return View();
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult Settings()
        {
            if (isAdmin())
            {
                return View();
            }

            else
            {
                return View("Error");
            }
        }

        public ActionResult Tickets()
        {
            if (isAdmin())
            {
                return View();
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult Categories()
        {
            if (isAdmin() || isPublisher())
            {
                return View();
            }
            else
            {
                return View("Error");
            }
        }

        public Boolean isAdmin()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                VideoGamesEncyclopediaDbEntities context = new VideoGamesEncyclopediaDbEntities();
                var UserManager = new UserManager<ApplicationUser>(new MySqlUserStore<ApplicationUser>("DefaultConnection"));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public Boolean isPublisher()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                VideoGamesEncyclopediaDbEntities context = new VideoGamesEncyclopediaDbEntities();
                var UserManager = new UserManager<ApplicationUser>(new MySqlUserStore<ApplicationUser>("DefaultConnection"));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Publisher")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

    }
}