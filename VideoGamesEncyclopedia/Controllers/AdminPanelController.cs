using System;
using System.Linq;
using System.Web.Mvc;
using VideoGamesEncyclopedia.Models;
using MySql.AspNet.Identity;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Web;
using System.Data;

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
            return View();
        }

        //Name=&CreatedBy=&PremiereDate=&Description=&file=

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGame([Bind(Include = "Name,CoverPath,PremiereDate,CreatedBy,Description,Rating,CreationDate")] product product,
            HttpPostedFileBase coverFile, HttpPostedFileBase screenFile, HttpPostedFileBase screenFile2, HttpPostedFileBase screenFile3)
        {
            if (isAdmin() || isPublisher())
            {
                try
                {
                    if (ModelState.IsValid)
                    {   
                        var lastId = db.products.OrderByDescending(x => x.Id).First().Id;
                        product.Id = lastId + 1;
                        if (coverFile != null)
                        {
                            product.CoverPath = product.Name + ".jpg";
                            string path = Path.Combine(Server.MapPath("~/img/covers"), product.Name + ".jpg");
                            // file is uploaded
                            coverFile.SaveAs(path);
                        }
                        db.products.Add(product);
                        db.SaveChanges();
                        if (screenFile != null)
                        {
                            ScreenUpload(screenFile);
                        }
                        if (screenFile2 != null)
                        {
                            ScreenUpload(screenFile2);
                        }
                        if (screenFile3 != null)
                        {
                            ScreenUpload(screenFile3);
                        }
                        return RedirectToAction("Games", "AdminPanel");
                    }
                }
                catch (DataException)
                {
                    //do edycji
                    ModelState.AddModelError("", "Nie mozna zapisac wprowadzonych danych. Sprobuj ponownie.");
                    return View("Error");
                }
            }
            else
            {
                return View("Error");
            }
            return View();
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

        public void ScreenUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                //string pic = Path.GetFileName(file.FileName);

                using(VideoGamesEncyclopediaDbEntities database = new VideoGamesEncyclopediaDbEntities()) { 

                    var lastProductScreenshotId = database.productscreenshots.OrderByDescending(x => x.Id).First().Id;
                    var newProductScreenshot = new productscreenshot();
                    newProductScreenshot.Id = lastProductScreenshotId + 1;
                    var lastProductId = database.products.OrderByDescending(x => x.Id).First().Id;
                    newProductScreenshot.ProductId = lastProductId;
                    var idToString = newProductScreenshot.Id.ToString();
                    newProductScreenshot.ScreenshotPath = idToString  + ".jpg";
                    database.productscreenshots.Add(newProductScreenshot);
                    database.SaveChanges();
                    string path = Path.Combine(Server.MapPath("~/img/screens"), lastProductScreenshotId+1 + ".jpg");
                    // file is uploaded
                    file.SaveAs(path);
                }
            }
        }

    }
}