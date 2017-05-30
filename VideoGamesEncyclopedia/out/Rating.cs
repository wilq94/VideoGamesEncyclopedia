﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoGamesEncyclopedia.Models
{
    public class Rating
    {
        [Key]

        int Id { get; set; }
        int UserId { get; set; }
        int ProductId { get; set; }
        float Rate { get; set; }

        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}