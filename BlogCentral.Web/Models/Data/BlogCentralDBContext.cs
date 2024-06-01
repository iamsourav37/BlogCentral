using BlogCentral.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogCentral.Web.Models.Data
{
    public class BlogCentralDBContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
