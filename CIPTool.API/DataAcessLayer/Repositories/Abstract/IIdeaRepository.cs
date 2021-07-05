using BusinessObjectLayer.Entities;
using System.Threading.Tasks;

namespace DataAcessLayer.Repositories.Abstract
{
    public interface IIdeaRepository : IRepository<IdeaEntity>
    {
        Task AddLeaderResponse(IdeaEntity ideaToUpdate, LeaderResponse leaderResponse);
    }
}
