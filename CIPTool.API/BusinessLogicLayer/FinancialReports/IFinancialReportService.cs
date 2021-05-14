using BusinessObjectLayer.Entities;
using System.Threading.Tasks;

namespace BusinessLogicLayer.FinancialReports
{
    public interface IFinancialReportService
    {
        Task<FinancialReportEntity> GetFinancialReportByIdea(string ideaId);
        Task AddFinancialReport(FinancialReportEntity financialReport);
        Task UpdateFinancialReport(FinancialReportEntity financialReportToUpdate);
        Task DeleteFinancialReport(FinancialReportEntity financialReportToDelete);
        Task<BonusEntity> GenerateBonus(IdeaEntity idea);
    }
}
