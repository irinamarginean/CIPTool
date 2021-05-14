using System;

namespace BusinessObjectLayer.Dtos
{
    public class LeaderResponseDto
    {
        public string IdeaId { get; set; }
        public string ReviewerId { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
        public string Reason { get; set; }
        public DateTime? Date { get; set; }
    }
}
