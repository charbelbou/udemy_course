using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace udemy.Controllers.Resources
{
    // MakeResource class.
    // Derived from KeyValuePairResource
    public class MakeResource : KeyValuePairResource
    {
        // Collection of ModelResources for this MakeResource
        public ICollection<KeyValuePairResource> Models { get; set; }
        
        // Constructor which creates an instance of an empty collection (ModelResources)
        public MakeResource()
        {
            Models = new Collection<KeyValuePairResource>();
        }
    }
}