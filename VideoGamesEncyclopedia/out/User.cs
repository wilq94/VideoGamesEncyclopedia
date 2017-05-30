using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoGamesEncyclopedia.Models
{
    public class User
    {
        [Key]

        int Id { get; set; }
        int RoleId { get; set; }
        string Nickname { get; set; }
        string PasswordHash { get; set; }
        DateTime CreatedAt { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<IgnoredProduct> IgnoredProducts { get; set; }
        public virtual ICollection<WishedProduct> WishedProducts { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}