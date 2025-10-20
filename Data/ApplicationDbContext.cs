using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PojisteniWebApp.Models;

namespace PojisteniWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PojisteniWebApp.Models.Client> Client { get; set; } = default!;
        public DbSet<PojisteniWebApp.Models.Insurance> Insurance { get; set; } = default!;
    }
}
