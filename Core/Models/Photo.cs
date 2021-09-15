using System.ComponentModel.DataAnnotations;

namespace udemy_course1.Core.Models
{
    // Photo class
    public class Photo
    {
        // Photo Id
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        // Photo's File Name, required and string length of 255
        public string FileName { get; set; }
        public int VehicleId { get; set; }
    }
}