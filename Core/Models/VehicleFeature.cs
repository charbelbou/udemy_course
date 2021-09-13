
using System.ComponentModel.DataAnnotations.Schema;

namespace udemy.Models
{
    [Table("VehicleFeatures")]
    // Relational table between Vechicles and Features
    public class VehicleFeature
    {
        // Vehicle ID and Feature ID
        public int VehicleId { get; set; }
        public int FeatureId { get; set; }
        // Vehicle and Feature
        public Vehicle Vehicle { get; set; }
        public Feature Feature { get; set; }
    }
}