using BusinessObjectLayer.Entities;
using DataAcessLayer.Repositories.Abstract;

namespace DataAcessLayer.Repositories
{
    public class LeaderResponseRepository : BaseRepository<LeaderResponse>, ILeaderResponseRepository
    {
        public LeaderResponseRepository(CIPToolContext dataContext) : base(dataContext) { }
    }
}
