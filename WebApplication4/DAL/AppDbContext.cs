using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options):base(options)
        {

        }
    public    DbSet<Slider> sliders { get; set; }
    }
}
