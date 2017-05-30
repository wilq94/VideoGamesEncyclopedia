﻿using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VideoGamesEncyclopedia.Models;

namespace VideoGamesEncyclopedia.Controllers
{
    public class HomeController : Controller
    {
        private VideoGamesEncyclopediaDbEntities db = new VideoGamesEncyclopediaDbEntities();

        public ActionResult Index(string searchString)
        {
            //dodac pole "data dodania do bazy"
            var recentlyAdded = db.products.Take(2).ToList();
            var topRated = db.products.OrderByDescending(x => x.Rating).Take(4).ToList();
            var view = new MainPageViewModels(recentlyAdded, topRated);

            if (!string.IsNullOrEmpty(searchString))
            {
                return RedirectToAction("Index", "product", searchString);
            }

            return View(view);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(MainPageViewModels model, string returnUrl)
        {
            return RedirectToAction("Login", "Account", model.LoginViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}