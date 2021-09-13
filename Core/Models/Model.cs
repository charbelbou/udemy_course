using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace udemy.Models
{
    [Table("Models")]
    // Car Model
    public class Model
    {
        // Model's id
        public int Id { get; set; }
        // Model's name
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        // Model's make
        public Make Make { get; set; }
        // Model's makeId
        public int MakeId { get; set; }
    }
}