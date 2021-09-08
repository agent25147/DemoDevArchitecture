using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Services.SiteSelection.Interfaces;

namespace DataAccess.Concrete.EntityFramework
{
    public class GroupRepository : EfEntityRepositoryBase<Group, ProjectDbContext>, IGroupRepository
    {
        public GroupRepository(ProjectDbContext context, ISiteSelector siteSelector)
            : base(context)
        {
            base.SetContext(siteSelector.GetCurrentContext());
        }
    }
}
