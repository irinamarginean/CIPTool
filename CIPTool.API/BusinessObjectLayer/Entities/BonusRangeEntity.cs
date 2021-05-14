using System.ComponentModel.DataAnnotations;

namespace BusinessObjectLayer.Entities
{
    public class BonusRangeEntity
    {
        [Key]
        public int Id  { get; set; }
        public decimal LowerBound { get; set; }
        public decimal UpperBound { get; set; }
        public decimal Award { get; set; }
    }
}
