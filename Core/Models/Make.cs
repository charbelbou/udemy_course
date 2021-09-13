using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace udemy.Models
{
    // Car make
    public class Make
    {
        // make's Id 
        public int Id { get; set; }
        // make's Name 
        [Required]
        [StringLength(255)]        
        public string Name { get; set; }
        // Collection of Models for this make
        public ICollection<Model> Models { get; set; }
        
        // Constructor which creates an instance of an empty collection (models)
        public Make()
        {
            Models = new Collection<Model>();
        }
    }
}