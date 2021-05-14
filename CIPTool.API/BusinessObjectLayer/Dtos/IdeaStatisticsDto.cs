namespace BusinessObjectLayer.Dtos
{
    public class IdeaStatisticsDto
    {
        public int AllIdeasNumber { get; set; }
        public int WaitingForApprovalIdeasNumber { get; set; }
        public int ApprovedIdeasNumber { get; set; }
        public int PostponedIdeasNumber { get; set; }
        public int DeclinedIdeasNumber { get; set; }
        public int ImplementedIdeasNumber { get; set; }
        public double OtdDecisionValue { get; set; }
        public double OtdImplementationValue { get; set; }
        public int OtdDecisionGreenCategoryNumber { get; set; }
        public int OtdDecisionYellowCategoryNumber { get; set; }
        public int OtdDecisionRedCategoryNumber { get; set; }
        public int OtdImplementationGreenCategoryNumber { get; set; }
        public int OtdImplementationYellowCategoryNumber { get; set; }
        public int OtdImplementationRedCategoryNumber { get; set; }
        public FinancialStatisticsDto SavingsValues { get; set; }
        public FinancialStatisticsDto ExpensesValues { get; set; }
        public FinancialStatisticsDto BalanceValues { get; set; }
        public FinancialStatisticsDto BonusValues { get; set; }
        public int FinancialBenefitsIdeasNumber { get; set; }
        public int NoFinancialBenefitsIdeasNumber { get; set; }
        public decimal TotalMoneySaved { get; set; }
        public decimal TotalBonuses { get; set; }
    }
}
