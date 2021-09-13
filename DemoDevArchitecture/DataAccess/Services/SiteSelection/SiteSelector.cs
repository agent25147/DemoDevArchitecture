
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Constants;
using DataAccess.Services.SiteSelection.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Services.SiteSelection
{
    public class SiteSelector : ISiteSelector
    {
        private readonly MsDbContext_Setra setraContext;
        private readonly MsDbContext_Ultra ultraContext;
        private readonly MsDbContext_Betkolik betkolikContext;

        // by default its setra
        public SiteSelector(MsDbContext_Setra setraContext,
            MsDbContext_Ultra ultraContext,
            MsDbContext_Betkolik betkolikContext)
        {
            this.setraContext = setraContext;
            this.ultraContext = ultraContext;
            this.betkolikContext = betkolikContext;
        }

        public ProjectDbContext GetCurrentContext()
        {
            if (SelectedSite == SiteNames.Ultra)
            {
                return ultraContext;
            }
            else if (SelectedSite == SiteNames.Betkolik)
            {
                return betkolikContext;
            }
            else
            {
                return setraContext;
            }
           
        }

        public string SelectedSite { get; set; } = SiteNames.Setra; 

       
    }
}
