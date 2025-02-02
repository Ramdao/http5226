using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Passion_Project.Models;
namespace Passion_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Users> users { get; set; }
        public DbSet<Timeline> timelines { get; set; }

        public DbSet<Entries> entries { get; set; }

        public DbSet<UserTimeline> UsersTimeline { get; set; }

        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
