using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjectLayer.Entities
{
    public class BonusEntity
    {
        [Key]
        public Guid Id { get; set; }
        public decimal LowerLimit { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal Award { get; set; }
        public bool IsSolutionPreviouslyDiscussed { get; set; }
        public float CorrectionFactor { get; set; }
        public decimal Bonus { get; set; }
        public FinancialReportEntity FinancialReport { get; set; }
    }
}
