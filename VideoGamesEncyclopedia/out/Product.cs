using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoGamesEncyclopedia.Models
{
    public class Product
    {
        [Key]

        int Id { get; set; }
        string Name { get; set; }
        string CoverPath { get; set; }
        string PremiereDate { get; set; }
        string CreatedBy { get; set; }
        string Description { get; set; }
        float Rating { get; set; }

        public virtual ICollection<ProductScreenshot> ProductScreenshots { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual ICollection<IgnoredProduct> IgnoredProducts { get; set; }
        public virtual ICollection<WishedProduct> WishedProducts { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}