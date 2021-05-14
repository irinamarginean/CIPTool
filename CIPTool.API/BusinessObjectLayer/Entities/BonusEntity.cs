using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjectLayer.Entities
{
    public class BonusEntity
    {
        [Key]
        public Guid Id { get; set; }
        public decimal Bonus { get; set; }
        public int BonusRangeId { get; set; }
        public BonusRangeEntity BonusRange { get; set; }
        public int BonusCorrectionFactorId { get; set; }
        public BonusCorrectionFactorEntity BonusCorrectionFactor { get; set; }
        public Guid? FinancialReportId { get; set; }
        public FinancialReportEntity FinancialReport { get; set; }
    }
}
