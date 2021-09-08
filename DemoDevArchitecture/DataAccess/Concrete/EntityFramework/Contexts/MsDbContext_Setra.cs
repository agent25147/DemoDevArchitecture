using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    /// <summary>
    /// Uses Setra Database
    /// </summary>
    public class MsDbContext_Setra : ProjectDbContext
    {
        public MsDbContext_Setra(DbContextOptions<MsDbContext_Setra> options, IConfiguration configuration) : base(options, configuration)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseSqlServer("data source=NOOBMASTER69;initial catalog=SetraDb;persist security info=False;user id=sa;password=efmukl"));
            }
        }
    }
}
