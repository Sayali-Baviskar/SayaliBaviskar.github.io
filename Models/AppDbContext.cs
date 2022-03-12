using Microsoft.EntityFrameworkCore;

namespace MVC_UI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ComponentProcessing> componentProcessings { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<ProcessResponse> processResponses { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
