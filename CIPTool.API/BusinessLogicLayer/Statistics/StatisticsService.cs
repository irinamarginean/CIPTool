using BusinessLogicLayer.Ideas;
using BusinessObjectLayer;
using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IIdeaService ideaService;

        public StatisticsService(IIdeaService ideaService)
        {
            this.ideaService = ideaService;
        }

        public async Task<IdeaStatisticsDto> GetIdeaStatisticsDto()
        {
            var allIdeas = await ideaService.GetAllIdeas();

            return new IdeaStatisticsDto
            {
                AllIdeasNumber = GetAllIdeasNumber(allIdeas),
                WaitingForApprovalIdeasNumber = GetWaitingForApprovalIdeasNumber(allIdeas),
                ApprovedIdeasNumber = GetApprovedIdeasNumber(allIdeas),
                PostponedIdeasNumber = GetPostponedIdeasNumber(allIdeas),
                DeclinedIdeasNumber = GetDeclinedIdeasNumber(allIdeas),
                ImplementedIdeasNumber = GetImplementedIdeasNumber(allIdeas),
                OtdDecisionValue = GetOtdDecisionValue(allIdeas),
                OtdDecisionGreenCategoryNumber = GetOtdDecisionGreenCategoryNumber(allIdeas),
                OtdDecisionYellowCategoryNumber = GetOtdDecisionYellowCategoryNumber(allIdeas),
                OtdDecisionRedCategoryNumber = GetOtdDecisionRedCategoryNumber(allIdeas),
                OtdImplementationValue = GetOtdImplementationValue(allIdeas),
                OtdImplementationGreenCategoryNumber = GetOtdImplementationGreenCategoryNumber(allIdeas),
                OtdImplementationYellowCategoryNumber = GetOtdImplementationYellowCategoryNumber(allIdeas),
                OtdImplementationRedCategoryNumber = GetOtdImplementationRedCategoryNumber(allIdeas),
                SavingsValues = GetSavingsStatistics(allIdeas),
                ExpensesValues = GetExpensesStatistics(allIdeas),
                BalanceValues = GetBalanceStatistics(allIdeas),
                BonusValues = GetBonusesStatistics(allIdeas),
                FinancialBenefitsIdeasNumber = GetFinancialBenefitsIdeasNumber(allIdeas),
                NoFinancialBenefitsIdeasNumber = GetNoFinancialBenefitsIdeasNumber(allIdeas),
                TotalMoneySaved = GetTotalMoneySaved(allIdeas),
                TotalBonuses = GetTotalBonuses(allIdeas)
            };
        }

        public int GetAllIdeasNumber(ICollection<IdeaEntity> allIdeas)
        {
            return allIdeas.Count;
        }

        public int GetWaitingForApprovalIdeasNumber(ICollection<IdeaEntity> allIdeas)
        {
            return allIdeas.Count(x => x.Status == ResponseStatus.WaitingForApprovalStatus);
        }

        public int GetApprovedIdeasNumber(ICollection<IdeaEntity> allIdeas)
        {
            return allIdeas.Count(x => x.Status == ResponseStatus.ApprovedStatus);
        }

        public int GetPostponedIdeasNumber(ICollection<IdeaEntity> allIdeas)
        {
            return allIdeas.Count(x => x.Status == ResponseStatus.PostponedStatus);
        }

        public int GetDeclinedIdeasNumber(ICollection<IdeaEntity> allIdeas)
        {
            return allIdeas.Count(x => x.Status == ResponseStatus.DeclinedStatus);
        }

        public int GetImplementedIdeasNumber(ICollection<IdeaEntity> allIdeas)
        {
            return allIdeas.Count(x => x.Status == ResponseStatus.ImplementedStatus);
        }

        public double GetOtdDecisionValue(ICollection<IdeaEntity> allIdeas)
        {
            var decisionGreenCategoryNumber = GetOtdDecisionGreenCategoryNumber(allIdeas);
            var decisionYellowCategoryNumber = GetOtdDecisionYellowCategoryNumber(allIdeas);
            var decisionRedCategoryNumber = GetOtdDecisionRedCategoryNumber(allIdeas);
            var allIdeasNumber = decisionGreenCategoryNumber + decisionYellowCategoryNumber + decisionRedCategoryNumber;
            var otd = (decisionGreenCategoryNumber * 100.0) / allIdeasNumber;

            return allIdeasNumber != 0 ? otd : 100;
        }

        public int GetOtdDecisionGreenCategoryNumber(ICollection<IdeaEntity> allIdeas)
        {
            return allIdeas
                .Where(x => x.Status != ResponseStatus.WaitingForApprovalStatus)
                .Count(x =>
                {
                    var leaderDecisionDate = x.LeaderResponses
                        .OrderBy(response => response.LeaderResponseDate)
                        .Select(response => response.LeaderResponseDate)
                        .FirstOrDefault();
                    var decisionDateDifference = (leaderDecisionDate - x.PlanDate)?.Days;

                    return decisionDateDifference < 10;
                });
        }

        public int GetOtdDecisionYellowCategoryNumber(ICollection<IdeaEntity> allIdeas)
        {
            return allIdeas
                .Where(x => x.Status != ResponseStatus.WaitingForApprovalStatus)
                .Count(x =>
                {
                    var leaderDecisionDate = x.LeaderResponses
                        .OrderBy(response => response.LeaderResponseDate)
                        .Select(response => response.LeaderResponseDate)
                        .FirstOrDefault();
                    var decisionDateDifference = (leaderDecisionDate - x.PlanDate)?.Days;

                    return decisionDateDifference >= 10 && decisionDateDifference < 20;
                });
        }

        public int GetOtdDecisionRedCategoryNumber(ICollection<IdeaEntity> allIdeas)
        {
            return allIdeas
                .Where(x => x.Status != ResponseStatus.WaitingForApprovalStatus)
                .Count(x =>
                {
                    var leaderDecisionDate = x.LeaderResponses
                        .OrderBy(response => response.LeaderResponseDate)
                        .Select(response => response.LeaderResponseDate)
                        .FirstOrDefault();
                    var decisionDateDifference = (leaderDecisionDate - x.PlanDate)?.Days;

                    return decisionDateDifference > 20;
                });
        }

        public double GetOtdImplementationValue(ICollection<IdeaEntity> allIdeas)
        {
            var implementationGreenCategoryNumber = GetOtdImplementationGreenCategoryNumber(allIdeas);
            var implementationYellowCategoryNumber = GetOtdImplementationYellowCategoryNumber(allIdeas);
            var implementationRedCategoryNumber = GetOtdImplementationRedCategoryNumber(allIdeas);
            var allIdeasNumber = implementationGreenCategoryNumber + implementationYellowCategoryNumber + implementationRedCategoryNumber;
            var otd = (implementationGreenCategoryNumber * 100.0) / allIdeasNumber;

            return allIdeasNumber != 0 ? otd : 100;
        }

        public int GetOtdImplementationGreenCategoryNumber(ICollection<IdeaEntity> allIdeas)
        {
            return allIdeas
                .Where(x => x.Status == ResponseStatus.ImplementedStatus)
                .Count(x => (x.ActDate - x.CheckDate)?.Days < 10);
        }

        public int GetOtdImplementationYellowCategoryNumber(ICollection<IdeaEntity> allIdeas)
        {
            return allIdeas
                .Where(x => x.Status == ResponseStatus.ImplementedStatus)
                .Count(x => (x.ActDate - x.CheckDate)?.Days >= 10 && (x.ActDate - x.CheckDate)?.Days < 10);
        }

        public int GetOtdImplementationRedCategoryNumber(ICollection<IdeaEntity> allIdeas)
        {
            return allIdeas
                .Where(x => x.Status == ResponseStatus.ImplementedStatus)
                .Count(x => (x.ActDate - x.CheckDate)?.Days > 20);
        }

        public FinancialStatisticsDto GetSavingsStatistics(ICollection<IdeaEntity> allIdeas)
        {
            var labels = allIdeas
                    .OrderBy(x => x.PlanDate?.Year)
                    .Select(x => x.PlanDate?.Year.ToString())
                    .Distinct()
                    .ToList();
            var data = new List<decimal>();

            foreach (var year in labels)
            {
                data.Add(allIdeas.Sum(x => x.FinancialReport.PlannedSavings));
            }

            return new FinancialStatisticsDto
            {
                Labels = labels,
                Data = data
            };
        }

        public FinancialStatisticsDto GetExpensesStatistics(ICollection<IdeaEntity> allIdeas)
        {
            var labels = allIdeas
                    .OrderBy(x => x.PlanDate?.Year)
                    .Select(x => x.PlanDate?.Year.ToString())
                    .Distinct()
                    .ToList();
            var data = new List<decimal>();

            foreach (var year in labels)
            {
                data.Add(allIdeas.Sum(x => x.FinancialReport.PlannedExpenses));
            }

            return new FinancialStatisticsDto
            {
                Labels = labels,
                Data = data
            };
        }

        public FinancialStatisticsDto GetBalanceStatistics(ICollection<IdeaEntity> allIdeas)
        {
            var labels = allIdeas
                    .OrderBy(x => x.PlanDate?.Year)
                    .Select(x => x.PlanDate?.Year.ToString())
                    .Distinct()
                    .ToList();
            var data = new List<decimal>();

            foreach (var year in labels)
            {
                data.Add(allIdeas.Sum(x => x.FinancialReport.PlannedBalance));
            }

            return new FinancialStatisticsDto
            {
                Labels = labels,
                Data = data
            };
        }

        public FinancialStatisticsDto GetBonusesStatistics(ICollection<IdeaEntity> allIdeas)
        {
            var labels = allIdeas
                    .OrderBy(x => x.PlanDate?.Year)
                    .Select(x => x.PlanDate?.Year.ToString())
                    .Distinct()
                    .ToList();
            var data = new List<decimal>();

            foreach (var year in labels)
            {
                data.Add(allIdeas.Sum(x => x.FinancialReport.Bonus.Bonus));
            }

            return new FinancialStatisticsDto
            {
                Labels = labels,
                Data = data
            };
        }

        public decimal GetTotalMoneySaved(ICollection<IdeaEntity> allIdeas)
        {
            return allIdeas.Sum(x => x.FinancialReport.PlannedBalance);
        }

        public decimal GetTotalBonuses(ICollection<IdeaEntity> allIdeas)
        {
            return allIdeas.Sum(x => x.FinancialReport.Bonus.Bonus);
        }

        public int GetFinancialBenefitsIdeasNumber(ICollection<IdeaEntity> allIdeas)
        {
            return allIdeas.Count(x => x.FinancialReport.PlannedBalance > 0);
        }

        public int GetNoFinancialBenefitsIdeasNumber(ICollection<IdeaEntity> allIdeas)
        {
            return allIdeas.Count(x => x.FinancialReport.PlannedBalance <= 0);
        }
    }
}
