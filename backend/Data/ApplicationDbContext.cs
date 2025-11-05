using Microsoft.EntityFrameworkCore;

namespace Veeb_TARpv23.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
