using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjectLayer.Entities
{
    public class Attachment
    {
        [Key]
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Location { get; set; }
        public DateTime? UploadedAt { get; set; }
        public IdeaEntity Idea { get; set; }
    }
}
