using Microsoft.EntityFrameworkCore;
using Veeb_TARpv23.Models;

namespace Veeb_TARpv23.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
