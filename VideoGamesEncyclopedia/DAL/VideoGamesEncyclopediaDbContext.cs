using VideoGamesEncyclopedia.Models;
using System.Data.Entity;

namespace VideoGamesEncyclopedia.DAL
{
    public class VideoGamesEncyclopediaDbContext : DbContext
    {
        public VideoGamesEncyclopediaDbContext()
            : base("VideoGamesEncyclopediaConnection")
        {
        }

        public static VideoGamesEncyclopediaDbContext Create()
        {
            return new VideoGamesEncyclopediaDbContext();
        }
        /*

        public DbSet<Category> Categories { get; set; }
        public DbSet<IgnoredProduct> IgnoredProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductScreenshot> ProductScreenshots { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WishedProduct> WishedProducts { get; set; }*/
    }
}