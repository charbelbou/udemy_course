using System.ComponentModel.DataAnnotations;

namespace udemy.Controllers.Resources
{
    public class ContactResource{
        //Contact Name, Required and string length of 255
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        //Contact Name, string length of 255
        [StringLength(255)]
        public string Email { get; set; }
        //Contact Phone number, Required and string length of 255
        [Required]
        [StringLength(255)]
        public string Phone { get; set; }
    }
}