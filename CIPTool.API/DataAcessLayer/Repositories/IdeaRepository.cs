using BusinessObjectLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace DataAcessLayer.Repositories
{
    public sealed class IdeaRepository : BaseRepository<IdeaEntity>
    {
        private readonly CIPToolContext dataContext;

        public IdeaRepository(CIPToolContext dataContext) : base(dataContext) 
        {
            this.dataContext = dataContext;
        }

        public override Task<List<IdeaEntity>> GetAll()
        {
            return dataContext.Ideas
                .Include(x => x.Associate)
                    .ThenInclude(x => x.Leader)
                .Include(x => x.FinancialReport)
                    .ThenInclude(x => x.Bonus)
                .Include(x => x.Categories)
                .Include(x => x.Attachments)
                .Include(x => x.LeaderResponses)
                .ToListAsync();
        }

        public async Task AddLeaderResponse(IdeaEntity ideaToUpdate, LeaderResponse leaderResponse)
        {
            //var idea = dataContext.Ideas.Include(x => x.LeaderResponses).Where(x => x.Id == ideaToUpdate.Id).FirstOrDefault();
            var reviewer = dataContext.Users.Where(x => x.UserName == leaderResponse.ReviewerId).FirstOrDefault() as Associate;

            dataContext.Entry(ideaToUpdate.Associate).State = EntityState.Unchanged;
            dataContext.Entry(ideaToUpdate.Reviewer).State = EntityState.Unchanged;
            ideaToUpdate.Status = leaderResponse.Response;
            dataContext.Ideas.Update(ideaToUpdate);
            
            leaderResponse.Reviewer = reviewer;
            dataContext.Entry(leaderResponse.Reviewer).State = EntityState.Unchanged;
            dataContext.LeaderResponses.Add(leaderResponse);

            await dataContext.SaveChangesAsync();
        }
    }
}
