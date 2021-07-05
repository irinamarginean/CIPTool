using BusinessObjectLayer.Entities;
using DataAcessLayer.Repositories.Abstract;

namespace DataAcessLayer.Repositories
{
    public sealed class BonusCorrectionFactorRepository : BaseRepository<BonusCorrectionFactorEntity>, IBonusCorrectionFactorRepository
    {
        public BonusCorrectionFactorRepository(CIPToolContext dataContext) : base(dataContext) { }
    }
}
