using BusinessObjectLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DataAcessLayer.Repositories.Abstract;

namespace DataAcessLayer.Repositories
{
    public sealed class IdeaRepository : BaseRepository<IdeaEntity>, IIdeaRepository
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
                .Include(x => x.Responsible)
                .Include(x => x.Reviewer)
                .Include(x => x.FinancialReport)
                    .ThenInclude(x => x.Bonus)
                .Include(x => x.Categories)
                .Include(x => x.Attachments)
                .Include(x => x.LeaderResponses)
                .ToListAsync();
        }

        public async Task AddLeaderResponse(IdeaEntity ideaToUpdate, LeaderResponse leaderResponse)
        {
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

        public async Task UpdateReviewer(IdeaEntity ideaToUpdate, string reviewerId)
        {
            var reviewer = dataContext.Users.Where(x => x.Id.ToLower() == reviewerId.ToLower()).FirstOrDefault() as Associate;

            dataContext.Entry(ideaToUpdate.Associate).State = EntityState.Unchanged;
            dataContext.Entry(ideaToUpdate.Reviewer).State = EntityState.Unchanged;
            ideaToUpdate.Reviewer = reviewer;
            dataContext.Ideas.Update(ideaToUpdate);

            await dataContext.SaveChangesAsync();
        }
    }
}
