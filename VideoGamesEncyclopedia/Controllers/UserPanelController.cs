using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VideoGamesEncyclopedia.Controllers
{
    public class UserPanelController : Controller
    {
        // GET: UserPanel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditProfile()
        {
            return View();
        }

        public ActionResult WishList()
        {
            return View();
        }

        public ActionResult IgnoredList()
        {
            return View();
        }

        public ActionResult RatedList()
        {
            return View();
        }
    }
}