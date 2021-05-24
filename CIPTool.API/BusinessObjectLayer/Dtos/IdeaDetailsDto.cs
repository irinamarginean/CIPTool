using System;
using System.Collections.Generic;

namespace BusinessObjectLayer.Dtos
{
    public class IdeaDetailsDto
    {
        public Guid Id { get; set; }
        public string IdeaNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RichTextDescription { get; set; }
        public string Context { get; set; }
        public string Target { get; set; }
        public string ReviewerName { get; set; }
        public bool IsAssociateResponsible { get; set; }
        public ResponseStatus Status { get; set; }
        public DateTime? PlanDate { get; set; }
        public DateTime? DoDate { get; set; }
        public DateTime? CheckDate { get; set; }
        public DateTime? ActDate { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? LeaderResponseAt { get; set; }
        public IdeaOwnerInfoDto IdeaOwnerDetails { get; set; }
        public FinancialReportDto FinancialReport { get; set; }
        public ICollection<string> Categories { get; set; }
        public ICollection<AttachmentDetailsDto> Attachments { get; set; }
        public ICollection<LeaderResponseDetailsDto> LeaderResponses { get; set; }
    }
}
