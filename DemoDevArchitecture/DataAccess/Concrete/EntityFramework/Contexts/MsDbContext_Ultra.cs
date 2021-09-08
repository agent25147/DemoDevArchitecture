using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    /// <summary>
    /// Uses Ultra Database
    /// </summary>
    public class MsDbContext_Ultra : ProjectDbContext
    {
        public MsDbContext_Ultra(DbContextOptions<MsDbContext_Ultra> options, IConfiguration configuration) : base(options, configuration)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseSqlServer("data source=NOOBMASTER69;initial catalog=TetraDb;persist security info=False;user id=sa;password=efmukl"));
            }
        }
    }
}
