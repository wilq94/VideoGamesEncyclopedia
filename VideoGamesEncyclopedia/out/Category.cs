using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;

namespace VideoGamesEncyclopedia.Models
{
    public class Category
    {
        [Key]
        int Id { get; set; }
        string Name { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}