using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjectLayer.Entities
{
    public class IdeaEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Context { get; set; }
        public string Target { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? LeaderResponseDate { get; set; }
        public bool IsApproved { get; set; }
        public bool IsAssociateResponsible { get; set; }
        public Associate Associate { get; set; }
        public Guid FinancialReportId { get; set; }
        public FinancialReportEntity FinancialReport { get; set; }
        public ICollection<IdeaStatusEntity> ApprovalStatuses { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
    }
}
