using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VSAtelier.Models.Entities;

namespace VSAtelier.Data
{
    public class AuthDbContext : IdentityDbContext<User>
    {
            public AuthDbContext (DbContextOptions<AuthDbContext> options) : base(options) 
        { 
        }
        
    }
}
