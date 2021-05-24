using BusinessObjectLayer;
using BusinessObjectLayer.Entities;
using DataAcessLayer.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLayer.FinancialReports
{
    public class FinancialReportService : IFinancialReportService
    {
        private readonly BaseRepository<FinancialReportEntity> financialReportRepository;
        private readonly BaseRepository<BonusRangeEntity> bonusRangeRepository;
        private readonly BaseRepository<BonusCorrectionFactorEntity> bonusCorrectionFactorRepository;

        public FinancialReportService(
            BaseRepository<FinancialReportEntity> financialReportRepository, 
            BaseRepository<BonusRangeEntity> bonusRangeRepository,
            BaseRepository<BonusCorrectionFactorEntity> bonusCorrectionFactorRepository)
        {
            this.financialReportRepository = financialReportRepository;
            this.bonusRangeRepository = bonusRangeRepository;
            this.bonusCorrectionFactorRepository = bonusCorrectionFactorRepository;
        }

        public async Task<FinancialReportEntity> GetFinancialReportByIdea(string ideaId)
        {
            var allIdeas = await financialReportRepository.GetAll();

            return allIdeas.FirstOrDefault(x => x.IdeaId.ToString() == ideaId);
        }

        public async Task AddFinancialReport(FinancialReportEntity financialReport)
        {
            await financialReportRepository.Insert(financialReport);
        }

        public async Task UpdateFinancialReport(FinancialReportEntity financialReportToUpdate)
        {
            await financialReportRepository.Update(financialReportToUpdate);
        }

        public async Task DeleteFinancialReport(FinancialReportEntity financialReportToDelete)
        {
            await financialReportRepository.Delete(financialReportToDelete);
        }

        public async Task<BonusEntity> GenerateBonus(IdeaEntity idea)
        {
            var financialReport = idea.FinancialReport;
            var bonusRange = await GetIdeaBonusRange(idea);
            var bonusCorrectionFactors = await bonusCorrectionFactorRepository.GetAll();
            var bonusCorrectionFactor = financialReport.Bonus?.BonusCorrectionFactor ?? bonusCorrectionFactors.FirstOrDefault(x => x.CorrectionFactor == 1.0m);
            var bonusAward = bonusRange.Award * bonusCorrectionFactor.CorrectionFactor;

            return new BonusEntity
            {
                Id = Guid.NewGuid(),
                FinancialReport = financialReport,
                FinancialReportId = financialReport.Id,
                BonusCorrectionFactor = bonusCorrectionFactor,
                BonusCorrectionFactorId = bonusCorrectionFactor.Id,
                BonusRange = bonusRange,                
                BonusRangeId = bonusRange.Id,
                Bonus = bonusAward
            };
        }

        private async Task<BonusRangeEntity> GetIdeaBonusRange(IdeaEntity idea)
        {
            var financialReport = idea.FinancialReport;
            var bonusBalance = GetBonusBalance(idea);
              
            var bonuseRanges = await bonusRangeRepository.GetAll();
            BonusRangeEntity bonusRange = null; 

            bonusRange = bonuseRanges.FirstOrDefault(x => x.LowerBound <= bonusBalance && x.UpperBound > bonusBalance);

            return bonusRange;
        }

        private decimal GetBonusBalance(IdeaEntity idea)
        {
            var financialReport = idea.FinancialReport;

            return financialReport.ActualSavings - financialReport.ActualExpenses;
        }
    }
}
