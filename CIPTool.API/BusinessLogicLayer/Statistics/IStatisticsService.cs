using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Statistics
{
    public interface IStatisticsService
    {
        Task<IdeaStatisticsDto> GetIdeaStatisticsDto();
        int GetAllIdeasNumber(ICollection<IdeaEntity> allIdeas);
        int GetWaitingForApprovalIdeasNumber(ICollection<IdeaEntity> allIdeas);
        int GetApprovedIdeasNumber(ICollection<IdeaEntity> allIdeas);
        int GetPostponedIdeasNumber(ICollection<IdeaEntity> allIdeas);
        int GetDeclinedIdeasNumber(ICollection<IdeaEntity> allIdeas);
        int GetImplementedIdeasNumber(ICollection<IdeaEntity> allIdeas);
        double GetOtdDecisionValue(ICollection<IdeaEntity> allIdeas);
        int GetOtdDecisionGreenCategoryNumber(ICollection<IdeaEntity> allIdeas);
        int GetOtdDecisionYellowCategoryNumber(ICollection<IdeaEntity> allIdeas);
        int GetOtdDecisionRedCategoryNumber(ICollection<IdeaEntity> allIdeas);
        double GetOtdImplementationValue(ICollection<IdeaEntity> allIdeas);
        int GetOtdImplementationGreenCategoryNumber(ICollection<IdeaEntity> allIdeas);
        int GetOtdImplementationYellowCategoryNumber(ICollection<IdeaEntity> allIdeas);
        int GetOtdImplementationRedCategoryNumber(ICollection<IdeaEntity> allIdeas);
        FinancialStatisticsDto GetSavingsStatistics(ICollection<IdeaEntity> allIdeas);
        FinancialStatisticsDto GetExpensesStatistics(ICollection<IdeaEntity> allIdeas);
        FinancialStatisticsDto GetBalanceStatistics(ICollection<IdeaEntity> allIdeas);
        FinancialStatisticsDto GetBonusesStatistics(ICollection<IdeaEntity> allIdeas);
        int GetFinancialBenefitsIdeasNumber(ICollection<IdeaEntity> allIdeas);
        int GetNoFinancialBenefitsIdeasNumber(ICollection<IdeaEntity> allIdeas);
        public decimal GetTotalMoneySaved(ICollection<IdeaEntity> allIdeas);
        public decimal GetTotalBonuses(ICollection<IdeaEntity> allIdeas);
    }
}
