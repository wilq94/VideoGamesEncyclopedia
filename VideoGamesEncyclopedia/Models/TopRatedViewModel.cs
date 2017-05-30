using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoGamesEncyclopedia.Models
{
    public class TopRatedViewModel
    {
        public TopRatedViewModel(product topRated)
        {
            ProductId = topRated.Id;
            Title = topRated.Name;
            ImageUrl = topRated.CoverPath;
            Rating = topRated.Rating;
        }

        public int ProductId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public float? Rating { get; set; }
    }
}