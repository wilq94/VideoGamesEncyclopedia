using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoGamesEncyclopedia.Models
{
    public class ProductScreenshot
    {
        [Key]

        int Id { get; set; }
        int ProductId { get; set; }
        string ScreenshotPath { get; set; }

        public virtual Product Product { get; set; }
    }
}