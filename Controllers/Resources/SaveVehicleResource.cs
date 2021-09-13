using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

// Refactoring Resources
namespace udemy.Controllers.Resources
{
    // Vehicle Resource
    // Made for saving Vehicles
    public class SaveVehicleResource
    {
        // VehicleResource Id
        public int Id { get; set; }
        // Model ID
        public int ModelId { get; set; }
        // Whether it's registered or not
        public bool isRegistered { get; set; }
        [Required]
        // ContactResource Object
        // Object has name, email, and phone
        public ContactResource Contact { get; set; }
        // List of feature IDs as ints
        public ICollection<int> Features { get; set; }
        // Constructor instantializing the Features collection
        public SaveVehicleResource()
        {
            Features = new Collection<int>();
        }
    }
}