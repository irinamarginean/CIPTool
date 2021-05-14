using BusinessObjectLayer.Entities;

namespace DataAcessLayer.Repositories
{
    public sealed class BonusCorrectionFactorRepository : BaseRepository<BonusCorrectionFactorEntity>
    {
        public BonusCorrectionFactorRepository(CIPToolContext dataContext) : base(dataContext) { }
    }
}
