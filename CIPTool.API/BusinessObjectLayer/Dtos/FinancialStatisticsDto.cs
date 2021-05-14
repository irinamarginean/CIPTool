using System.Collections.Generic;

namespace BusinessObjectLayer.Dtos
{
    public class FinancialStatisticsDto
    {
        public ICollection<string> Labels { get; set; }
        public ICollection<decimal> Data { get; set; }
    }
}
