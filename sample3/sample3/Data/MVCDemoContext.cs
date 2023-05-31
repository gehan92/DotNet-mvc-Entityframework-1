using Microsoft.EntityFrameworkCore;
using sample3.Models.Domain;

namespace sample3.Data
{
    public class MVCDemoContext : DbContext
    {
        public MVCDemoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; }

        
    }
}
