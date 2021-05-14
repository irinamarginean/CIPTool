namespace BusinessObjectLayer.Dtos
{
    public class FinancialReportDto
    {
        public decimal PlannedSavings { get; set; }
        public decimal PlannedExpenses { get; set; }
        public decimal PlannedBalance { get; set; }
        public decimal ActualSavings { get; set; }
        public decimal ActualExpenses { get; set; }
        public decimal ActualBalance { get; set; }
        public decimal Bonus { get; set; }
    }
}
