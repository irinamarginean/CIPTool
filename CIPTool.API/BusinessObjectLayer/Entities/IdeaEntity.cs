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
        public string Description { get; set; }
        public string RichTextDescription { get; set; }
        public string Context { get; set; }
        public string Target { get; set; }
        public string IdeaNumber { get; set; }
        public ResponseStatus Status { get; set; }
        public DateTime? PlanDate { get; set; }
        public DateTime? DoDate { get; set; }
        public DateTime? CheckDate { get; set; }
        public DateTime? ActDate { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsAssociateResponsible { get; set; }
        public string AssociateId { get; set; }
        public Associate Associate { get; set; }
        public string ReviewerId { get; set; }
        public Associate Reviewer { get; set; }
        public string ResponsibleId { get; set; }
        public Associate Responsible { get; set; }
        public Guid? FinancialReportId { get; set; }
        public FinancialReportEntity FinancialReport { get; set; }
        public ICollection<LeaderResponse> LeaderResponses { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
    }
}
