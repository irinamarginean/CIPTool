using System.Collections.Generic;

namespace BusinessObjectLayer.Entities
{
    public class Leader : Associate
    {
        public ICollection<Associate> Associates { get; set; }
    }
}
