using BusinessObjectLayer.Entities;
using DataAcessLayer.Repositories.Abstract;

namespace DataAcessLayer.Repositories
{
    public sealed class BonusRepository : BaseRepository<BonusEntity>, IBonusRepository
    {
        public BonusRepository(CIPToolContext dataContext) : base(dataContext) { }
    }
}
