using BusinessObjectLayer.Entities;

namespace DataAcessLayer.Repositories
{
    public sealed class BonusRangeRepository : BaseRepository<BonusRangeEntity>
    {
        public BonusRangeRepository(CIPToolContext dataContext) : base(dataContext) { }
    }
}
