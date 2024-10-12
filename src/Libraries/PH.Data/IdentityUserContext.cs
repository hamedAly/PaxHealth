using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PH.Data;

public class IdentityUserContext : IdentityDbContext<IdentityUser>
{
    public IdentityUserContext(DbContextOptions<IdentityUserContext> options) 
        : base(options)
    {
      
    }
}   