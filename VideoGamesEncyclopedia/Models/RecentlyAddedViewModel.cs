using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoGamesEncyclopedia.Models
{
    public class RecentlyAddedViewModel
    {
        public RecentlyAddedViewModel(product recentlyAdded)
        {
            ProductId = recentlyAdded.Id;
                Title = recentlyAdded.Name;
                ImageUrl = recentlyAdded.CoverPath;
                Descritpion = recentlyAdded.Description;
            }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Descritpion { get; set; }
    }
}