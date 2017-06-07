using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VideoGamesEncyclopedia.Models
{
    public class AdminPanelViewModels
    {
        public class SiteStatisticsViewModel
        {
            public SiteStatisticsViewModel(string date, int usersCounter, int gamesCounter, int ticketsCounter)
            {
                CreationDate = date;
                RegisteredUsers = usersCounter;
                GamesCounter = gamesCounter;
                UnsolvedTickets = ticketsCounter;
            }

            public string CreationDate { get; set; }
            public int RegisteredUsers { get; set; }
            public int GamesCounter { get; set; }
            public int UnsolvedTickets { get; set; }
        }

        public enum UserRoles
        {
            User,
            Publisher,
            Admin
        }

        public class UsersViewModel
        {
            public UsersViewModel(List<UsersWithRoles> userList)
            {
                UserList = userList;
            }

            public UsersViewModel()
            {
            }

            public List<UsersWithRoles> UserList { get; set; }
        }

        public class CategoriesListViewModel
        {
            public CategoriesListViewModel(List<category> toList, List<productcategory> productCategoryList)
            {
                CategoryList = toList;
                ProductCategoryList = productCategoryList;
            }

            public List<category> CategoryList { get; set; }
            public List<productcategory> ProductCategoryList { get; set; }
        }
        public class TicketsViewModel
        {
            //
        }
        public class SettingsViewModel
        {
            //
        }
    }
}