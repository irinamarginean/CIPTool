using BusinessObjectLayer.Entities;

namespace DataAcessLayer.Repositories
{
    public sealed class BonusRepository : BaseRepository<BonusEntity>
    {
        public BonusRepository(CIPToolContext dataContext) : base(dataContext) { }
    }
}
