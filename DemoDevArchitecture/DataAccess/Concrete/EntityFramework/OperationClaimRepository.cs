using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Services.SiteSelection.Interfaces;

namespace DataAccess.Concrete.EntityFramework
{
    public class OperationClaimRepository : EfEntityRepositoryBase<OperationClaim, ProjectDbContext>, IOperationClaimRepository
    {
        public OperationClaimRepository(ProjectDbContext context , ISiteSelector siteSelector)
            : base(context)
        {
            base.SetContext(siteSelector.GetCurrentContext());
        }
    }
}
