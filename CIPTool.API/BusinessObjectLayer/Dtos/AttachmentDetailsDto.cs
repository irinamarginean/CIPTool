using System;

namespace BusinessObjectLayer.Dtos
{
    public class AttachmentDetailsDto
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public DateTime? UploadedAt { get; set; }
    }
}
