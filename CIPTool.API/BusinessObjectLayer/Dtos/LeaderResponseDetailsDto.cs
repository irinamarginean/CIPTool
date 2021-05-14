using System;

namespace BusinessObjectLayer.Dtos
{
    public class LeaderResponseDetailsDto
    {
        public string IdeaId { get; set; }
        public string ReviewerName { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
        public string Reason { get; set; }
        public DateTime? Date { get; set; }
    }
}
