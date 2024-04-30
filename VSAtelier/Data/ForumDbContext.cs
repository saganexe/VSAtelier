using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VSAtelier.Models;
using VSAtelier.Models.Entities;

namespace VSAtelier.Data
{
    public class ForumDbContext: DbContext
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {
        }
        public DbSet<Forum> Forums { get; set; }
    }
}
