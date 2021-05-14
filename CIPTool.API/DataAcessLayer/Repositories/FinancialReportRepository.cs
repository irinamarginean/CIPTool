using BusinessObjectLayer.Entities;

namespace DataAcessLayer.Repositories
{
    public sealed class FinancialReportRepository : BaseRepository<FinancialReportEntity>
    {
        public FinancialReportRepository(CIPToolContext dataContext) : base(dataContext) { }
    }
}
