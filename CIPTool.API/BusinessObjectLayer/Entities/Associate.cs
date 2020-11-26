using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BusinessObjectLayer.Entities
{
    public class Associate : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Prefix { get; set; }
        public bool IsLeader { get; set; }
        public string Team { get; set; }
        public string Group { get; set; }
        public string Department { get; set; }
        public Leader Leader { get; set; }
        public ICollection<IdeaEntity> Ideas { get; set; }
    }
}
