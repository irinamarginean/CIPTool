using System.ComponentModel.DataAnnotations;

namespace BusinessObjectLayer.Dtos
{
    public class SimplifiedUserDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
