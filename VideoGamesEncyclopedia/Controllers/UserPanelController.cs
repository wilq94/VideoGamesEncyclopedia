using Microsoft.AspNet.Identity;
using MySql.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoGamesEncyclopedia.Models;

namespace VideoGamesEncyclopedia.Controllers
{
    public class UserPanelController : Controller
    {
        // GET: UserPanel
        public ActionResult Index()
        {
            if (isUser())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult WishList()
        {
            if (isUser())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult IgnoredList()
        {
            if (isUser())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult RatedList()
        {
            if (isUser())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public Boolean isUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                VideoGamesEncyclopediaDbEntities context = new VideoGamesEncyclopediaDbEntities();
                var UserManager = new UserManager<ApplicationUser>(new MySqlUserStore<ApplicationUser>("DefaultConnection"));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin" || s[0].ToString() == "User" || s[0].ToString() == "Publisher")
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