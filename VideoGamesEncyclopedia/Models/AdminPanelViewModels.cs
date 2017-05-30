using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoGamesEncyclopedia.Models
{
    public class AdminPanelViewModels
    {
        class SiteStatisticsViewModel
        {
            DateTime CreationDate { get; set; }
            int RegisteredUsers { get; set; }
            int DailyVisitCounter { get; set; }
            int GamesCounter { get; set; }
            int UnsolvedTickets { get; set; }
        }
        class UsersViewModel
        {
            string EmailAddress { get; set; }
            string UserName { get; set; }
            string Role { get; set; }
        }
        class CategoriesListViewModel
        {
            string Name { get; set; }
            DateTime DateOfAdding { get; set; }
        }
        class TicketsViewModel
        {
            //
        }
        class SettingsViewModel
        {
            //
        }
    }
}