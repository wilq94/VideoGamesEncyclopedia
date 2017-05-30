using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoGamesEncyclopedia.Models
{
    public class Recommendation
    {
        [Key]

        int Id { get; set; }
        int UserId { get; set; }
        int ProductId { get; set; }
        float RecommendationRating { get; set; }
        bool WasPresented { get; set; }
        DateTime GenerationDate { get; set; }

        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}