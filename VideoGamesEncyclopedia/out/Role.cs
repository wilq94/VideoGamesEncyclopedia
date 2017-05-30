using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoGamesEncyclopedia.Models
{
    public class Role
    {
        [Key]

        int Id { get; set; }
        string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}