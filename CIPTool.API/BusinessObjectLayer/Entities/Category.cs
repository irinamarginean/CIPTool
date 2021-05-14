using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjectLayer.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public ICollection<IdeaEntity> Ideas { get; set; }
    }
}
