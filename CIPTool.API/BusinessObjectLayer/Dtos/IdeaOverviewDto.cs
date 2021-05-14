using System;
using System.Collections.Generic;

namespace BusinessObjectLayer.Dtos
{
    public class IdeaOverviewDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ResponseStatus Status { get; set; }
        public DateTime? PlanDate { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? ActualImplementationDate { get; set; }
        public DateTime? LeaderResponseAt { get; set; }
        public string AssociateName { get; set; }
        public string ReviewerName { get; set; }
        public FinancialReportDto FinancialReport { get; set; }
        public ICollection<string> Categories { get; set; }
    }
}
