using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoGamesEncyclopedia.Models
{
    public class MainPageViewModels
    {
        public MainPageViewModels(List<product> recentlyAdded, List<product> topRated)
        {
            RecentlyAdded = new List<RecentlyAddedViewModel>();
            TopRated = new List<TopRatedViewModel>();
            LoginViewModel = new LoginViewModel();

            foreach (var item in recentlyAdded)
            {
                RecentlyAdded.Add(new RecentlyAddedViewModel(item));
            }

            foreach (var item in topRated)
            {
                TopRated.Add(new TopRatedViewModel(item));
            }
        }

        public List<RecentlyAddedViewModel> RecentlyAdded { get; set; }
        public List<TopRatedViewModel> TopRated { get; set; }
        public LoginViewModel LoginViewModel { get; set; }
    }
}