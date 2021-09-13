using System.ComponentModel.DataAnnotations;

namespace udemy.Models
{
    public class Feature
    {
        // Feature's Id
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        //Feature's name, required and has a limit of 255 characters
        public string Name { get; set; }
    }
}