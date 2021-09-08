using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Services.SiteSelection.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class UserRepository : EfEntityRepositoryBase<User, ProjectDbContext>, IUserRepository
    {
        private readonly MsDbContext_Setra setraContext;
        private readonly MsDbContext_Ultra ultraContext;
        private readonly MsDbContext_Betkolik betkolikContext;
        private readonly ISiteSelector siteSelector;

        public UserRepository(
            ProjectDbContext context,
            MsDbContext_Setra setraContext ,
            MsDbContext_Ultra ultraContext , 
            MsDbContext_Betkolik betkolikContext,
            ISiteSelector siteSelector

           )
            : base(context)
        {
            this.setraContext = setraContext;
            this.ultraContext = ultraContext;
            this.betkolikContext = betkolikContext;
            this.siteSelector = siteSelector;

            base.SetContext(siteSelector.GetCurrentContext());

        }

        public List<OperationClaim> GetClaims(int userId)
        {
            var result = (from user in Context.Users
                          join userGroup in Context.UserGroups on user.UserId equals userGroup.UserId
                          join groupClaim in Context.GroupClaims on userGroup.GroupId equals groupClaim.GroupId
                          join operationClaim in Context.OperationClaims on groupClaim.ClaimId equals operationClaim.Id
                          where user.UserId == userId
                          select new
                          {
                              operationClaim.Name
                          }).
                                        Union(from user in Context.Users
                                              join userClaim in Context.UserClaims on user.UserId equals userClaim.UserId
                                              join operationClaim in Context.OperationClaims on userClaim.ClaimId equals operationClaim.Id
                                              where user.UserId == userId
                                              select new
                                              {
                                                  operationClaim.Name
                                              });

            return result.Select(x => new OperationClaim { Name = x.Name }).Distinct()
                    .ToList();
        }

        public void UseDb(string siteName)
        {
            if (siteName == Constants.SiteNames.Setra)
            {
                base.SetContext(setraContext);
            }
            else if (siteName == Constants.SiteNames.Ultra)
            {
                base.SetContext(ultraContext);
            }
            else if (siteName == Constants.SiteNames.Betkolik)
            {
                base.SetContext(betkolikContext);
            }
        }
    }
}