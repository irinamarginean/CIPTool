using System;
using System.Collections.Generic;

namespace BusinessObjectLayer.Dtos
{
    public class AddIdeaDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string RichTextDescription { get; set; }
        public string Context { get; set; }
        public string Target { get; set; }
        public DateTime DoDate { get; set; }
        public bool IsAssociateResponsible { get; set; }
        public bool IsIdeaSavingMoney { get; set; }
        public AddIdeaFinancialReportDto FinancialReport { get; set; }
        public List<string> Categories { get; set; }
        public List<IdeaAttachmentsDto> Attachments { get; set; }
        
    }
}
