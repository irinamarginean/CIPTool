using System.ComponentModel.DataAnnotations;

namespace BusinessObjectLayer.Dtos
{
    public class UsernameLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
