using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using VideoGamesEncyclopedia.Models;
using MySql.AspNet.Identity;

[assembly: OwinStartupAttribute(typeof(VideoGamesEncyclopedia.Startup))]
namespace VideoGamesEncyclopedia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesAndUsers();
        }

        // In this method we will create default User roles and Admin user for login   
        private void createRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new MySqlRoleStore<IdentityRole>("DefaultConnection"));
            var UserManager = new UserManager<ApplicationUser>(new MySqlUserStore<ApplicationUser>("DefaultConnection"));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {
                // first we create Admin rool   
                var role = new IdentityRole();
                role.Id = "1";
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  
                /*
                var user = new ApplicationUser();
                user.UserName = "shanu";
                user.Email = "syedshanumcain@gmail.com";

                string userPWD = "A@Z200711";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }*/
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Publisher"))
            {
                var role = new IdentityRole();
                role.Id = "2";
                role.Name = "Publisher";
                roleManager.Create(role);

            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Id = "3";
                role.Name = "User";
                roleManager.Create(role);
            }
        }
    }
}
