using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using udemy_course1.Core.Models;

namespace udemy.Models
{
    [Table("Vehicles")]
    // Vehicle class
    public class Vehicle
    {
        // Vehicle Id
        public int Id { get; set; }

        // Model ID
        public int ModelId { get; set; }

        // Model object
        public Model Model { get; set; }

        // Whether it's registered or not
        public bool isRegistered { get; set; }

        [Required]
        [StringLength(255)]
        // Contact name (required)
        public string ContactName { get; set; }

        [StringLength(255)]
        // Contact Email (required)
        public string ContactEmail { get; set; }

        [Required]
        [StringLength(255)]
        // Contact Phone number (required)
        public string ContactPhone { get; set; }

        // DateTime for when it was last updated
        public DateTime LastUpdate { get; set; }

        // Collection of features
        public ICollection<VehicleFeature> Features { get; set; }

        // Collection of photos
        public ICollection<Photo> Photos { get; set; }
        
        // Constructor instantializing the Features collection
        public Vehicle()
        {
            Features = new Collection<VehicleFeature>();
            Photos = new Collection<Photo>();
        }
    }
}