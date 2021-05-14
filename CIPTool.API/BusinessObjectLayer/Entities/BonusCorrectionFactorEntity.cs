using System.ComponentModel.DataAnnotations;

namespace BusinessObjectLayer.Entities
{
    public class BonusCorrectionFactorEntity
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public decimal CorrectionFactor { get; set; }
    }
}
