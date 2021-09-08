using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    /// <summary>
    /// Uses Betkolik database
    /// </summary>
    public class MsDbContext_Betkolik : ProjectDbContext
    {
        public MsDbContext_Betkolik(DbContextOptions<MsDbContext_Betkolik> options, IConfiguration configuration) : base(options, configuration)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseSqlServer("data source=NOOBMASTER69;initial catalog=BetkolikDb;persist security info=False;user id=sa;password=efmukl"));
            }
        }
    }
}
