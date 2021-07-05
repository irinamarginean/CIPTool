using BusinessObjectLayer.Entities;
using DataAcessLayer.Repositories.Abstract;

namespace DataAcessLayer.Repositories
{
    public sealed class FinancialReportRepository : BaseRepository<FinancialReportEntity>, IFinancialReportRepository
    {
        public FinancialReportRepository(CIPToolContext dataContext) : base(dataContext) { }
    }
}
