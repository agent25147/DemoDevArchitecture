using DataAccess.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Services.SiteSelection.Interfaces
{
    public interface ISiteSelector
    {
        public string SelectedSite { get; set; }
        ProjectDbContext GetCurrentContext();
    }
}
