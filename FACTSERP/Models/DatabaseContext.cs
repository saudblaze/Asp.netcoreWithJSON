using Microsoft.EntityFrameworkCore;

namespace FACTSERP.Models
{
    public partial class DatabaseContext  : DbContext
    {
        public DatabaseContext() 
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<products> products { get; set; }
        public DbSet<stockIns> stockIns { get; set; }
        public DbSet<stockOuts> stockOuts { get; set; }
    }
}
