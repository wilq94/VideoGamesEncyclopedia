using Microsoft.AspNet.Identity;
using MySql.AspNet.Identity;
using System;
using System.Web.Mvc;
using VideoGamesEncyclopedia.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace VideoGamesEncyclopedia.Controllers
{
    public class UserPanelController : Controller
    {
        // GET: UserPanel
        public ActionResult Index()
        {
            if (IsUser())
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
            if (IsUser())
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
            if (IsUser())
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
            if (IsUser())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public Boolean IsUser()
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
        public ActionResult GetWishlistedGames()
        {
            using (VideoGamesEncyclopediaDbEntities db = new VideoGamesEncyclopediaDbEntities())
            {
                var user = User.Identity;
                var UserManager = new UserManager<ApplicationUser>(new MySqlUserStore<ApplicationUser>("DefaultConnection"));
                var s = UserManager.GetLogins(user.GetUserId());

                if (s != null)
                    {
                    var pos = s[0].ProviderKey.LastIndexOf("/") + 1;
                    var steamId = s[0].ProviderKey.Substring(pos, s[0].ProviderKey.Length - pos);
                
                    var webGet = new HtmlWeb();

                    try {
                        HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://steamcommunity.com/profiles/" + steamId + "/wishlist");
                        HttpWebResponse myResp = (HttpWebResponse)req.GetResponse();

                        string new_print_url = myResp.ResponseUri.ToString();

                        HtmlDocument document = webGet.Load(new_print_url);

                        var gameNodes = document.DocumentNode.SelectNodes("//*[@class='ellipsis']");

                        foreach (var game in gameNodes)
                        {
                            var product = db.products.FirstOrDefault(g => g.Name == game.InnerHtml);
                            if (product != null)
                            {
                                var userId = user.GetUserId();

                                var wishlistedProduct = db.wishedproducts.Where(u => u.UserId == userId).FirstOrDefault(g => g.product.Name == game.InnerHtml);
                                if (wishlistedProduct == null)
                                {
                                    db.wishedproducts.Add(new wishedproduct (user.GetUserId(), product.Id));
                                }
                            }

                        }
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {

                    }
                }  
            }
            return RedirectToAction("WishList", "UserPanel");
        }

        public static string JsonDownloader(Uri uriAddress)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    var json = webClient.DownloadString(uriAddress);
                    return json;
                }
            }
            catch (WebException e)
            {
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        public class OwnedGames
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Playtime { get; set; }
            public string IconUrl { get; set; }
            public string LogoUrl { get; set; }
            public string CommunityVisibility { get; set; }
        }

    }
}