using blog.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace blog.Models
{
    public class BlogContext:IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public BlogContext(DbContextOptions<BlogContext> options):base(options)
        {

        }

        public DbSet<Blog> Blogs { get; set; }
    }
}
