using System;
using System.Linq;
using System.Web.Mvc;
using VideoGamesEncyclopedia.Models;
using MySql.AspNet.Identity;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Web;
using System.Data;
using static VideoGamesEncyclopedia.Models.AdminPanelViewModels;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoGamesEncyclopedia.Models;

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
                var usersCounter = db.aspnetusers.Count();
                var gamesCounter = db.products.Count();
                var ticketsCounter = db.tickets.Where(t => t.IsFinished == 0).Count();
                SiteStatisticsViewModel indexViewModel = new SiteStatisticsViewModel("17.05.2017", usersCounter, gamesCounter, ticketsCounter);

                return View(indexViewModel);
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
                        product.CreationDate = DateTime.Now;
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

        public ActionResult AddNewCategory()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewCategory([Bind(Include = "Name")] category category)
        {
            if (isAdmin() || isPublisher())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var lastId = db.categories.OrderByDescending(x => x.Id).First().Id;
                        category.Id = lastId + 1;
                        db.categories.Add(category);
                        db.SaveChanges();
                        
                        return RedirectToAction("Categories", "AdminPanel");
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

        public ActionResult EditGame(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGame([Bind(Include = "Name,CoverPath,PremiereDate,CreatedBy,Description,Rating,CreationDate")] product product,
    HttpPostedFileBase coverFile, HttpPostedFileBase screenFile, HttpPostedFileBase screenFile2, HttpPostedFileBase screenFile3, int id)
        {
            if (isAdmin() || isPublisher())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        /*var lastId = db.products.OrderByDescending(x => x.Id).First().Id;
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
                        }*/
                        product.Id = id;
                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();
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

        public ActionResult Games(string searchString)
        {
            if (isAdmin() || isPublisher())
            {
                IQueryable<product> products = db.products;

                if (!string.IsNullOrEmpty(searchString))
                {
                    products = db.products.Where(g => g.Name.ToString().Contains(searchString) || g.Description.Contains(searchString));
                }

                return View(products.ToList());
            }
            else
            {
                return View("Error");
            }
        }

        public List<UsersWithRoles> GetUsersWithRoles(string searchString)
        {
            var userList = new List<UsersWithRoles>();

            var allUsers = db.aspnetusers.ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                allUsers = db.aspnetusers.Where(g => g.UserName.Contains(searchString) || g.Email.Contains(searchString)).ToList();
            }

            foreach (var user in allUsers)
            {
                string role = "User";
                var UserManager = new UserManager<ApplicationUser>(new MySqlUserStore<ApplicationUser>("DefaultConnection"));
                var s = UserManager.GetRoles(user.Id);
                if(s.Count > 0)
                { 
                    if (s[0].ToString() == "Admin")
                    {
                        role = "Admin";
                    }
                    if (s[0].ToString() == "Publisher")
                    {
                        role = "Publisher";
                    }
                }
                var userWithRole = new UsersWithRoles (user.Email, user.UserName, role);
                userList.Add(userWithRole);
            }
            return userList;
        }

        public ActionResult DeleteGame(int? id)
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
        [HttpPost, ActionName("DeleteGame")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Games","AdminPanel");
        }

        public ActionResult EditUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aspnetuser aspnetuser = db.aspnetusers.Where(u => u.UserName == id).FirstOrDefault();
            var userList = new List<UsersWithRoles>();
            string role = "User";
            var UserManager = new UserManager<ApplicationUser>(new MySqlUserStore<ApplicationUser>("DefaultConnection"));
            var s = UserManager.GetRoles(aspnetuser.Id);
            if (s.Count > 0)
            {
                if (s[0].ToString() == "Admin")
                {
                    role = "Admin";
                }
                if (s[0].ToString() == "Publisher")
                {
                    role = "Publisher";
                }
            }
            var userWithRole = new UsersWithRoles(aspnetuser.Email, aspnetuser.UserName, role);
            userList.Add(userWithRole);
            if (aspnetuser == null)
            {
                return HttpNotFound();
            }
            UsersViewModel usersViewModel = new UsersViewModel(userList);

            return View(usersViewModel);
        }

        // POST: Aspnetuser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(UsersViewModel id, string role)
        {
            if (isAdmin() || isPublisher())
            {
                try
                {
                    //var aspnetuse
                        //userView.UserList.Id = id;
                        //db.Entry(product).State = EntityState.Modified;
                        //db.SaveChanges();
                        return RedirectToAction("Games", "AdminPanel");
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
        /*
        if (ModelState.IsValid)
        {

            db.Entry(aspnetuser).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(aspnetuser);
        
    }*/

        // GET: Aspnetuser/Delete/5
        public ActionResult DeleteUser(string id)
        {
            ViewBag.UserName = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aspnetuser aspnetuser = db.aspnetusers.Where(u => u.UserName == id).FirstOrDefault();
            var userList = new List<UsersWithRoles>();
            string role = "User";
            var UserManager = new UserManager<ApplicationUser>(new MySqlUserStore<ApplicationUser>("DefaultConnection"));
            var s = UserManager.GetRoles(aspnetuser.Id);
            if (s.Count > 0)
            {
                if (s[0].ToString() == "Admin")
                {
                    role = "Admin";
                }
                if (s[0].ToString() == "Publisher")
                {
                    role = "Publisher";
                }
            }
            var userWithRole = new UsersWithRoles(aspnetuser.Email, aspnetuser.UserName, role);
            userList.Add(userWithRole);
            if (aspnetuser == null)
            {
                return HttpNotFound();
            }
            UsersViewModel usersViewModel = new UsersViewModel(userList);

            return View(usersViewModel);
        }

        // POST: Aspnetuser/Delete/5
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            aspnetuser aspnetuser = db.aspnetusers.Where(u => u.UserName == id).FirstOrDefault();
            db.aspnetusers.Remove(aspnetuser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: product/Delete/5
        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategoryConfirmed(int id)
        {
            category category = db.categories.Find(id);
            db.categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Categories", "AdminPanel");
        }


        public ActionResult Users(string searchString)
        {
            if (isAdmin())
            {
                UsersViewModel usersViewModel = new UsersViewModel(GetUsersWithRoles(searchString));

                return View(usersViewModel);
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult Statistics()
        {
            if (isAdmin() || isPublisher())
            {
                var usersCounter = db.aspnetusers.Count();
                var gamesCounter = db.products.Count();
                var ticketsCounter = db.tickets.Where(t => t.IsFinished == 0).Count();
                SiteStatisticsViewModel indexViewModel = new SiteStatisticsViewModel("17.05.2017", usersCounter, gamesCounter, ticketsCounter);

                return View(indexViewModel);
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
                var tickets = db.tickets.Include(t => t.aspnetuser).Include(t => t.aspnetuser1);
                return View(tickets.ToList());
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult Categories(string searchString)
        {
            if (isAdmin() || isPublisher())
            {
                IQueryable<category> categories = db.categories;

                if (!string.IsNullOrEmpty(searchString))
                {
                    categories = db.categories.Where(g => g.Name.Contains(searchString));
                }

                return View(categories.ToList());
            }
            else
            {
                return View("Error");
            }
        }


        public ActionResult Categorize(int id, string searchString)
        {
            ViewBag.MyId = id;

            if (isAdmin() || isPublisher())
            {
                var categories = db.categories.Include(p => p.productcategories);

                if (!string.IsNullOrEmpty(searchString))
                {
                    categories = db.categories.Where(g => g.Name.ToString().Contains(searchString));
                }

                var oldCategories = db.productcategories.Where(p => p.ProductId == id).ToList();

                CategoriesListViewModel categoriesList = new CategoriesListViewModel(categories.ToList(), oldCategories); 

                return View(categoriesList);
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult AddCategory(int GameId, int CategoryId)
        {
            int lastId = 0;
            var lastRecord = db.productcategories.OrderByDescending(x => x.Id).First();
            if(lastRecord != null)
            {
                lastId = lastRecord.Id + 1;
            }
            var newProductCategory = new productcategory(lastId, GameId, CategoryId);
            db.productcategories.Add(newProductCategory);
            db.SaveChanges();

            return RedirectToAction("Categorize/"+GameId,"AdminPanel");
        }

        public ActionResult RemoveCategory(int RecordId, int GameId)
        {
            productcategory productcategory = db.productcategories.Find(RecordId);
            db.productcategories.Remove(productcategory);
            db.SaveChanges();

            return RedirectToAction("Categorize/" + GameId, "AdminPanel");
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