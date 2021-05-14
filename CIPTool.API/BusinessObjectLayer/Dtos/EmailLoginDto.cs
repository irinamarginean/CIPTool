using System.ComponentModel.DataAnnotations;

namespace BusinessObjectLayer.Dtos
{
    public class EmailLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
