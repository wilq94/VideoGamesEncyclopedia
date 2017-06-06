using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System.Web;
using VideoGamesEncyclopedia.Models;
using Microsoft.AspNet.Identity;
using MySql.AspNet.Identity;

namespace VideoGamesEncyclopedia.Extensions
{
    public static class RolesExtensions
    {
        public static string GetRoleName(string name)
        {
            string roleName = "";
            using (VideoGamesEncyclopediaDbEntities db = new VideoGamesEncyclopediaDbEntities())
            {
                var userId = db.aspnetusers.FirstOrDefault(u => u.UserName == name).Id;
                var UserManager = new UserManager<ApplicationUser>(new MySqlUserStore<ApplicationUser>("DefaultConnection"));
                roleName = UserManager.GetRoles(userId)[0].ToString();
            }
            return roleName;
        }
    }
}