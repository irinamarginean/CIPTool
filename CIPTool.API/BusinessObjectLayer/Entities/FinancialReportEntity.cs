using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjectLayer.Entities
{
    public class FinancialReportEntity
    {
        [Key]
        public Guid Id { get; set; }
        public decimal PlannedSavings { get; set; }
        public decimal PlannedExpenses { get; set; }
        public decimal PlannedBalance { get; set; }
        public decimal ActualSavings { get; set; }
        public decimal ActualExpenses { get; set; }
        public decimal ActualBalance { get; set; }
        public DateTime? UploadedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid? BonusId { get; set; }
        public BonusEntity Bonus { get; set; }
        public Guid? IdeaId { get; set; }
        public IdeaEntity Idea { get; set; }
    }
}

