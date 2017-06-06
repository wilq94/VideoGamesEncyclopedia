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
        public class UsersViewModel
        {
            public UsersViewModel(List<UsersWithRoles> userList)
            {
                UserList = userList;
            }
            public List<UsersWithRoles> UserList { get; set; }
        }
        public class CategoriesListViewModel
        {
            string Name { get; set; }
            DateTime DateOfAdding { get; set; }
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