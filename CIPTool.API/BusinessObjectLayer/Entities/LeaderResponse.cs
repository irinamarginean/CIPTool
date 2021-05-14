using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjectLayer.Entities
{
    public class LeaderResponse
    {
        [Key]
        public Guid? Id { get; set; }
        public ResponseStatus Response { get; set; }
        public string Reason { get; set; }
        public DateTime? LeaderResponseDate { get; set; }
        public Guid? IdeaId { get; set; }
        public IdeaEntity Idea { get; set; }
        public string ReviewerId { get; set; }
        public Associate Reviewer { get; set; }
    }
}
