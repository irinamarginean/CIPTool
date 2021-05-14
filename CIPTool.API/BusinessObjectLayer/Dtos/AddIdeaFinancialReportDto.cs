using System;

namespace BusinessObjectLayer.Dtos
{
    public class AddIdeaFinancialReportDto
    {
        public decimal PlannedSavings { get; set; }
        public decimal PlannedExpenses { get; set; }
        public DateTime? UploadedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
