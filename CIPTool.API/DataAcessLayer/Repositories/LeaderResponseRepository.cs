using BusinessObjectLayer.Entities;

namespace DataAcessLayer.Repositories
{
    public class LeaderResponseRepository : BaseRepository<LeaderResponse>
    {
        public LeaderResponseRepository(CIPToolContext dataContext) : base(dataContext) { }
    }
}
